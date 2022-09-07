// See https://aka.ms/new-console-template for more information
using Spectre.Console;
using Spectre.Console.Cli;

// string? @switch = args.Length < 1 ? null : args[0];
// Switch.Run(@switch);

var app = new CommandApp<TodosCommand>();
app.Configure(config =>
{
    config.AddCommand<AddCommand>("add")
        // .WithDescription("Add todo items")
        // .WithExample(new[] { "add", "\"Install Docker\"" })
        ;

#if DEBUG
    config.PropagateExceptions();
    config.ValidateExamples();
#endif
});

await app.RunAsync(args);

string text = System.IO.File.ReadAllText(Constants.TodoFilePath);
System.Console.WriteLine("Contents of file: {0}", text);
