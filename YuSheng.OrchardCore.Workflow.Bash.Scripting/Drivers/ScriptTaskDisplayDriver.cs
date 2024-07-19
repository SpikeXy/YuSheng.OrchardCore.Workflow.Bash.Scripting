using OrchardCore.Workflows.Display;
using OrchardCore.Workflows.Models;
using YuSheng.OrchardCore.Workflow.Bash.Scripting.Activities;
using YuSheng.OrchardCore.Workflow.Bash.Scripting.ViewModels;

namespace YuSheng.OrchardCore.Workflow.Bash.Scripting.Drivers
{
    public class ScriptTaskDisplayDriver : ActivityDisplayDriver<BashScriptTask, YuShengScriptTaskViewModel>
    {
        protected override void EditActivity(BashScriptTask source, YuShengScriptTaskViewModel model)
        {
            model.BashBinPath = source.BashBinPath.ToString();
            model.BashFilePath = source.BashFilePath.ToString();
            model.Script = source.Script.Expression;
        }

        protected override void UpdateActivity(YuShengScriptTaskViewModel model, BashScriptTask activity)
        {
            activity.BashBinPath = new WorkflowExpression<string>(model.BashBinPath);
            activity.BashFilePath = new WorkflowExpression<string>(model.BashFilePath);
            activity.Script = new WorkflowExpression<object>(model.Script);
        }
    }
}
