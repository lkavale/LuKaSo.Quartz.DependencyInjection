using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.DependencyInjection.NetCore;
using System;

namespace QuartzNetCoreDependencyInjectionExample
{
    public class Program
    {
        protected Program() { }

        static void Main()
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddQuartz(cfg => cfg.AddJob<ExampleJob>())
                .AddLogging(configure => configure.AddConsole())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Debug)
                .BuildServiceProvider();

            //configure console logging
            var logger = serviceProvider
                .GetService<ILogger<Program>>();

            logger.LogDebug("Starting test application...");

            // Quartz
            var scheduler = serviceProvider.GetService<IScheduler>();

            var jobName = typeof(ExampleJob).FullName;

            var job = JobBuilder.Create<ExampleJob>()
                .WithIdentity(jobName)
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{jobName}.trigger")
                .StartNow()
                .WithSimpleSchedule(scheduleBuilder =>
                    scheduleBuilder
                        .WithInterval(new TimeSpan(0, 0, 5))
                        .RepeatForever())
                .Build();

            scheduler.ScheduleJob(job, trigger);

            Console.Read();
        }
    }
}
