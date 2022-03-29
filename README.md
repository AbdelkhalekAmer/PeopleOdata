# People OData Console Application

*Build with DotNet 6 and run .exe file with one of the following commands*

Commands:
- List all people.

```PowerShell
    .\PeopleOdata.exe list
```
- Search with criterea.

```PowerShell
    .\PeopleOdata.exe list --username <username> --first-name <first-name> --gender <gender> --fav-feature <fav-feature>
```

- Get person data.

```PowerShell
    .\PeopleOdata.exe get <username>
```

- Create a new person.

```PowerShell
    .\PeopleOdata.exe create --username <username> --first-name <first-name> --gender <gender> --fav-feature <fav-feature>
```

- Update person's data.

```PowerShell
    .\PeopleOdata.exe update <username> --first-name <first-name> --gender <gender> --fav-feature <fav-feature>
```