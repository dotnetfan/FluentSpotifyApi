using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace FluentSpotifyApi.Core.Internal.Extensions
{
    /// <summary>
    /// Set of <see cref="IServiceCollection"/> extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the provided <see cref="ServiceDescriptor.ServiceType"/> is already present in the <paramref name="services"/>.
        /// </exception>
        public static IServiceCollection RegisterSingleton<TService>(this IServiceCollection services)
            where TService : class
        {
            return services.RegisterSingleton<TService, TService>();
        }

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the provided <see cref="ServiceDescriptor.ServiceType"/> is already present in the <paramref name="services"/>.
        /// </exception>
        public static IServiceCollection RegisterSingleton<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            return services.Register(ServiceDescriptor.Singleton<TService, TImplementation>());
        }

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="implementationInstance">The implementation instance.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the provided <see cref="ServiceDescriptor.ServiceType"/> is already present in the <paramref name="services"/>.
        /// </exception>
        public static IServiceCollection RegisterSingleton<TService>(this IServiceCollection services, TService implementationInstance) where TService : class
        {
            return services.Register(ServiceDescriptor.Singleton(implementationInstance));
        }

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="implementationFactory">The implementation factory.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the provided <see cref="ServiceDescriptor.ServiceType"/> is already present in the <paramref name="services"/>.
        /// </exception>
        public static IServiceCollection RegisterSingleton<TService>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory) where TService : class
        {
            return services.Register(ServiceDescriptor.Singleton(implementationFactory));
        }

        /// <summary>
        /// Registers the specified service descriptor.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="serviceDescriptor">The service descriptor.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the provided <see cref="ServiceDescriptor.ServiceType"/> is already present in the <paramref name="services"/>.
        /// </exception>
        public static IServiceCollection Register(this IServiceCollection services, ServiceDescriptor serviceDescriptor)
        {
            if (services.Any(item => item.ServiceType == serviceDescriptor.ServiceType))
            {
                throw new InvalidOperationException($"Service of type {serviceDescriptor.ServiceType} has already been registered.");
            }

            services.Add(serviceDescriptor);

            return services;
        }
    }
}
