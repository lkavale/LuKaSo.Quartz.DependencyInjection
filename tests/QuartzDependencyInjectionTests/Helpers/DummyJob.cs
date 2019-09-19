using Quartz;
using System.Threading.Tasks;

namespace QuartzDependencyInjectionTests.Helpers
{
    public class DummyJob : IJob
    {
        public DummyJob()
        {
        }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() => { });
        }
    }
}
