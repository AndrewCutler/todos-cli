using Newtonsoft.Json;
using Spectre.Console;

public static class Utility
{
    public static void ListTable()
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
                isComplete ? "[green]Done[/]" : "[yellow]Pending[/]",
                timestamp);
        }

        AnsiConsole.Write(table);
    }

    public static Settings Settings()
    {
        using StreamReader r = new StreamReader(Constants.SettingsPath);
        string json = r.ReadToEnd();
        Settings? settings = JsonConvert.DeserializeObject<Settings>(json);

        return settings ?? new Settings();
    }

    public static int HandleError(Exception ex)
    {
        AnsiConsole.MarkupLine($"[red]There was an error processing the request:\n[/][yellow]{ex.Message}[/]");

        return 1;
    }

    public static bool IsComplete(string todo) => todo.StartsWith(Constants.CompleteMarker);
}