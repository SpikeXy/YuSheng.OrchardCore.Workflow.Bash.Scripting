using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using OrchardCore.Workflows.Helpers;
using YuSheng.OrchardCore.Workflow.Bash.Scripting.Activities;
using YuSheng.OrchardCore.Workflow.Bash.Scripting.Drivers;

namespace YuSheng.OrchardCore.Workflow.Python.Scripting
{
    [Feature("OrchardCore.Workflows")]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {

            services.AddActivity<BashScriptTask, ScriptTaskDisplayDriver>(); ;


        }
    }
}
