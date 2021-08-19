using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watchdog.ProxyListener.Exceptions
{
    class ConfigFileCreatedException : Exception
    {
        public ConfigFileCreatedException(string fileDir) : base($"Create Configuration File At '{fileDir}'. Update configuration and relaunch Watchdog.") {

        }
    }
}
