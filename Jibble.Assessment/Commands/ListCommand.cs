using System.CommandLine;
using System.Text.Json;

using Jibble.Assessment.Core.Common;
using Jibble.Assessment.Core.Common.Interfaces;
using Jibble.Assessment.Core.Entities;
using Jibble.Assessment.Parsers;

namespace Jibble.Assessment.ConsoleApp.Commands;

internal class ListCommand : Command
{
    private readonly IPersonRepository _personRepository;

    public ListCommand(IPersonRepository personRepository) : base("list", "List all people.")
    {
        _personRepository = personRepository;

        #region Configure Command
        Option<string> firstNameOption = new("--first-name", "First Name");
        firstNameOption.AddAlias("-fn");

        Option<string> genderOption = new("--gender", "Gender");
        genderOption.AddAlias("-g");

        Option<string> favFeatureOption = new("--fav-feature", "Favourite Feature");
        favFeatureOption.AddAlias("-fav");

        AddOption(firstNameOption);
        AddOption(genderOption);
        AddOption(favFeatureOption);

        System.CommandLine.Handler.SetHandler<string?, string?, string?>(this, GetPeople, firstNameOption, genderOption, favFeatureOption);
        #endregion
    }

    public void GetPeople(string? firstName, string? gender, string? favFeature)
    {
        string? parsedFirstName = PersonParser.ParseFirstName(firstName);

        Gender? parsedGender = PersonParser.ParseGender(gender);

        Feature? parsedFeature = PersonParser.ParseFeature(favFeature);

        IEnumerable<Person> people = _personRepository.GetPeople(parsedFirstName, parsedGender, parsedFeature);

        JsonSerializerOptions options = new() { WriteIndented = true };

        Console.WriteLine(JsonSerializer.Serialize(people, options));
    }
}