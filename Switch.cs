using Spectre.Console;

public static class Switch
{
    public static void Run(string? arg)
    {
        Action? command = arg switch
        {
            "-a" or "--add" => () => AnsiConsole.Write("list em"),
            null or _ => () => AnsiConsole.Write(arg ?? string.Empty),
        };

        command();
    }
}