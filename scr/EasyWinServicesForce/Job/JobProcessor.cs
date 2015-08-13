namespace EasyWinServicesForce.Job
{
    using System;

    using EasyWinServicesForce.LogicLayer;
    using EasyWinServicesForce.Salesforce;

    using Common.Logging;

    using Quartz;

    public class JobProcessor : IJob
    {
        private readonly ILog log;
        public IBusinessProcess BusinessProcess { get; set; }
        public JobProcessor()
        {
            this.log = LogManager.GetLogger(this.GetType());

        }


        /// <summary>
        /// Called by the <c>Quartz.IScheduler</c> when a <c>Quartz.ITrigger</c> fires that is associated with the <c>Quartz.IJob</c>.
        /// </summary>
        /// <param name="context">JobExecutionContext instance</param>
        public void Execute(IJobExecutionContext context)
        {
            this.log.Info("Application executing");
            this.log.Info("Job Process ID : " + this.GetHashCode());
            this.log.Info("BusinessProcess ID : " + BusinessProcess.GetHashCode());
            try
            {
                var task = BusinessProcess.Run();
                task.Wait();
            }
            catch (Exception e)
            {
                log.Fatal(e);

            }
            finally
            {

            }

            this.log.Info("Application executed");
        }
    }
}
