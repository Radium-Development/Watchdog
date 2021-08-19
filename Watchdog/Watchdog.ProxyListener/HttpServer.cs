using System;
using System.Net;
using Watchdog.ProxyListener.Exceptions;

namespace Watchdog.ProxyListener {
    public class HttpServer {

        private HttpListener listener { get; }

        public HttpServer() {
            listener = new HttpListener();
        }

        public void StartListener() {
            if (!HttpListener.IsSupported)
                throw new HttpListenerNotSupportedException();
        }


    }
}