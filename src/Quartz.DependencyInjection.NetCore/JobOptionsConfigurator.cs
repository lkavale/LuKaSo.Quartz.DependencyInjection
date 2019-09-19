using Microsoft.AspNetCore.Builder;
using System;

namespace Quartz.DependencyInjection.NetCore
{
    /// <summary>
    /// Job options configurator
    /// </summary>
    public class JobOptionsConfigurator
    {
        /// <summary>
        /// Application builder
        /// </summary>
        private readonly IApplicationBuilder _builder;

        /// <summary>
        /// Job option configurator
        /// </summary>
        /// <param name="services"></param>
        public JobOptionsConfigurator(IApplicationBuilder builder)
        {
            _builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        /// <summary>
        /// Configure job
        /// </summary>
        /// <typeparam name="TJob"></typeparam>
        /// <param name="configure"></param>
        public void Configure<TJob>(Action<JobOptionConfigurator> configure) where TJob : IJob
        {
            if (_builder.ApplicationServices.GetService(typeof(TJob)) == null)
                throw new NotSupportedException($"Cannot configure not registered job {typeof(TJob).FullName}");

            var options = new JobOptionConfigurator(typeof(TJob));

            configure?.Invoke(options);

            ScheduleJob(options.JobDetail, options.Trigger);
        }

        /// <summary>
        /// Schedule job with trigger
        /// </summary>
        /// <param name="jobDetail"></param>
        /// <param name="trigger"></param>
        private void ScheduleJob(IJobDetail jobDetail, ITrigger trigger)
        {
            if (jobDetail == null)
                throw new ArgumentNullException(nameof(jobDetail));

            if (trigger == null)
                throw new ArgumentNullException(nameof(trigger));

            var scheduler = (IScheduler)_builder.ApplicationServices.GetService(typeof(IScheduler)) ?? throw new NotSupportedException($"Cannot schedule job without registered scheduler");

            scheduler.ScheduleJob(jobDetail, trigger);
        }
    }
}
