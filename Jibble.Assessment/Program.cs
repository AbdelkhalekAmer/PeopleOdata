using System.CommandLine;

using Jibble.Assessment.ConsoleApp.Commands;
using Jibble.Assessment.Infrastracture.Repositories;
using Jibble.Assessment.Parsers;

namespace Jibble.Assessment.ConsoleApp;

internal class Program
{
    private static void Main(string[] args)
    {
        RootCommand application = new("People OData Service");

        JibbleConsole console = new();
        PersonParser parser = new();
        PersonRepository repository = new();

        application.AddCommand(new ListCommand(repository, console, parser));
        application.AddCommand(new GetPersonCommand(repository, console, parser));
        application.AddCommand(new CreatePersonCommand(repository, console, parser));
        application.AddCommand(new UpdatePersonCommand(repository, console, parser));

        application.Invoke(args);
    }
}