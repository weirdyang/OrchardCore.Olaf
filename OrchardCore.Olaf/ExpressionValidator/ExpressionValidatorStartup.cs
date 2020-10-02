using Microsoft.Extensions.DependencyInjection;
using OrchardCore.BackgroundTasks;
using OrchardCore.Modules;
using OrchardCore.Olaf.Delay;
using OrchardCore.Olaf.Dummy;
using OrchardCore.Olaf.ExpressionValidator;
using OrchardCore.Olaf.ExpressionValidator.Services;
using OrchardCore.Workflows.Helpers;
namespace OrchardCore.Olaf
{
    [Feature("OrchardCore.Olfa.ExpressionValidator")]
    public class ExpressionValidatorStartup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddActivity<ExpressionValidatorTask, ExpressionValidatorTaskDisplay>();
            services.AddScoped<ValidationService>();
        }
    }
}
