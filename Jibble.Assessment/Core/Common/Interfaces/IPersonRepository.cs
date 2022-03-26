using Jibble.Assessment.Core.Entities;

namespace Jibble.Assessment.Core.Common.Interfaces;

public interface IPersonRepository
{
    IEnumerable<Person> GetPeople(string? firstName, Gender? gender, Feature? favFeature);
    Person GetPerson(string username);
    void CreatePerson(Person person);
    void UpdatePerson(Person person);
}