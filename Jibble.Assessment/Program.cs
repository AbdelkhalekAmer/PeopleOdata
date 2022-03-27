using System.CommandLine;

using Jibble.Assessment.Commands;
using Jibble.Assessment.Infrastracture.Repositories;
using Jibble.Assessment.Parsers;

namespace Jibble.Assessment;

internal class Program
{
    private static void Main(string[] args)
    {
        RootCommand application = new("People OData Service");

        #region Application Dependencies
        JibbleConsole console = new();
        PersonParser parser = new();

        string rootServiceUri = "https://services.odata.org/TripPinRESTierService/(S(atq0tbnnufbk1mm0manwxy3i))";
        PersonRepository repository = new(new(new(rootServiceUri)));
        #endregion

        #region Commands
        application.AddCommand(new ListCommand(repository, console, parser));
        application.AddCommand(new GetPersonCommand(repository, console, parser));
        application.AddCommand(new CreatePersonCommand(repository, console, parser));
        application.AddCommand(new UpdatePersonCommand(repository, console, parser));
        #endregion

        application.Invoke(args);
    }
}