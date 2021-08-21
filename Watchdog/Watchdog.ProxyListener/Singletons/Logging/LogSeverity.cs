using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watchdog.ProxyListener.Singletons.Logging
{
    public enum LogSeverity
    {
        CLI,
        FATAL,
        ERROR,
        WARN,
        INFO,
        DEBUG,
        STACK
    }

    public static class LogSeverityExtensions
    {
        public static ConsoleColor GetForegroundColor(this LogSeverity logSeverity) =>
            (logSeverity) switch
            {
                LogSeverity.CLI => ConsoleColor.Gray,
                LogSeverity.DEBUG => ConsoleColor.Gray,
                LogSeverity.ERROR => ConsoleColor.Red,
                LogSeverity.FATAL => ConsoleColor.Red,
                LogSeverity.INFO => ConsoleColor.Gray,
                LogSeverity.WARN => ConsoleColor.Yellow,
                LogSeverity.STACK => ConsoleColor.Red,
                _ => ConsoleColor.Gray
            };
    }
}
