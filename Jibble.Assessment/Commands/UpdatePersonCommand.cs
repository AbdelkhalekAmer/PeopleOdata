using System.CommandLine;

using Jibble.Assessment.Core.Common;
using Jibble.Assessment.Core.Common.Interfaces;
using Jibble.Assessment.Core.Entities;
using Jibble.Assessment.Parsers;

namespace Jibble.Assessment.ConsoleApp.Commands;

internal class UpdatePersonCommand : Command
{
    private readonly IPersonRepository _personRepository;

    public UpdatePersonCommand(IPersonRepository personRepository) : base("update", "update person's data.")
    {
        _personRepository = personRepository;

        #region Configure Command
        Argument<string> usernameArgument = new("username", "Username");

        AddArgument(usernameArgument);

        Option<string> firstNameOption = new("--first-name", "First Name");
        Option<string> genderOption = new("--gender", "Gender");
        Option<string> favFeatureOption = new("--fav-feature", "Favourite Feature");

        AddOption(firstNameOption);
        AddOption(genderOption);
        AddOption(favFeatureOption);

        System.CommandLine.Handler.SetHandler<string, string, string, string>(this, UpdatePerson, usernameArgument, firstNameOption, genderOption, favFeatureOption);
        #endregion
    }

    public void UpdatePerson(string username, string firstName, string gender, string favFeature)
    {
        Gender? parsedGender = PersonParser.ParseGender(gender);

        Feature? parsedFeature = PersonParser.ParseFeature(favFeature);

        _personRepository.UpdatePerson(new Person()
        {
            UserName = username,
            FirstName = firstName,
            Gender = parsedGender,
            FavoriteFeature = parsedFeature
        });
    }
}
