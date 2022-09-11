using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Spectre.Console;
using Spectre.Console.Cli;

public class EditCommand : Command<EditCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "[TODO]")]
        [Description("Edit a todo item")]
        public int Index { get; set; }
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        try
        {
            var allLines = File.ReadAllLines(Constants.TodoFilePath);
            string? lineToEdit = allLines
                .Where((line, index) => index == settings.Index - 1)
                .FirstOrDefault();

            if (lineToEdit is null)
            {
                throw new NullReferenceException($"Cannot find todo with index {settings.Index}.");
            }

            if (Utility.IsComplete(lineToEdit))
            {
                AnsiConsole.Markup("[yellow]Todo is already completed and cannot be edited.\n[/]");

                return 0;
            }

            var todo = AnsiConsole.Prompt(
                new TextPrompt<string>($"Enter updated todo ([yellow]{lineToEdit}[/]):")
                .PromptStyle("blue")
                .Validate(todo =>
                {
                    return todo.Trim().Length switch
                    {
                        0 => ValidationResult.Error("[red]Update todo cannot be empty[/]"),
                        _ => ValidationResult.Success(),
                    };
                })
            );

            var updatedLines = allLines
                .Select((line, index) =>
                {
                    if (index == settings.Index - 1)
                    {
                        return todo ?? throw new NullReferenceException("Updated todo cannot be empty.");
                    }

                    return line;
                });

            File.WriteAllLines(Constants.TodoFilePath, updatedLines);

            AnsiConsole.Markup($"[green]Successfully edited todo item.[/]\n");

            return 0;
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]There was an error editing the todo item:\n[/][yellow]{ex.Message}[/]");

            return 1;
        }
    }
}