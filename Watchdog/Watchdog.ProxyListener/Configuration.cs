using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watchdog.ProxyListener.IO;
using Watchdog.ProxyListener.Models;
using Watchdog.ProxyListener.Exceptions;

namespace Watchdog.ProxyListener
{
    public class Configuration : JsonInterfacer<Config>
    {
        public Configuration() : base("config.json", (location) => {
            throw new ConfigFileCreatedException(location); // Throw an exception if the config file was just created
        }) {

        }


    }
}
