using System.CommandLine;
using System.Text.Json;

using Jibble.Assessment.Core.Common.Interfaces;
using Jibble.Assessment.Core.Entities;
using Jibble.Assessment.Parsers;

namespace Jibble.Assessment.ConsoleApp.Commands;

internal class GetPersonCommand : Command
{
    private readonly IPersonRepository _personRepository;

    public GetPersonCommand(IPersonRepository personRepository) : base("get", "Get person data.")
    {
        _personRepository = personRepository;

        #region Configure Command
        Argument<string> usernameArgument = new("username", "Username");

        AddArgument(usernameArgument);

        System.CommandLine.Handler.SetHandler<string>(this, GetPerson, usernameArgument);
        #endregion
    }

    public void GetPerson(string username)
    {
        string parsedUsername = PersonParser.ParseUsername(username);

        Person person = _personRepository.GetPerson(parsedUsername);

        JsonSerializerOptions options = new() { WriteIndented = true };

        Console.WriteLine(JsonSerializer.Serialize(person, options));
    }
}