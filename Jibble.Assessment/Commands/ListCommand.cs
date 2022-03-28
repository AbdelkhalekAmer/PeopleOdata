using System.CommandLine;

using Jibble.Assessment.Core.Common;
using Jibble.Assessment.Core.Common.Interfaces;
using Jibble.Assessment.Core.Entities;
using Jibble.Assessment.Parsers;

namespace Jibble.Assessment.Commands;

internal class ListCommand : Command
{
    private readonly IPersonRepository _personRepository;
    private readonly IConsole _console;
    private readonly PersonParser _parser;

    public ListCommand(IPersonRepository personRepository, IConsole console, PersonParser parser)
        : base("list", "List all people.")
    {
        #region Fields
        _personRepository = personRepository;
        _console = console;
        _parser = parser;
        #endregion

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

        System.CommandLine.Handler.SetHandler<string?, string?, string?>(this, GetPeopleAsync, firstNameOption, genderOption, favFeatureOption);
        #endregion
    }

    public async Task GetPeopleAsync(string? firstName, string? gender, string? favFeature)
    {
        try
        {
            string? parsedFirstName = _parser.ParseFirstName(firstName);

            Gender? parsedGender = _parser.ParseGender(gender);

            Feature? parsedFeature = _parser.ParseFeature(favFeature);

            IEnumerable<Person>? people = await _personRepository.GetPeopleAsync(parsedFirstName, parsedGender, parsedFeature);

            _console.Write(people);
        }
        catch (Exception ex)
        {
            _console.WriteError(ex);
        }
    }
}