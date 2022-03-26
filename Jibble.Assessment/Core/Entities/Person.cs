using Jibble.Assessment.Core.Common;

namespace Jibble.Assessment.Core.Entities;

public class Person
{
    public string UserName { get; set; }
    public string? FirstName { get; set; }
    public Gender? Gender { get; set; }
    public Feature? FavoriteFeature { get; set; }
}