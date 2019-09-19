using Microsoft.AspNetCore.Builder;
using System;

namespace Quartz.DependencyInjection.NetCore
{
    /// <summary>
    /// ASP.NET Core application builder extension
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configure Quartz
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseQuartz(this IApplicationBuilder builder, Action<JobOptionsConfigurator> configure)
        {
            configure?.Invoke(new JobOptionsConfigurator(builder));

            return builder;
        }
    }
}
