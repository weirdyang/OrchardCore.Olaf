using Microsoft.Extensions.DependencyInjection;
using OrchardCore.BackgroundTasks;
using OrchardCore.Modules;
using OrchardCore.Olaf.Dummy;
using OrchardCore.Workflows.Helpers;
namespace OrchardCore.Olaf
{
    [Feature("OrchardCore.Olfa.Dummy")]
    public class DummyStartup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddActivity<DummyEvent, DummyEventDisplay>();
            services.AddSingleton<IBackgroundTask, DummyBackgroundTask>();
        }
    }
}
