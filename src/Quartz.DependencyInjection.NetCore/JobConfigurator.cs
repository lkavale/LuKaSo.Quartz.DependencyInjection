using Microsoft.Extensions.DependencyInjection;
using Quartz.DependencyInjection.NetCore.Common;
using System;
using System.Linq;

namespace Quartz.DependencyInjection.NetCore
{
    /// <summary>
    /// Job configurator
    /// </summary>
    public class JobConfigurator
    {
        /// <summary>
        /// Service collection
        /// </summary>
        private readonly IServiceCollection _services;

        /// <summary>
        /// Job configurator
        /// </summary>
        /// <param name="services"></param>
        public JobConfigurator(IServiceCollection services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        /// <summary>
        /// Add job
        /// </summary>
        /// <typeparam name="TJob"></typeparam>
        public void AddJob<TJob>() where TJob : IJob
        {
            AddJobToServices(typeof(TJob));
        }

        /// <summary>
        /// Add job
        /// </summary>
        /// <param name="type"></param>
        public void AddJob(Type type)
        {
            if (!type.GetInterfaces().Any(t => t == typeof(IJob)))
                throw new ArgumentException($"Given {type.FullName} is not implement IJob interface", nameof(type));

            AddJobToServices(type);
        }

        /// <summary>
        /// Add job to service collection
        /// </summary>
        /// <param name="type"></param>
        private void AddJobToServices(Type type)
        {
            var jobDescription = new ServiceDescriptor(type, type, ServiceLifetime.Singleton);

            if (_services.Contains(jobDescription, new ServiceDescriptorEqualityComparer()))
                throw new ObjectAlreadyExistsException($"Job {type.FullName} is already registered");

            _services.Add(jobDescription);
        }
    }
}
