// See https://aka.ms/new-console-template for more information
using Spectre.Console;

string? arg = args.Length < 1 ? null : args[0];
Switch.Run(arg);

string text = System.IO.File.ReadAllText(Constants.TodoFilePath);
System.Console.WriteLine("{0}", text);