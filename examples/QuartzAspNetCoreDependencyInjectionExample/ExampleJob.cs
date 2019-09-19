using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;

namespace QuartzAspNetCoreDependencyInjectionExample
{
    public class ExampleJob : IJob
    {
        readonly ILogger _log;

        public ExampleJob(ILoggerFactory logFactory)
        {
            _log = logFactory.CreateLogger(GetType());
        }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() => _log.LogDebug($"Job {GetType().FullName} executed"));
        }
    }
}
