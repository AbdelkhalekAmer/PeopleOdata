using Jibble.Assessment.Core.Entities;

namespace Jibble.Assessment.Core.Common.Interfaces;

public interface IPersonRepository
{
    Task<IEnumerable<Person>?> GetPeopleAsync(string? firstName, Gender? gender, Feature? favFeature);
    Task<Person?> GetPersonAsync(string username);
    Task CreatePersonAsync(Person person);
    Task UpdatePersonAsync(Person person);
}