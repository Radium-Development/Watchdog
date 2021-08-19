using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watchdog.ProxyListener.IO
{
    class ConfigFileCreatedException : Exception
    {
        public ConfigFileCreatedException(string fileDir) =>
            throw new Exception($"Created Configuration File at '{fileDir}'. Update configuration and relaunch Watchdog.");
    }
}
