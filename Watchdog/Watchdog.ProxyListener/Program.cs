using System;
using System.Linq;
using Watchdog.ProxyListener.DependencyInjection;
using Watchdog.ProxyListener.Singletons;
using Watchdog.ProxyListener.Singletons.Logging;

namespace Watchdog.ProxyListener
{
    class Program
    {
        private static ILogger logger;
        static void Main(string[] args)
        {
            IServiceProvider Services = 
                new ContainerBuilder()
                    .WithLogger<Logger>(LogSeverity.INFO)
                    .WithStartup<Startup>()
                    .Build();

            logger = (ILogger)Services.GetService(typeof(ILogger));

            var appShouldTerminate = false;
            while (!appShouldTerminate)
            {
                var command = generateCommand(Console.ReadLine());
                switch (command.command)
                {
                    case "stop":
                        appShouldTerminate = true;
                        break;
                    case "help":
                    case "?":
                        Log("Watchdog Help:");
                        Log(" 'stop' - Stop the WatchdogListener service");
                        Log(" '?' - Prints this message");
                        break;
                    default:
                        Log("Unknown command. Type '?' for help.");
                        break;
                }
            }
            WatchdogListener listener = (WatchdogListener)Services.GetService(typeof(WatchdogListener));
            listener.Listening = false;
            listener.TerminateAllClients();
        }

        private static void Log(string msg, ConsoleColor color = ConsoleColor.Gray) =>
            logger.Log(msg, LogSeverity.CLI, color);

        private static Command generateCommand(string cmd)
        {
            if (string.IsNullOrWhiteSpace(cmd))
                return new Command();
            var splitString = cmd.Split(' ');
            return new Command()
            {
                label = splitString[0],
                args = splitString.Length > 1 ? splitString.ToList().Skip(1).ToArray() : new string[0]
            };
        } 
    }

    public class Command
    {
        public string label { get; set; } // Case sensitive version of command string
        public string command { // lowercase version of command string
            get
            {
                return label.ToLower();
            }
        }
        public string[] args { get; set; }
    }
}
