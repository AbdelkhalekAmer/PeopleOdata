using System.CommandLine;

using Jibble.Assessment.ConsoleApp.Handlers;
using Jibble.Assessment.Infrastracture.Repositories;

namespace Jibble.Assessment.ConsoleApp;

internal static class Commands
{
    internal static IEnumerable<Command> Load()
    {
        Command listCommand = new("list", "List all people.");

        listCommand.AddOption(new(new[] { "u", "username" }, "Username"));
        listCommand.AddOption(new("first-name", "First Name"));
        listCommand.AddOption(new("gender", "Gender"));
        listCommand.AddOption(new("fav-feature", "Favourite Feature"));

        listCommand.Handler = new ListCommandHandler(new PersonRepository());

        return new[] { listCommand };
    }
}