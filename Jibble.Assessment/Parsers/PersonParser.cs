using System.Text.Json;

using Jibble.Assessment.Core.Common;

namespace Jibble.Assessment.Parsers;

internal static class PersonParser
{
    public static string ParseUsername(string username) => username.Trim('\'').Trim('\"');

    public static Gender? ParseGender(string? gender)
    {
        gender = gender?.Trim('\'').Trim('\"');

        Gender parsedGender = Gender.Unknown;

        if (!string.IsNullOrWhiteSpace(gender) && !Enum.TryParse(gender, true, out parsedGender))
        {
            string message = $"Invalid value, please use: {JsonSerializer.Serialize(Enum.GetNames<Gender>())} or leave it empty.";
            throw new ArgumentException(message, nameof(gender));
        }

        return string.IsNullOrWhiteSpace(gender) ? null : parsedGender;
    }

    public static Feature? ParseFeature(string? feature)
    {
        feature = feature?.Trim('\'').Trim('\"');

        Feature parsedFeature = Feature.None;

        if (!string.IsNullOrWhiteSpace(feature) && !Enum.TryParse(feature, true, out parsedFeature))
        {
            string message = $"Invalid value, please use: {JsonSerializer.Serialize(Enum.GetNames<Feature>())} or leave it empty.";
            throw new ArgumentException(message, nameof(feature));
        }

        return string.IsNullOrWhiteSpace(feature) ? null : parsedFeature;
    }
}