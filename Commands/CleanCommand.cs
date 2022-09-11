using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Spectre.Console;
using Spectre.Console.Cli;

public class CleanCommand : Command<CleanCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "[TODO]")]
        [Description("Removes all completed todos")]
        public int Index { get; set; }
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        try
        {
            string[] lines = System.IO.File.ReadAllLines(Constants.TodoFilePath);

            var linesToKeep = lines.Where((line) => !Utility.IsComplete(line));

            File.WriteAllLines(Constants.TodoFilePath, linesToKeep);

            return 0;
        }
        catch (Exception ex)
        {
            return Utility.HandleError(ex);
        }
    }
}