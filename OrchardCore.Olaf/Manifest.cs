using OrchardCore.Modules.Manifest;

[assembly: Module(
    Author = "Arcstone Co.",
    Category = "workflows",
    Description = "I'm Olaf the snowman",
    Name = "Olaf",
    Version = "0.0.1",
    Website = "https://arcstone.co",
        Dependencies = new[] { "OrchardCore.Workflows" }
)]

[assembly: Feature(
    Id = "OrchardCore.Olfa.Dummy",
    Name = "Dummy Event Workflow Activities",
    Description = "Event that is triggered almost immediately.",
    Dependencies = new[] { "OrchardCore.Workflows" },
    Category = "Workflows"
)]

[assembly: Feature(
    Id = "OrchardCore.Olfa.Delay",
    Name = "Delay Event Workflow Activities",
    Description = "Event that is triggered based on the passed in date time.",
    Dependencies = new[] { "OrchardCore.Workflows" },
    Category = "Workflows"
)]

[assembly: Feature(
    Id = "OrchardCore.Olfa.ExpressionValidator",
    Name = "Validate Expression Tasks",
    Description = "Validate variables using lambda expressions",
    Dependencies = new[] { "OrchardCore.Workflows" },
    Category = "Workflows"
)]
