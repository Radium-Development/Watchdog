using System;
using System.Net;

namespace Watchdog.ProxyListener
{
    class Program
    {
        static void Main(string[] args) => new Program()
            .BuildArgs(args); // Escape static scope

        public Configuration Configuration { get; set; }

        public Program()
        {
            Configuration = new Configuration();

            // Create the HttpListener and start accepting client connections
        }

        public void BuildArgs(string[] args)
        {
            // Use a CLI library to parse arguments from console
            // Some arguments will be used to override the ports used from the config file for example
        }
    }
}
