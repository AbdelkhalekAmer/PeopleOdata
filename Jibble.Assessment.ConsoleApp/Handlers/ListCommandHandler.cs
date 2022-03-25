using System.CommandLine.Invocation;
using System.Text.Json;

using Jibble.Assessment.Core.Common.Interfaces;
using Jibble.Assessment.Core.Entities;

namespace Jibble.Assessment.ConsoleApp.Handlers;

internal class ListCommandHandler : ICommandHandler
{
    private readonly IPersonRepository _personRepository;

    public ListCommandHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<int> InvokeAsync(InvocationContext context)
    {
        //foreach (var item in context.ParseResult.CommandResult.Children)
        //{
        //    var y = item.GetValueForOption(new Option<string>("--username", "Username"));
        //}
        //string? x = context.ParseResult.GetValueForOption(new Option<string>("--username", "Username"));
        //Console.WriteLine(x);

        IEnumerable<Person> people = await _personRepository.GetPeopleAsync();

        Console.WriteLine(JsonSerializer.Serialize(people));

        return 0;
    }
}
