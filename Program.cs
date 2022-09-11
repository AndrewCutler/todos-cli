using Spectre.Console.Cli;

var app = new CommandApp<TodosCommand>();

app.Configure(config =>
{
    config.AddCommand<AddCommand>("add")
        .WithAlias("a")
        .WithDescription("Add todo items")
        .WithExample(new[] { "add", "\"Install Docker\"" })
        ;

    config.AddCommand<CompleteCommand>("complete")
        .WithAlias("c")
        .WithDescription("Mark a todo as complete, by index")
        .WithExample(new[] { "complete", "2" })
        ;

    config.AddCommand<RemoveCommand>("remove")
        .WithAlias("r")
        .WithDescription("Remove a todo item by index")
        .WithExample(new[] { "remove", "2" })
        ;

    config.AddCommand<CleanCommand>("clean")
        .WithAlias("l")
        .WithDescription("Remove all completed todos")
        .WithExample(new[] { "clean" })
        ;

    config.AddCommand<EditCommand>("edit")
        .WithAlias("e")
        .WithDescription("Edit an existing todo")
        .WithExample(new[] { "edit", "2" })
        ;

#if DEBUG
    config.PropagateExceptions();
    config.ValidateExamples();
#endif
});

await app.RunAsync(args);
