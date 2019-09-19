# LuKaSo Quartz.Net dependency injection
### About the project
Goal of this project is provide integration for Quartz.Net with IoC containers.  
### Supported IoC containers
* Native .NET Core IoC conatiner 
### Installation
Run command in package manager:
```
Install-Package Quartz.DependencyInjection.NetCore
```
### Usage
* Add following namespace ```Quartz.DependencyInjection.NetCore;```
* Adding Quartz.Net (ExampleJob) job to container
```
services
    .AddQuartz(cfg =>
    {
        cfg.AddJob<ExampleJob>();
    })
```
* Scheduling job using ApplicationBuilder
```
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
});
```
* Scheduling job manually  
```
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
```

For more informations take a look on the examples.