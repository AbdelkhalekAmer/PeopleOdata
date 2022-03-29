using System.CommandLine;

using Jibble.Assessment.Core.Common;
using Jibble.Assessment.Core.Common.Interfaces;
using Jibble.Assessment.Parsers;

namespace Jibble.Assessment.Commands;

internal class CreatePersonCommand : Command
{
    private readonly IPersonRepository _personRepository;
    private readonly IConsole _console;
    private readonly PersonParser _parser;

    public CreatePersonCommand(IPersonRepository personRepository, IConsole console, PersonParser parser)
        : base("create", "create a new person.")
    {
        #region Fields
        _personRepository = personRepository;
        _console = console;
        _parser = parser;
        #endregion

        #region Configure Command
        Option<string> usernameOption = new("--username", "Username");
        Option<string> firstNameOption = new("--first-name", "First Name");
        Option<string> genderOption = new("--gender", "Gender");
        Option<string> favFeatureOption = new("--fav-feature", "Favourite Feature");

        AddOption(usernameOption);
        AddOption(firstNameOption);
        AddOption(genderOption);
        AddOption(favFeatureOption);

        System.CommandLine.Handler.SetHandler<string, string, string, string>(this, CreatePersonAsync, usernameOption, firstNameOption, genderOption, favFeatureOption);
        #endregion
    }

    public async Task CreatePersonAsync(string username, string firstName, string gender, string favFeature)
    {
        try
        {
            string parsedUsername = _parser.ParseUsername(username);

            string? parsedFirstName = _parser.ParseFirstName(firstName);

            Gender? parsedGender = _parser.ParseGender(gender);

            Feature? parsedFeature = _parser.ParseFeature(favFeature);

            await _personRepository.CreatePersonAsync(new()
            {
                UserName = parsedUsername,
                FirstName = parsedFirstName,
                Gender = parsedGender,
                FavoriteFeature = parsedFeature
            });

            _console.WriteLine($"{parsedUsername} is created successfully.");
        }
        catch (Exception ex)
        {
            _console.WriteError(ex);
        }
    }
}