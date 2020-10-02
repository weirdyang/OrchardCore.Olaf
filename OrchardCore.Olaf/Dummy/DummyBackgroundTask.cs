using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.BackgroundTasks;
using OrchardCore.Workflows.Services;

namespace OrchardCore.Olaf.Dummy
{
    [BackgroundTask(Schedule = "* * * * *", Description = "Trigger Dummy events.")]
    public class DummyBackgroundTask : IBackgroundTask
    {
        public Task DoWorkAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            var workflowManager = serviceProvider.GetRequiredService<IWorkflowManager>();
            return workflowManager.TriggerEventAsync(DummyEvent.EventName, null, null, isExclusive: true, isAlwaysCorrelated: true);
        }
    }
}
