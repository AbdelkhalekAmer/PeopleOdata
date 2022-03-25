using Jibble.Assessment.Core.Common;
using Jibble.Assessment.Core.Common.Interfaces;
using Jibble.Assessment.Core.Entities;

namespace Jibble.Assessment.Infrastracture.Repositories;

public class PersonRepository : IPersonRepository
{
    public Task<IEnumerable<Person>> GetPeopleAsync()
    {
        IEnumerable<Person> people = new[]
        {
            new Person()
            {
                UserName = "TeaMate",
                FirstName = "Abdelkhalek",
                Gender = Gender.Male.ToString(),
                FavoriteFeature = Feature.Feature1.ToString()
            }
        };

        return Task.FromResult(people);
    }
}