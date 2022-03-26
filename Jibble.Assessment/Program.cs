using System.CommandLine;

using Jibble.Assessment.ConsoleApp.Commands;
using Jibble.Assessment.Infrastracture.Repositories;

namespace Jibble.Assessment.ConsoleApp;

internal class Program
{
    private static void Main(string[] args)
    {
        RootCommand application = new("People OData Service");

        PersonRepository repository = new();

        application.AddCommand(new ListCommand(repository));
        application.AddCommand(new GetPersonCommand(repository));
        application.AddCommand(new CreatePersonCommand(repository));
        application.AddCommand(new UpdatePersonCommand(repository));

        application.Invoke(args);
    }
}