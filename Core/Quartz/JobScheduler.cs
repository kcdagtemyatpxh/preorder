﻿using Autofac;
using Quartz;

namespace TripleSix.Core.Quartz
{
    public class JobScheduler
    {
        private readonly IScheduler _scheduler;

        public JobScheduler(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public IComponentContext Container { get; set; }

        public void Start()
        {
            var jobTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes()
                .Where(t => t.IsPublic)
                .Where(t => !t.IsAbstract)
                .Where(t => t.IsAssignableTo<BaseJob>()));

            foreach (var jobType in jobTypes)
            {
                var instance = Activator.CreateInstance(jobType) as BaseJob;
                if (instance == null) continue;

                var job = instance.JobBuilder(JobBuilder.Create(jobType)).Build();
                var trigger = instance.TriggerBuilder(TriggerBuilder.Create()).Build();
                _scheduler.ScheduleJob(job, trigger);
            }

            _scheduler.Start();
        }
    }
}
