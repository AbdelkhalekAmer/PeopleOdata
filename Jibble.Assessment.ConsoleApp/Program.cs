using System.CommandLine;

namespace Jibble.Assessment.ConsoleApp;

internal class Program
{
    private static void Main(string[] args)
    {
        //args = "list --username 'test'".Split(' ');

        RootCommand application = new("People OData Service");

        foreach (Command command in Commands.Load()) application.Add(command);

        application.Invoke(args);
    }
}