using System.ComponentModel.DataAnnotations;

namespace YuSheng.OrchardCore.Workflow.Bash.Scripting.ViewModels
{
    public class ScriptTaskViewModel
    {
        [Required]
        public string BashBinPath { get; set; }
        [Required]
        public string BashFilePath { get; set; }

        [Required]
        public string Script { get; set; }
    }
}
