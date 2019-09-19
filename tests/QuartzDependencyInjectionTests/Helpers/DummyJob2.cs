using Quartz;
using System.Threading.Tasks;

namespace QuartzDependencyInjectionTests.Helpers
{
    public class DummyJob2 : IJob
    {
        public DummyJob2()
        {
        }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() => { });
        }
    }
}
