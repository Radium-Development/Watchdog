using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Watchdog.ProxyListener.Singletons.Logging;

namespace Watchdog.ProxyListener.DependencyInjection
{
    public class ContainerBuilder
    {
        private ServiceCollection services;
        private Type startupType;
        private object startupInstance;

        public ContainerBuilder()
        {
            services = new ServiceCollection();
        }

        public ContainerBuilder WithStartup<T>() where T : class, new()
        {
            startupType = typeof(T);

            startupInstance = Activator.CreateInstance(startupType);
            MethodInfo configureServicesMethod = startupType.GetMethod("ConfigureServices");
            configureServicesMethod.Invoke(startupInstance, new[]{ services });

            services.AddSingleton<T>((T)Activator.CreateInstance(typeof(T)));
            return this;
        }

        public ContainerBuilder WithCLIParser<T>() where T : class
        {
            services.AddSingleton<T>();
            return this;
        }

        public ContainerBuilder WithLogger<T>(LogSeverity severity = LogSeverity.INFO) where T : class, ILogger, new()
        {
            ILogger logger = (T)new T();
            logger.SetSeverity(severity);
            services.AddSingleton<ILogger>(logger);
            return this;
        }

        public IServiceProvider Build()
        {
            var serviceProvider = services.BuildServiceProvider();

            MethodInfo configureMethod = startupType.GetMethod("Configure");
            object[] Params = configureMethod.GetParameters().ToList()
                .Select(t => t.ParameterType)
                .Select(t => serviceProvider.GetService(t))
                .ToArray();

            configureMethod.Invoke(startupInstance, Params);

            return serviceProvider;
        }
    }
}
