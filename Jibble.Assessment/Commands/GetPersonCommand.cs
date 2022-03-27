using System.CommandLine;

using Jibble.Assessment.Core.Common.Interfaces;
using Jibble.Assessment.Core.Entities;
using Jibble.Assessment.Parsers;

namespace Jibble.Assessment.ConsoleApp.Commands;

internal class GetPersonCommand : Command
{
    private readonly IPersonRepository _personRepository;
    private readonly IConsole _console;
    private readonly PersonParser _parser;

    public GetPersonCommand(IPersonRepository personRepository, IConsole console, PersonParser parser)
        : base("get", "Get person data.")
    {
        #region Fields
        _personRepository = personRepository;
        _console = console;
        _parser = parser;
        #endregion

        #region Configure Command
        Argument<string> usernameArgument = new("username", "Username");

        AddArgument(usernameArgument);

        System.CommandLine.Handler.SetHandler<string>(this, GetPerson, usernameArgument);
        #endregion
    }

    public void GetPerson(string username)
    {
        try
        {
            string parsedUsername = _parser.ParseUsername(username);

            Person person = _personRepository.GetPerson(parsedUsername);

            _console.Write(person);
        }
        catch (Exception ex)
        {
            _console.WriteError(ex);
        }
    }
}