using Quartz;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public abstract class JobBase : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            throw new System.NotImplementedException();
        }

    }
}
