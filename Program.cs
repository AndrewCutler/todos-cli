// See https://aka.ms/new-console-template for more information
using Spectre.Console;

string? arg;

if (args.Length < 1)
{
    arg = null;
}

arg = args[0];

Switch.Run(arg);
// AnsiConsole.Write(args[0]);
