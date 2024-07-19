using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using OrchardCore.Workflows.Abstractions.Models;
using OrchardCore.Workflows.Activities;
using OrchardCore.Workflows.Models;
using OrchardCore.Workflows.Services;
using SimpleExec;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace YuSheng.OrchardCore.Workflow.Bash.Scripting.Activities
{
    public class BashScriptTask : TaskActivity
    {
        private readonly IWorkflowScriptEvaluator _scriptEvaluator;
        private readonly IStringLocalizer S;
        private readonly IHtmlHelper _htmlHelper;
        private readonly IWorkflowExpressionEvaluator _expressionEvaluator;
        public BashScriptTask(IWorkflowScriptEvaluator scriptEvaluator,
            IWorkflowExpressionEvaluator expressionEvaluator,
            IHtmlHelper htmlHelper,
            IStringLocalizer<BashScriptTask> localizer)
        {
            _scriptEvaluator = scriptEvaluator;
            _expressionEvaluator = expressionEvaluator;
            S = localizer;
            _htmlHelper = htmlHelper;

        }

        public override string Name => nameof(BashScriptTask);

        public override LocalizedString DisplayText => S["Bash Script Task"];

        public override LocalizedString Category => S["Script"];

        public WorkflowExpression<string> BashBinPath
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }

        public WorkflowExpression<string> BashFilePath
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }

        /// <summary>
        /// 
        /// </summary>
        public WorkflowExpression<object> Script
        {
            get => GetProperty(() => new WorkflowExpression<object>());
            set => SetProperty(value);
        }

        public override IEnumerable<Outcome> GetPossibleOutcomes(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return Outcomes(S["Success"], S["Failed"]);
        }

        public override async Task<ActivityExecutionResult> ExecuteAsync(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {

            var tempPythonFileName = Guid.NewGuid().ToString();
            var bashBinPath = await _expressionEvaluator.EvaluateAsync(BashBinPath, workflowContext, null);
            var bashFilePath = await _expressionEvaluator.EvaluateAsync(BashFilePath, workflowContext, null);
            var script = Script.Expression;
            var exeContent = "";
            if (string.IsNullOrEmpty(bashBinPath)) 
            {
                workflowContext.Output["BashScript"] = "Bash Bin File Path is null";
                return Outcomes("Failed");
            } 
            else
            {
                if (!string.IsNullOrEmpty(bashFilePath))
                {
                    exeContent = bashFilePath;
                }
                else
                {
                    if (!string.IsNullOrEmpty(script))
                    {
                        exeContent= script;
                    }
                    else
                    {
                        workflowContext.Output["BashScript"] = "Bash Script Content is null";
                        return Outcomes("Failed");
                    }


                }

            }

            string code = "";
            bool isSuccess = true;
            try
            {
                var (standardOutput1, standardError1) = await Command.ReadAsync(bashBinPath, exeContent);
                if (string.IsNullOrEmpty(standardError1))
                {
                    code = standardOutput1;
                }
                else
                {
                    isSuccess = false;
                    code = standardError1;
                }
            }
            catch (Exception ex)
            {
                code = ex.Message;
                isSuccess = false;
            }

            workflowContext.Output["BashScript"] = _htmlHelper.Raw(_htmlHelper.Encode(code));
            if (isSuccess)
            {
                return Outcomes("Success");
            }
            else
            {
                return Outcomes("Failed");
            }

        }
    }
}
