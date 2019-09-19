using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.DependencyInjection.NetCore;
using System;

namespace QuartzAspNetCoreDependencyInjectionExample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //setup our DI
            services
                .AddQuartz(cfg =>
                {
                    cfg.AddJob<ExampleJob>();
                    cfg.AddJob(typeof(ExampleJob2));
                })
                .AddLogging(configure => configure.AddDebug())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Debug);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseQuartz(cfg =>
            {
                cfg.Configure<ExampleJob>(j =>
                {
                    j.ConfigureJobDetail((builder, type) => builder.WithIdentity(type.FullName).Build());
                    j.ConfigureTrigger((builder, type) => builder.WithIdentity($"{type.FullName}.trigger")
                        .StartNow()
                        .WithSimpleSchedule(scheduleBuilder => scheduleBuilder.WithInterval(new TimeSpan(0, 0, 5)).RepeatForever())
                        .Build());
                });

                cfg.Configure<ExampleJob2>(j =>
                {
                    j.ConfigureJobDetail((builder, type) => builder.WithIdentity(type.FullName).Build());
                    j.ConfigureTrigger((builder, type) => builder.WithIdentity($"{type.FullName}.trigger")
                        .StartNow()
                        .WithSimpleSchedule(scheduleBuilder => scheduleBuilder.WithInterval(new TimeSpan(0, 0, 10)).RepeatForever())
                        .Build());
                });
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Observe output!");
            });
        }
    }
}
