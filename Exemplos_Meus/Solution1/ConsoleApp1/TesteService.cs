using Quartz;
using System.Linq;

namespace ConsoleApp1
{
    public class TesteService
    {
        private readonly IScheduler _sched;

        public void Start()
        {

            var listaTypeJobBase = GetType().Assembly.GetTypes()
               .Where(w => w.BaseType == typeof(JobBase));

            foreach (var item in listaTypeJobBase)
            {
                var jobId = item.Name;
                var job = JobBuilder.Create(item)
                    .WithIdentity(jobId)
                    .Build();

                var trigger = TriggerBuilder.Create()
                    .WithIdentity(jobId)
                    .StartNow()
                    //.WithCronSchedule("0 0/1 * * * ?") //A cada 1 minuto
                    .WithSimpleSchedule(b => b.WithIntervalInSeconds(60)
                    .RepeatForever())
                        .Build();

                _sched.ScheduleJob(job, trigger);
            }

            _sched.Start();
        }

        public void Stop()
        {
        }
    }
}
