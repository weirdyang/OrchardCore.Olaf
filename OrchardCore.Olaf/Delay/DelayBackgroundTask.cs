using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.BackgroundTasks;
using OrchardCore.Workflows.Services;


namespace OrchardCore.Olaf.Delay
{
    [BackgroundTask(Schedule = "* * * * *", Description = "Trigger Delay events.")]
    public class DelayBackgroundTask : IBackgroundTask
    {
        public Task DoWorkAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            var workflowManager = serviceProvider.GetRequiredService<IWorkflowManager>();
            return workflowManager.TriggerEventAsync(DelayEvent.EventName, null, null, isExclusive: true, isAlwaysCorrelated: true);
        }
    }
}
