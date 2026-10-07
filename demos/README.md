# .NET Workshop Demos

These presenter demos support the PowerPoint deck and are designed to be repeatable without changing the attendee starter solution.

| Demo                 | Asset                                   | Purpose                                                                            |
| -------------------- | --------------------------------------- | ---------------------------------------------------------------------------------- |
| ASP.NET Core web app | `TodoWebApp` and `aspnetcore-webapp.md` | Show Razor Pages, dependency injection, routing, model binding, and form handlers. |
| Controller API       | `aspnetcore-contacts-api.md`            | Build the Contacts API from the workshop starter solution.                         |
| OpenAPI and Scalar   | `aspnetcore-contacts-api-document.md`   | Inspect and exercise the generated API contract.                                   |

Run `.\verify-demos.ps1` before presenting. The script builds the Todo app and both workshop solutions.
The local `global.json` pins the same .NET 11 RC 1 SDK used by the workshop repository.

By default, the scripts resolve the workshop repository from the parent of this `demos` folder.

Override that location with the `WORKSHOP_REPO` environment variable:

```powershell
$env:WORKSHOP_REPO = "C:\path\to\dotnet-workshop-hands-on-api-building"
```
