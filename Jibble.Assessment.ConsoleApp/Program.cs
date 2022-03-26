using System.CommandLine;

using Jibble.Assessment.ConsoleApp.Commands;
using Jibble.Assessment.Infrastracture.Repositories;

namespace Jibble.Assessment.ConsoleApp;

internal class Program
{
    private static Task Main(string[] args)
    {
        //args = "get 'russellwhyte'".Split(' ');

        RootCommand application = new("People OData Service");

        PersonRepository repository = new();

        application.AddCommand(new ListCommand(repository));
        application.AddCommand(new GetPersonCommand(repository));
        application.AddCommand(new CreatePersonCommand(repository));
        application.AddCommand(new UpdatePersonCommand(repository));

        return application.InvokeAsync(args);
    }
}