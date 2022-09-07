using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Spectre.Console;
using Spectre.Console.Cli;

public class TodosCommand : Command<TodosCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandOption("-h|--help")]
        public int? Help { get; set; }
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        try
        {
            string text = System.IO.File.ReadAllText(Constants.TodoFilePath);
            System.Console.WriteLine("{0}", text);

            return 0;
        }
        catch
        {
            AnsiConsole.MarkupLine("[red]There was an error reading the todo list.[/]");

            return 1;
        }
    }
}