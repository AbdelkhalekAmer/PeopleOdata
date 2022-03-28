using System.Text;

using Jibble.Assessment.Core.Common;
using Jibble.Assessment.Core.Common.Interfaces;
using Jibble.Assessment.Core.Entities;

using Simple.OData.Client;

namespace Jibble.Assessment.Infrastracture.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly ODataClient _client;

    public PersonRepository(ODataClient client)
    {
        _client = client;
    }

    public Task CreatePersonAsync(Person person)
    {
        if (person.Gender is null)
            person.Gender = Gender.Unknown;

        if (person.FavoriteFeature is null)
            person.FavoriteFeature = Feature.Feature1;

        person.Features = new[]
        {
            Feature.Feature1,
            Feature.Feature2,
            Feature.Feature3,
            Feature.Feature4
        };

        person.Emails = new[] { "abdelkhalekamer@outlook.com" };

        return _client.For<Person>("People").Set(person).InsertEntryAsync();
    }

    public async Task<IEnumerable<Person>?> GetPeopleAsync(string? firstName, Gender? gender, Feature? favFeature)
    {
        StringBuilder query = new("People");

        if (!string.IsNullOrWhiteSpace(firstName) || gender is not null || favFeature is not null)
        {
            query = query.Append("?$filter=");

            if (!string.IsNullOrWhiteSpace(firstName))
                query = query.Append($"contains({nameof(Person.FirstName)}, '{firstName}')");

            if (gender is not null)
            {
                if (!string.IsNullOrWhiteSpace(firstName)) query.Append(" and ");
                query = query.Append($"{nameof(Person.Gender)} eq Trippin.PersonGender'{gender}'");
            }

            if (favFeature is not null)
            {
                if (!string.IsNullOrWhiteSpace(firstName) || gender is not null) query.Append(" and ");
                query = query.Append($"{nameof(Person.FavoriteFeature)} eq Trippin.Feature'{(Feature)favFeature}'");
            }
        }

        IEnumerable<IDictionary<string, object>>? oDataPeople = await _client.FindEntriesAsync(query.ToString());

        IEnumerable<Person>? people = oDataPeople?.Select(oDataPerson =>
            {
                Enum.TryParse(oDataPerson[nameof(Person.Gender)] as string ?? Gender.Unknown.ToString(), true, out Gender gender);

                Enum.TryParse(oDataPerson[nameof(Person.FavoriteFeature)] as string ?? Feature.None.ToString(), true, out Feature favFeature);

                return new Person()
                {
                    UserName = oDataPerson[nameof(Person.UserName)] as string,
                    FirstName = oDataPerson[nameof(Person.FirstName)] as string,
                    MiddleName = oDataPerson[nameof(Person.MiddleName)] as string,
                    LastName = oDataPerson[nameof(Person.LastName)] as string,
                    Gender = gender,
                    Age = oDataPerson[nameof(Person.Age)] as long?,
                    FavoriteFeature = favFeature
                };
            });

        return people;
    }

    public async Task<Person?> GetPersonAsync(string username)
    {
        IDictionary<string, object>? oDataPerson = (await _client.FindEntriesAsync($"People('{username}')")).FirstOrDefault();

        if (oDataPerson is null)
            return null;

        Enum.TryParse(oDataPerson[nameof(Person.Gender)] as string ?? Gender.Unknown.ToString(), true, out Gender gender);

        Enum.TryParse(oDataPerson[nameof(Person.FavoriteFeature)] as string ?? Feature.None.ToString(), true, out Feature favFeature);

        return new Person()
        {
            UserName = oDataPerson[nameof(Person.UserName)] as string,
            FirstName = oDataPerson[nameof(Person.FirstName)] as string,
            MiddleName = oDataPerson[nameof(Person.MiddleName)] as string,
            LastName = oDataPerson[nameof(Person.LastName)] as string,
            Gender = gender,
            Age = oDataPerson[nameof(Person.Age)] as long?,
            FavoriteFeature = favFeature
        };
    }

    public Task UpdatePersonAsync(Person person)
    {
        Dictionary<string, object> data = new();

        if (!string.IsNullOrWhiteSpace(person.FirstName))
            data.Add(nameof(Person.FirstName), person.FirstName);

        if (person.Gender is not null)
            data.Add(nameof(Person.Gender), person.Gender.ToString());

        if (person.FavoriteFeature is not null)
            data.Add(nameof(Person.FavoriteFeature), person.FavoriteFeature.ToString());

        return _client.For<Person>().Key(person.UserName).Set(data).UpdateEntryAsync();
    }
}