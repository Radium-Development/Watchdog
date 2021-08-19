using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Watchdog.ProxyListener.Exceptions;
using Watchdog.ProxyListener.Models;

namespace Watchdog.ProxyListener
{
    public class HttpServer
    {
        private Config _config { get; }
        private HttpListener httpListener;
        public HttpServer(Config config)
        {
            this._config = config;
            httpListener = new HttpListener();
        }

        public void StartListener()
        {
            if (!HttpListener.IsSupported)
                throw new HttpListenerNotSupportedException();

            foreach(string ip in _config.ListnerIPs)
            {
                string prefix = $"http://{ip}:{_config.WatchdogListenerPort}/";
                httpListener.Prefixes.Add(prefix);
                Console.WriteLine($"[HttpServer] Started Listening on: {prefix}");
            }

            httpListener.Start(); // There is no multithreading yet. Just POC that server runs.
                                  // Check open ports with command line to ensure httpListener is in fact running correctly
        }
    }
}
