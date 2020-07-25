using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection
{
    using System;
    using System.Reflection;
    using DependencyInjection.ServiceFactory;
    using global::Microsoft.Extensions.Configuration;
    using global::Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;

    namespace ServiceFactory
    {
        public interface IServiceFactory<T>
        {
            T GetService(string env);
        }

        internal class ContainerServiceFactory<T> : IServiceFactory<T>
        {
            readonly IServiceProvider _services;
            readonly IConfiguration _configuration;
            public ContainerServiceFactory(IServiceProvider services, IConfiguration configuration)
            {
                _services = services;
                _configuration = configuration;
            }
            public T GetService(string env) 
            {
                if(!string.IsNullOrWhiteSpace(_configuration[$"{env}:class"]) && !string.IsNullOrWhiteSpace(_configuration[$"{env}:type"]))
                {
                    Assembly assem2 = Assembly.Load(_configuration[$"{env}:type"]);
                    var mytype = assem2.GetType(_configuration[$"{env}:class"]);
                    var myInstance = Activator.CreateInstance(mytype);
                    return (T)myInstance;
                }
                
                return _services.GetRequiredService<T>();

            }
        }
    }

    namespace Microsoft.Extensions.DependencyInjection
    {
        public static class ServiceFactoryServiceCollectionExtensions
        {
            public static IServiceCollection AddServiceFactory(this IServiceCollection services)
            {
                services.AddSingleton(typeof(IServiceFactory<>), typeof(ContainerServiceFactory<>));
                
                return services;
            }

        }
    }
}
