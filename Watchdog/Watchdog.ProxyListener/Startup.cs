using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Threading.Tasks;
using Watchdog.ProxyListener.Exceptions;
using Watchdog.ProxyListener.IO;
using Watchdog.ProxyListener.Models;
using Watchdog.ProxyListener.Singletons;

namespace Watchdog.ProxyListener {
    public class Startup {

        private WatchdogListener listener { get; }
        private Configuration configuration { get; }

        public Startup()
        {
            configuration = new Configuration();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Configuration>(configuration);    // Both Configuration and Config singletons are different
            services.AddSingleton<Config>(configuration);           // Configuration handles loading the config, while Config is the actual config
            services.AddSingleton<WatchdogListener>();
            services.AddSingleton<RemoteDB>();
        }

        public void Configure(WatchdogListener httpServer, RemoteDB database, ILogger logger)
        {
            httpServer.StartListener();
            logger.log(database.GetAllAttackStrings().First().String);
        }
    }
}