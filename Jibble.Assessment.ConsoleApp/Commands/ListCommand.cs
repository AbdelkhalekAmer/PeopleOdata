using System.CommandLine;
using System.Text.Json;

using Jibble.Assessment.Core.Common;
using Jibble.Assessment.Core.Common.Interfaces;
using Jibble.Assessment.Core.Entities;

namespace Jibble.Assessment.ConsoleApp.Commands;

internal class ListCommand : Command
{
    public ListCommand(IPersonRepository personRepository) : base("list", "List all people.")
    {
        Option<string> firstNameOption = new("--first-name", "First Name");
        Option<string> genderOption = new("--gender", "Gender");
        Option<string> favFeatureOption = new("--fav-feature", "Favourite Feature");

        AddOption(firstNameOption);
        AddOption(genderOption);
        AddOption(favFeatureOption);

        System.CommandLine.Handler.SetHandler(this,
            async (string firstName, string gender, string favFeature) =>
            {
                Gender parsedGender = Gender.Unknown;

                if (!string.IsNullOrWhiteSpace(gender) && !Enum.TryParse(gender, true, out parsedGender))
                    throw new ArgumentException($"Invalid gender value, please use one of the following, {Gender.Male}, {Gender.Female} or leave it empty.", nameof(gender));

                Feature parsedFavFeature = Feature.None;

                if (!string.IsNullOrWhiteSpace(favFeature) && !Enum.TryParse(favFeature, true, out parsedFavFeature))
                    throw new ArgumentException($"Invalid favourite feature value, please use one of the following, {Feature.Feature1}, {Feature.Feature2}, {Feature.Feature3}, {Feature.Feature4} or leave it empty.", nameof(favFeature));

                IEnumerable<Person> people = await personRepository.GetPeopleAsync(firstName,
                   string.IsNullOrWhiteSpace(gender) ? null : parsedGender,
                    string.IsNullOrWhiteSpace(favFeature) ? null : parsedFavFeature);

                Console.WriteLine(JsonSerializer.Serialize(people));
            },
            firstNameOption,
            genderOption,
            favFeatureOption);
    }
}