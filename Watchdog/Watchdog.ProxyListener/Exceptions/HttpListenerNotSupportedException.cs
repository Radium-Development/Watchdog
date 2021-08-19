using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watchdog.ProxyListener.Exceptions
{
    class HttpListenerNotSupportedException : Exception
    {
        public HttpListenerNotSupportedException() : base($"Installed version of .NET does not support HttpListener") {

        }
    }
}
