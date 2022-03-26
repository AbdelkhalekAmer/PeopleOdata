# Jibble Assessment

*Build with DotNet 6 and run .exe file with one of the following commands*

Commands:
- List all people.

```PowerShell
    .\Jibble.Assessment.exe list
```
- Search with criterea.

```PowerShell
    .\Jibble.Assessment.exe list --username <username> --first-name <first-name> --gender <gender> --fav-feature <fav-feature>
```

- Get person data.

```PowerShell
    .\Jibble.Assessment.exe get <username>
```

- Create a new person.

```PowerShell
    .\Jibble.Assessment.exe create --username <username> --first-name <first-name> --gender <gender> --fav-feature <fav-feature>
```

- Update person's data.

```PowerShell
    .\Jibble.Assessment.exe update <username> --first-name <first-name> --gender <gender> --fav-feature <fav-feature>
```