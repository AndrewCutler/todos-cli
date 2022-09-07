using Spectre.Console;

public static class Switch
{
    public static void Run(string? arg)
    {
        using StreamWriter file = new(Constants.TodoFilePath, append: true);
        Action? command = arg switch
        {
            "-a" or "--add" => () => file.WriteLine("code"),
            "-h" or "--help" => () => file.WriteLine("Help section"),
            null or _ => () => AnsiConsole.Write(arg ?? string.Empty),
        };

        command();
    }
}