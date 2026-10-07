# Demo: Build the Contacts Controller API

**Slides:** 93-103
**Duration:** 12-15 minutes

## Goal

Show the controller request flow, dependency injection, accurate HTTP results, and the completed Contacts API without modifying the attendee starter during the presentation.

## Before presenting

From the repository root:

```powershell
cd src\complete
dotnet build Contacts.sln
dotnet test Contacts.sln --no-build
dotnet run --project Contacts.Api --launch-profile Contacts.Api
```

Open:

- Scalar: <https://localhost:7113/scalar/v1>
- OpenAPI JSON: <https://localhost:7113/openapi/v1.json>

## Demo flow

1. Compare the two solution layouts:
   - `src\start\src\Contacts.sln` contains the domain, data, logic, and tests.
   - `src\complete\Contacts.sln` adds `Contacts.Api`.
2. Open `src\complete\Contacts.Api\Program.cs`.
   - Read the SQLite connection string.
   - Register `ContactContext` and the data, repository, and manager abstractions as scoped services.
   - Add controllers, problem details, and OpenAPI.
   - Map OpenAPI and Scalar only in Development.
3. Open `src\complete\Contacts.Api\Controllers\ContactsController.cs`.
   - Show `[ApiController]` and the controller route.
   - Show primary-constructor injection.
   - Walk through `GetContact`: `200` when found and `404` when missing.
   - Walk through `SaveContact`: `CreatedAtAction` returns `201` and a `Location` header.
   - Walk through `DeleteContact`: `204` or `404`.
   - Show the nested phone and address routes.
4. Open `src\complete\Contacts-Sample-Requests.http`.
   - Run the existing-contact and missing-contact requests.
   - Run the named create request.
   - Follow the returned `Location`.
   - Run the delete-created-contact request, which reuses the response ID.
   - Run the invalid request and inspect the RFC 9457 validation response.
5. Return to the controller and connect each response to the contract shown by the request output.

## Key message

The controller owns HTTP concerns. Application logic stays behind `IContactManager`, persistence stays behind repository and data-store abstractions, and the returned HTTP status accurately describes the outcome.

## Recovery

The create/delete sequence cleans up its own contact. If the demo is interrupted after create, run this from the repository root:

```powershell
Copy-Item .\src\start\sqlite\contacts.db `
    .\src\complete\Contacts.Api\contacts.db -Force
```
