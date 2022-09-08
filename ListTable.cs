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
            var isComplete = new Random().Next(0, 2) == 1;
            table.AddRow(
                (current.index + 1).ToString(),
                current.line,
                isComplete ? "[green]Done[/]" : "[yellow]In progress[/]",
                isComplete ? DateTime.Now.ToString() : string.Empty);
        }

        AnsiConsole.Write(table);
    }
}