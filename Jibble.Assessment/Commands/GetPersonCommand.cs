using System.CommandLine;
using System.Text.Json;

using Jibble.Assessment.Core.Common.Interfaces;
using Jibble.Assessment.Core.Entities;

namespace Jibble.Assessment.ConsoleApp.Commands;

internal class GetPersonCommand : Command
{
    public GetPersonCommand(IPersonRepository personRepository) : base("get", "Get person data.")
    {
        Argument<string> usernameArgument = new("username", "Username");

        AddArgument(usernameArgument);

        System.CommandLine.Handler.SetHandler(this, (string username) =>
        {
            Person person = personRepository.GetPerson(username.Trim('\'').Trim('\"'));

            JsonSerializerOptions options = new()
            {
                WriteIndented = true
            };

            Console.WriteLine(JsonSerializer.Serialize(person, options));
        }, usernameArgument);
    }
}