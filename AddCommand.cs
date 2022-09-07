using System.Diagnostics.CodeAnalysis;
using Spectre.Console;
using Spectre.Console.Cli;

public class AddCommand : Command<AddCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "[TODO]")]
        public string? Add { get; set; }
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        try
        {
            using StreamWriter file = new(Constants.TodoFilePath, append: true);
            // TODO: allow escaping commas
            foreach (var line in settings.Add?.Split(",") ?? new string[] { })
            {
                file.WriteLine(line);
            }
            AnsiConsole.MarkupLine($"[green]Item added.[/]");

            return 0;
        }
        catch
        {
            AnsiConsole.MarkupLine("[red]There was an error adding the todo item.[/]");

            return 1;
        }
    }
}