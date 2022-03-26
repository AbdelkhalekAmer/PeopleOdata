﻿using System.CommandLine;

using Jibble.Assessment.ConsoleApp.Commands;
using Jibble.Assessment.Infrastracture.Repositories;

namespace Jibble.Assessment.ConsoleApp;

internal class Program
{
    private static void Main(string[] args)
    {
        //args = "list --gender female".Split(' ');
        //args = "list --first-name 'C'".Split(' ');
        //args = "get 'ursulabright'".Split(' ');

        RootCommand application = new("People OData Service");

        PersonRepository repository = new();

        application.AddCommand(new ListCommand(repository));
        application.AddCommand(new GetPersonCommand(repository));
        application.AddCommand(new CreatePersonCommand(repository));
        application.AddCommand(new UpdatePersonCommand(repository));

        application.Invoke(args);
    }
}