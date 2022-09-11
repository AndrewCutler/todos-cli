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

            if (Utility.IsComplete(lineToComplete))
            {
                AnsiConsole.MarkupLine("[yellow]Todo already completed.[/]");

                return 0;
            }

            var timestamp = $"{Constants.Timestamp}{DateTime.Now.ToString()}";

            lines[settings.Index - 1] = $"{Constants.CompleteMarker}{lineToComplete}{timestamp}";
            File.WriteAllLines(Constants.TodoFilePath, lines);

            return 0;
        }
        catch (Exception ex)
        {
            return Utility.HandleError(ex);
        }
    }
}