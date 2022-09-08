using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Spectre.Console;
using Spectre.Console.Cli;

public class CompleteCommand : Command<CompleteCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "[TODO]")]
        [Description("Marks the specified todo as complete")]
        public int Index { get; set; }
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        try
        {
            string[] lines = System.IO.File.ReadAllLines(Constants.TodoFilePath);

            var lineToComplete = lines[settings.Index - 1];

            // TODO: implement actual functionality
            AnsiConsole.Markup(lineToComplete);

            return 0;
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]There was an error marking the todo item as complete:\n[/][yellow]{ex.Message}[/]");

            return 1;
        }
    }
}