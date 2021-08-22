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
                    case "exit":
                        appShouldTerminate = true;
                        break;
                    case "help":
                    case "?":
                        Log("(&9)Watchdog Help(&r):");
                        Log(" '(&e)stop(&r)' - Stop the WatchdogListener service");
                        Log(" '(&e)?(&r)' - Prints this message");
                        break;
                    case "status":
                        Log("Watchdog Listener: (&a)OK");
                        break;
                    case null:
                        break;
                    default:
                        Log("Unknown command. Type '?' for help.");
                        break;
                }
            }
            WatchdogListener listener = (WatchdogListener)Services.GetService(typeof(WatchdogListener));
            Log("Stopping WatchdogListener Service...");
            listener.Listening = false;
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
                return label?.ToLower();
            }
        }
        public string[] args { get; set; }
    }
}
