using Jibble.Assessment.Core.Entities;

namespace Jibble.Assessment.Core.Common.Interfaces;

public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetPeopleAsync();
}