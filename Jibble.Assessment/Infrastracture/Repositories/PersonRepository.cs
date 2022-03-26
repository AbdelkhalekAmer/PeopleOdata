﻿using Jibble.Assessment.Core.Common;
using Jibble.Assessment.Core.Common.Interfaces;
using Jibble.Assessment.Core.Entities;

using Microsoft.OData.Client;

namespace Jibble.Assessment.Infrastracture.Repositories;

public class PersonRepository : IPersonRepository
{
    private static Uri _rootUri = new("https://services.odata.org/TripPinRESTierService");
    private static Trippin.Container _container = new(_rootUri);

    public void CreatePerson(Person person)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Person> GetPeople(string? firstName, Gender? gender, Feature? favFeature)
    {
        DataServiceQuery<Trippin.Person> query = _container.People;

        if (!string.IsNullOrWhiteSpace(firstName))
            query.AddQueryOption("$filter", $"contains({nameof(Trippin.Person.FirstName)}, '{firstName}')");

        if (gender is not null)
            query.AddQueryOption("$filter",
                $"{nameof(Trippin.Person.Gender)} eq Microsoft.OData.Service.Sample.TrippinInMemory.Models.PersonGender'{(Trippin.PersonGender)gender}'");

        if (favFeature is not null)
            query.AddQueryOption("$filter",
                $"{nameof(Trippin.Person.FavoriteFeature)} eq Microsoft.OData.Service.Sample.TrippinInMemory.Models.Feature'{(Trippin.Feature)favFeature}'");

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
        Trippin.Person? person = _container.People.AddQueryOption("$filter", $"{nameof(Trippin.Person.UserName)} eq {username}").First();

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
        throw new NotImplementedException();
    }
}