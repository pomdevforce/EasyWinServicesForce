using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWinServicesForce.Service
{
    using Atlas;

    using EasyWinServicesForce.Job;

    using Common.Logging;

    using Quartz;
    using Quartz.Spi;
    using System.Configuration;

    public class ServicesHost : IAmAHostedProcess
    {
        private readonly ILog log;


        /// <summary>
        /// Gets or sets the scheduler instance.
        /// </summary>
        public IScheduler Scheduler { get; set; }           // #1

        /// <summary>
        /// Gets or sets the job factory instance.
        /// </summary>
        public IJobFactory JobFactory { get; set; }         // #2

        /// <summary>
        /// Gets or sets the job listener instance.
        /// </summary>
        public IJobListener JobListener { get; set; }       // #3

        /// <summary>
        /// Starts the Windows Service.
        /// </summary>
        public ServicesHost()
        {
            this.log = LogManager.GetLogger(this.GetType());
        }
        public void Start()
        {
            this.log.Info("Windows Service starting");
            //Job Code
            var job = JobBuilder.Create<JobProcessor>()
                           .WithIdentity("SampleJob", "SampleWindowsService")
                           .Build();                   // #4

            var trigger = TriggerBuilder.Create()
                                        .WithIdentity("SampleTrigger", "SampleWindowsService")
                                        .WithCronSchedule(ConfigurationManager.AppSettings["CronExpression"])   // #5
                                        .ForJob("SampleJob", "SampleWindowsService")
                                        .Build();           // #6

            this.Scheduler.JobFactory = this.JobFactory;    // #7
            this.Scheduler.ScheduleJob(job, trigger);       // #8
            this.Scheduler.ListenerManager.AddJobListener(this.JobListener);    // #9
            this.Scheduler.Start();                         // #10

            this.log.Info("Windows Service started");
        }
        public void Stop()
        {
            this.log.Info("Sample Windows Service stopping");

            this.Scheduler.Shutdown();

            this.log.Info("Sample Windows Service stopped");
        }

        /// <summary>
        /// Resumes the Windows Service.
        /// </summary>
        public void Resume()
        {
            this.log.Info("Windows Service resuming");

            this.Scheduler.ResumeAll();

            this.log.Info("Windows Service resumed");
        }

        /// <summary>
        /// Pauses the Windows Service.
        /// </summary>
        public void Pause()
        {
            this.log.Info("Windows Service pausing");

            this.Scheduler.PauseAll();

            this.log.Info("Windows Service paused");
        }

    }
}
