// See https://aka.ms/new-console-template for more information
using Spectre.Console.Cli;

var app = new CommandApp<TodosCommand>();
app.Configure(config =>
{
    config.AddCommand<AddCommand>("add")
        .WithDescription("Add todo items")
        .WithExample(new[] { "add", "\"Install Docker\"" })
        ;

#if DEBUG
    config.PropagateExceptions();
    config.ValidateExamples();
#endif
});

await app.RunAsync(args);
