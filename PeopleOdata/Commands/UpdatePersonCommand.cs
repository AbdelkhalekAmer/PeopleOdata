using System.CommandLine;

using Jibble.Assessment.Core.Common;
using Jibble.Assessment.Core.Common.Interfaces;
using Jibble.Assessment.Parsers;

namespace Jibble.Assessment.Commands;

internal class UpdatePersonCommand : Command
{
    private readonly IPersonRepository _personRepository;
    private readonly IConsole _console;
    private readonly PersonParser _parser;

    public UpdatePersonCommand(IPersonRepository personRepository, IConsole console, PersonParser parser)
        : base("update", "update person's data.")
    {
        #region Fields
        _personRepository = personRepository;
        _console = console;
        _parser = parser;
        #endregion

        #region Configure Command
        Argument<string> usernameArgument = new("username", "Username");

        AddArgument(usernameArgument);

        Option<string> firstNameOption = new("--first-name", "First Name");
        Option<string> genderOption = new("--gender", "Gender");
        Option<string> favFeatureOption = new("--fav-feature", "Favourite Feature");

        AddOption(firstNameOption);
        AddOption(genderOption);
        AddOption(favFeatureOption);

        System.CommandLine.Handler.SetHandler<string, string, string, string>(this, UpdatePersonAsync, usernameArgument, firstNameOption, genderOption, favFeatureOption);
        #endregion
    }

    public async Task UpdatePersonAsync(string username, string firstName, string gender, string favFeature)
    {
        try
        {
            string parsedUsername = _parser.ParseUsername(username);

            string? parsedFirstName = _parser.ParseFirstName(firstName);

            Gender? parsedGender = _parser.ParseGender(gender);

            Feature? parsedFeature = _parser.ParseFeature(favFeature);

            await _personRepository.UpdatePersonAsync(new()
            {
                UserName = parsedUsername,
                FirstName = parsedFirstName,
                Gender = parsedGender,
                FavoriteFeature = parsedFeature
            });

            _console.WriteLine($"{parsedUsername} is updated successfully.");
        }
        catch (Exception ex)
        {
            _console.WriteError(ex);
        }
    }
}