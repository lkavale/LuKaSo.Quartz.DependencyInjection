using Microsoft.AspNetCore.Builder;
using System;
using System.Linq;

namespace Quartz.DependencyInjection.NetCore
{
    /// <summary>
    /// Job option configurator
    /// </summary>
    public class JobOptionConfigurator
    {
        /// <summary>
        /// Job type
        /// </summary>
        private readonly Type _jobType;

        /// <summary>
        /// Job detail
        /// </summary>
        internal IJobDetail JobDetail { get; private set; }

        /// <summary>
        /// Trigger
        /// </summary>
        internal ITrigger Trigger { get; private set; }

        /// <summary>
        /// Job option configurator
        /// </summary>
        /// <param name="services"></param>
        public JobOptionConfigurator(Type jobType)
        {
            _jobType = jobType ?? throw new ArgumentNullException(nameof(jobType));

            if (!jobType.GetInterfaces().Any(t => t == typeof(IJob)))
                throw new ArgumentException($"Given {jobType.FullName} is not implement IJob interface", nameof(jobType));
        }

        /// <summary>
        /// Configure job detail
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureJobDetail(Func<JobBuilder, Type, IJobDetail> builder)
        {
            JobDetail = builder(JobBuilder.Create(_jobType), _jobType);
        }

        /// <summary>
        /// Configure trigger
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureTrigger(Func<TriggerBuilder, Type, ITrigger> builder)
        {
            Trigger = builder(TriggerBuilder.Create(), _jobType);
        }
    }
}
