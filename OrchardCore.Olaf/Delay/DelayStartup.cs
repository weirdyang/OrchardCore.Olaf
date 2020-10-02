using Microsoft.Extensions.DependencyInjection;
using OrchardCore.BackgroundTasks;
using OrchardCore.Modules;
using OrchardCore.Olaf.Dummy;
using OrchardCore.Workflows.Helpers;
namespace OrchardCore.Olaf
{
    [Feature("OrchardCore.Olfa.Delay")]
    public class DelayStartup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddActivity<OrchardCore.Olaf.Delay.DelayEvent, OrchardCore.Olaf.Delay.DelayEventDisplay>();
            services.AddSingleton<IBackgroundTask, OrchardCore.Olaf.Delay.DelayBackgroundTask>();
        }
    }
}
