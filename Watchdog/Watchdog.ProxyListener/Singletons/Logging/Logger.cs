using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watchdog.ProxyListener.Singletons.Logging
{
    public class Logger : ILogger
    {
        private LogSeverity _severity;
        public LogSeverity severity
        {
            get { return this._severity; }
            private set { this._severity = value; }
        }

        public void Log(string message, LogSeverity logSeverity = LogSeverity.INFO, ConsoleColor color = ConsoleColor.Gray)
        {
            if (logSeverity <= severity)
            {
                foreach (var line in message.Split("\n"))
                {
                    Console.ForegroundColor = logSeverity.GetForegroundColor();
                    Console.Write($"[{DateTime.Now.ToString("HH:mm:ss")} {logSeverity.ToString()}]: ");
                    Console.ForegroundColor = color;
                    Console.WriteLine($"{line}");
                }
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        public void SetSeverity(LogSeverity severity)
        {
            this.severity = severity;
        }
    }
}
