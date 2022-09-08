﻿using Spectre.Console.Cli;

var app = new CommandApp<TodosCommand>();
app.Configure(config =>
{
    config.AddCommand<AddCommand>("add")
        .WithAlias("-a")
        .WithDescription("Add todo items")
        .WithExample(new[] { "add", "\"Install Docker\"" })
        ;
    config.AddCommand<CompleteCommand>("complete")
        .WithAlias("-c")
        .WithDescription("Mark a todo as complete, by index")
        .WithExample(new[] { "complete", "2" })
        ;

#if DEBUG
    config.PropagateExceptions();
    config.ValidateExamples();
#endif
});

await app.RunAsync(args);
