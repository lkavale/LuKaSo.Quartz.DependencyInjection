using Microsoft.Extensions.DependencyInjection;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Threading.Tasks;

namespace Quartz.DependencyInjection.NetCore
{
    /// <summary>
    /// NET Core service collection extension
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add quartz to service collection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IServiceCollection AddQuartz(this IServiceCollection services, Action<JobConfigurator> configure)
        {
            services.AddSingleton<IJobFactory, QuartzFactory>();
            services.AddSingleton<IScheduler>(provider => CreateScheduler(provider).GetAwaiter().GetResult());

            configure?.Invoke(new JobConfigurator(services));

            return services;
        }

        /// <summary>
        /// Create scheduler
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static async Task<IScheduler> CreateScheduler(IServiceProvider provider)
        {
            var scheduler = await new StdSchedulerFactory().GetScheduler();
            scheduler.JobFactory = provider.GetRequiredService<IJobFactory>();
            await scheduler.Start();

            return scheduler;
        }
    }
}
