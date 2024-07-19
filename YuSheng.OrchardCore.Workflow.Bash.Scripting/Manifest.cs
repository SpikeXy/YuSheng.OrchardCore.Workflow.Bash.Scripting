using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "YuSheng OrchardCore Workflow Bash Scripting",
    Author = "spike",
    Website = "",
    Version = "0.0.1"
)]

[assembly: Feature(
    Id = "YuSheng OrchardCore Workflow Bash Scripting",
    Name = "YuSheng OrchardCore Workflow Bash Scripting",
    Description = "Provides bash scripting ",
    Dependencies = new[] { "OrchardCore.Workflows" },
    Category = "Workflows"
)]
