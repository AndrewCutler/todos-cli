using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Spectre.Console;
using Spectre.Console.Cli;

public class RemoveCommand : Command<RemoveCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "[TODO]")]
        [Description("Remove a todo item")]
        public int Index { get; set; }
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        try
        {
            var allLines = File.ReadAllLines(Constants.TodoFilePath);
            // Seems to have a bug
            var linesToRemove = allLines
                .Where((line, index) => index == settings.Index - 1);
            var linesToKeep = allLines
                .Except(linesToRemove);

            File.WriteAllLines(Constants.TodoFilePath, linesToKeep);

            AnsiConsole.Markup($"[green]Successfully removed \"{linesToRemove.FirstOrDefault()}\".[/]\n");

            return 0;
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]There was an error removing the todo item:\n[/][yellow]{ex.Message}[/]");

            return 1;
        }
    }
}