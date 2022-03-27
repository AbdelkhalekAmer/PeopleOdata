using Jibble.Assessment.Core.Common;
using Jibble.Assessment.Core.Common.Interfaces;
using Jibble.Assessment.Core.Entities;

using Microsoft.OData.Client;

namespace Jibble.Assessment.Infrastracture.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly Trippin.Container _container;

    public PersonRepository(Trippin.Container container)
    {
        _container = container;
    }

    public void CreatePerson(Person person)
    {
        Trippin.Person oDataPerson = new()
        {
            UserName = person.UserName,
            FirstName = person.FirstName ?? "N/A",
            Gender = (Trippin.PersonGender)(person.Gender ?? Gender.Unknown),
            FavoriteFeature = (Trippin.Feature)(person.FavoriteFeature ?? Feature.Feature1)
        };

        _container.AddToPeople(oDataPerson);

        _container.SaveChanges();
    }

    public IEnumerable<Person> GetPeople(string? firstName, Gender? gender, Feature? favFeature)
    {
        DataServiceQuery<Trippin.Person> query = _container.People;

        if (!string.IsNullOrWhiteSpace(firstName))
            query = query.AddQueryOption("$filter",
                $"contains({nameof(Trippin.Person.FirstName)}, '{firstName}')");

        if (gender is not null)
            query = query.AddQueryOption("$filter",
                $"{nameof(Trippin.Person.Gender)} eq Trippin.PersonGender'{(Trippin.PersonGender)gender}'");

        if (favFeature is not null)
            query = query.AddQueryOption("$filter",
                $"{nameof(Trippin.Person.FavoriteFeature)} eq Trippin.Feature'{(Trippin.Feature)favFeature}'");

        Trippin.Person[] oDataPeople = query.ToArray();

        IEnumerable<Person> people = oDataPeople.Select(person => new Person()
        {
            UserName = person.UserName,
            FirstName = person.FirstName,
            Gender = (Gender)person.Gender,
            FavoriteFeature = (Feature)person.FavoriteFeature
        });

        return people;
    }

    public Person GetPerson(string username)
    {
        Trippin.Person? person = _container.People.AddQueryOption("$filter", $"{nameof(Trippin.Person.UserName)} eq '{username}'").First();

        return new Person()
        {
            UserName = person.UserName,
            FirstName = person.FirstName,
            Gender = (Gender)person.Gender,
            FavoriteFeature = (Feature)person.FavoriteFeature
        };
    }

    public void UpdatePerson(Person person)
    {
        Trippin.Person? oDataPerson = _container.People.AddQueryOption("$filter", $"{nameof(Trippin.Person.UserName)} eq '{person.UserName}'").First();

        if (oDataPerson is null)
            throw new InvalidOperationException($"Unable to find a person with {nameof(Trippin.Person.UserName)} '{person.UserName}'");

        if (!string.IsNullOrWhiteSpace(person.FirstName) && oDataPerson.FirstName != person.FirstName)
            oDataPerson.FirstName = person.FirstName;

        if (person.Gender is not null && oDataPerson.Gender != (Trippin.PersonGender)person.Gender)
            oDataPerson.Gender = (Trippin.PersonGender)person.Gender;

        if (person.FavoriteFeature is not null && oDataPerson.FavoriteFeature != (Trippin.Feature)person.FavoriteFeature)
            oDataPerson.FavoriteFeature = (Trippin.Feature)person.FavoriteFeature;

        _container.UpdateObject(oDataPerson);

        _container.SaveChanges();
    }
}