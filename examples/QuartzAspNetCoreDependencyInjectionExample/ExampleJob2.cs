using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;

namespace QuartzAspNetCoreDependencyInjectionExample
{
    public class ExampleJob2 : IJob
    {
        readonly ILogger _log;

        public ExampleJob2(ILoggerFactory logFactory)
        {
            _log = logFactory.CreateLogger(GetType());
        }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() => _log.LogDebug($"Job {GetType().FullName} executed"));
        }
    }
}
