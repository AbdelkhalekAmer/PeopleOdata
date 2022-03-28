using Jibble.Assessment.Core.Common;

namespace Jibble.Assessment.Core.Entities;

public class Person
{
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public long? Age { get; set; }
    public Gender? Gender { get; set; }
    public IEnumerable<Feature>? Features { get; set; }
    public Feature? FavoriteFeature { get; set; }
    public IEnumerable<string>? Emails { get; set; }
}