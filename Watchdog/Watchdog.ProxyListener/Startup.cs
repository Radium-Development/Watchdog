using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Threading.Tasks;
using Watchdog.ProxyListener.Exceptions;
using Watchdog.ProxyListener.Models;

namespace Watchdog.ProxyListener {
    public class Startup {

        private HttpServer listener { get; }
        private Configuration configuration { get; }

        public Startup()
        {
            configuration = new Configuration();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Configuration>(configuration);    // Both Configuration and Config singletons are different
            services.AddSingleton<Config>(configuration);           // Configuration handles loading the config, while Config is the actual config
            services.AddSingleton<HttpServer>();
        }

        public void Configure(HttpServer httpServer)
        {
            httpServer.StartListener();
        }


    }
}