using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Threading.Tasks;
using Watchdog.ProxyListener.Exceptions;
using Watchdog.ProxyListener.IO;
using Watchdog.ProxyListener.Models;
using Watchdog.ProxyListener.Singletons;
using Watchdog.ProxyListener.Data;
using Watchdog.ProxyListener.Singletons.Logging;
using System.Linq;

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
            //services.AddSingleton<IRemoteDB, RemoteDB>();     // use this for connecting to actual database
            services.AddSingleton<IRemoteDB, MockRemoteDB>();   // use this for mock test
            services.AddSingleton<WatchdogChecker>();
        }

        public void Configure(WatchdogListener httpServer, WatchdogChecker stringChecker, IRemoteDB database, ILogger logger)
        {
            //httpServer.StartListener(); // This currently hangs the thread. Leave it for last
            stringChecker.ContainsAttackString("aasdf");
            stringChecker.ContainsAttackString("aasdf123");
            stringChecker.ContainsAttackString("aasdf123123123");
            stringChecker.ContainsAttackString("<script>alert('xss')</script>");
        }
    }
}