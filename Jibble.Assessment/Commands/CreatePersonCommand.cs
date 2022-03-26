using System.CommandLine;

using Jibble.Assessment.Core.Common;
using Jibble.Assessment.Core.Common.Interfaces;
using Jibble.Assessment.Core.Entities;

namespace Jibble.Assessment.ConsoleApp.Commands;

internal class CreatePersonCommand : Command
{
    public CreatePersonCommand(IPersonRepository personRepository) : base("create", "create a new person.")
    {
        Option<string> usernameOption = new("--username", "Username");
        Option<string> firstNameOption = new("--first-name", "First Name");
        Option<string> genderOption = new("--gender", "Gender");
        Option<string> favFeatureOption = new("--fav-feature", "Favourite Feature");

        AddOption(usernameOption);
        AddOption(firstNameOption);
        AddOption(genderOption);
        AddOption(favFeatureOption);

        System.CommandLine.Handler.SetHandler(this,
            (string username, string firstName, string gender, string favFeature) =>
            {
                Gender parsedGender = Gender.Unknown;

                if (!string.IsNullOrWhiteSpace(gender) && !Enum.TryParse(gender, true, out parsedGender))
                    throw new ArgumentException($"Invalid gender value, please use one of the following, {Gender.Male}, {Gender.Female} or leave it empty.", nameof(gender));

                Feature parsedFavFeature = Feature.None;

                if (!string.IsNullOrWhiteSpace(favFeature) && !Enum.TryParse(favFeature, true, out parsedFavFeature))
                    throw new ArgumentException($"Invalid favourite feature value, please use one of the following, {Feature.Feature1}, {Feature.Feature2}, {Feature.Feature3}, {Feature.Feature4} or leave it empty.", nameof(favFeature));

                personRepository.CreatePerson(new Person()
                {
                    UserName = username,
                    FirstName = firstName,
                    Gender = parsedGender,
                    FavoriteFeature = parsedFavFeature
                });
            },
            usernameOption,
            firstNameOption,
            genderOption,
            favFeatureOption);
    }
}