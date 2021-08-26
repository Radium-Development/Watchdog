using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                    Queue<string> sections = new Queue<string>(Regex.Split(line, "(\\([*&][0-9a-fA-FrR]\\))"));

                    while(sections.Count > 0)
                    {
                        var section = sections.Dequeue();
                        //string msg;
                        while ((section.StartsWith("(&") || section.StartsWith("(*")) && section.EndsWith(")"))
                        {
                            var clrSelection = section[2].ToString();
                            int clrCode = 0;
                            if (clrSelection.ToLower() == "r")
                            {
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                            else
                                clrCode = int.Parse(clrSelection, System.Globalization.NumberStyles.HexNumber);

                            if (section.StartsWith("(&") && clrSelection.ToLower() != "r")
                                Console.ForegroundColor = (ConsoleColor)clrCode;
                            else if (section.StartsWith("(*") && clrSelection.ToLower() != "r")
                                Console.BackgroundColor = (ConsoleColor)clrCode;

                            section = sections.Count > 0 ? sections.Dequeue() : "";
                        }

                        if (sections.Count == 0)
                            Console.WriteLine($"{section}");
                        else
                            Console.Write($"{section}");
                    }
                    
                }
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        public void SetSeverity(LogSeverity severity)
        {
            this.severity = severity;
        }
    }
}
