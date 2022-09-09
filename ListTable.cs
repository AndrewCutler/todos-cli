using Spectre.Console;

public static class ListTable
{
    public static void Run()
    {
        var table = new Table();

        table.Border(TableBorder.Rounded);

        table.AddColumn(string.Empty);
        table.AddColumn(new TableColumn(new Markup("[blue]Todo[/]")).Centered());
        table.AddColumn(new TableColumn(new Markup("[blue]Status[/]")));
        table.AddColumn(new TableColumn(new Markup("[blue]Completed[/]")));

        string[] lines = System.IO.File.ReadAllLines(Constants.TodoFilePath);

        foreach (var current in lines.Select((line, index) => (line, index)))
        {
            var split = current.line.Split(Constants.Timestamp);
            if (split.Count() > 2)
            {
                throw new Exception("Invalidly formatted todo; cannot parse timestamp.");
            }

            var isComplete = current.line.StartsWith(Constants.CompleteMarker);

            var todo = split.FirstOrDefault() ?? throw new NullReferenceException("Null todo encountered.");
            var timestamp = split.Skip(1).FirstOrDefault() ?? string.Empty;

            table.AddRow(
                (current.index + 1).ToString(),
                todo.Replace(Constants.CompleteMarker, string.Empty),
                isComplete ? "[green]Done[/]" : "[yellow]In progress[/]",
                timestamp);
        }

        AnsiConsole.Write(table);
    }
}