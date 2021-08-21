using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watchdog.ProxyListener.Singletons.Logging
{
    public interface ILogger
    {
        public void Log(string message, LogSeverity logSeverity = LogSeverity.INFO, ConsoleColor color = ConsoleColor.Gray);
        public void SetSeverity(LogSeverity severity);
    }
}
