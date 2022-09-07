using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Spectre.Console;
using Spectre.Console.Cli;

public class TodosCommand : Command<TodosCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandOption("-h|--help")]
        public bool Help { get; init; }
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        try
        {
            if (settings.Help)
            {
                AnsiConsole.Markup("[blue]Help section[/]");
            }
            else
            {
                string text = System.IO.File.ReadAllText(Constants.TodoFilePath);
                System.Console.WriteLine("Current todos: {0}", text);
            }

            return 0;
        }
        catch
        {
            AnsiConsole.MarkupLine("[red]There was an error reading the todo list.[/]");

            return 1;
        }
    }
}