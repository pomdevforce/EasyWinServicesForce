using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWinServicesForce.IoC
{
    using Atlas;

    using Quartz;

    /// This represents an entity for event handling while a job is running.
    /// </summary>
    internal class JobListener : IJobListener
    {
        private readonly IContainerProvider _provider;

        private IUnitOfWorkContainer _container;

        /// <summary>
        /// Initialises a new instance of the SampleJobListener class.
        /// </summary>
        /// <param name="provider">Container provider instance.</param>
        public JobListener(IContainerProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            this._provider = provider;
            this.Name = "JobListener";
        }

        /// <summary>
        /// Gets the name of the job listener.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Called by the <c>Quartz.IScheduler</c> when a <c>Quartz.IJobDetail</c> is about to be executed
        /// (an associated <c>Quartz.ITrigger</c> has occurred).
        /// </summary>
        /// <param name="context">JobExecutionContext instance.</param>
        /// <remarks>
        /// This method will not be invoked if the execution of the Job was vetoed by a <c>Quartz.ITriggerListener</c>.
        /// </remarks>
        public void JobToBeExecuted(IJobExecutionContext context)
        {
            this._container = this._provider.CreateUnitOfWork();
            this._container.InjectUnsetProperties(context.JobInstance);
        }

        /// <summary>
        /// Called by the <c>Quartz.IScheduler</c> when a <c>Quartz.IJobDetail</c> was about to be executed
        /// (an associated <c>Quartz.ITrigger</c> has occurred), but a <c>Quartz.ITriggerListener</c> vetoed it's execution.
        /// </summary>
        /// <param name="context">JobExecutionContext instance.</param>
        public void JobExecutionVetoed(IJobExecutionContext context)
        {
        }

        /// <summary>
        /// Called by the <c>Quartz.IScheduler</c> after a <c>Quartz.IJobDetail</c> has been executed,
        /// and be for the associated <c>Quartz.Spi.IOperableTrigger</c>'s <c>Quartz.Spi.IOperableTrigger.Triggered(Quartz.ICalendar)</c> method has been called.
        /// </summary>
        /// <param name="context">JobExecutionContext instance.</param>
        /// <param name="jobException">JobExecutionException instance.</param>
        public void JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
        {
            this._container.Dispose();
        }
    }
}
