using System.CommandLine;

using Jibble.Assessment.Core.Common;
using Jibble.Assessment.Core.Common.Interfaces;
using Jibble.Assessment.Core.Entities;
using Jibble.Assessment.Parsers;

namespace Jibble.Assessment.ConsoleApp.Commands;

internal class CreatePersonCommand : Command
{
    private readonly IPersonRepository _personRepository;
    public CreatePersonCommand(IPersonRepository personRepository) : base("create", "create a new person.")
    {
        _personRepository = personRepository;

        #region Configure Command
        Option<string> usernameOption = new("--username", "Username");
        Option<string> firstNameOption = new("--first-name", "First Name");
        Option<string> genderOption = new("--gender", "Gender");
        Option<string> favFeatureOption = new("--fav-feature", "Favourite Feature");

        AddOption(usernameOption);
        AddOption(firstNameOption);
        AddOption(genderOption);
        AddOption(favFeatureOption);

        System.CommandLine.Handler.SetHandler<string, string, string, string>(this, CreatePerson, usernameOption, firstNameOption, genderOption, favFeatureOption);
        #endregion
    }

    public void CreatePerson(string username, string firstName, string gender, string favFeature)
    {
        string parsedUsername = PersonParser.ParseUsername(username);

        string? parsedFirstName = PersonParser.ParseFirstName(firstName);

        Gender? parsedGender = PersonParser.ParseGender(gender);

        Feature? parsedFeature = PersonParser.ParseFeature(favFeature);

        _personRepository.CreatePerson(new Person()
        {
            UserName = parsedUsername,
            FirstName = parsedFirstName,
            Gender = parsedGender,
            FavoriteFeature = parsedFeature
        });
    }
}