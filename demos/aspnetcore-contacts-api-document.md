# Demo: OpenAPI and Scalar

**Slides:** 104-107
**Duration:** 6-8 minutes

## Goal

Show that the OpenAPI document is an executable contract generated from the running application and that Scalar is a development-time client for exploring it.

## Before presenting

Start the completed API as described in [`aspnetcore-contacts-api.md`](aspnetcore-contacts-api.md).

## Demo flow

1. Open <https://localhost:7113/openapi/v1.json>.
   - Find `info.title`, `info.version`, and `info.description`.
   - Find `/api/Contacts/{id}`.
   - Expand the `200` and `404` responses.
   - Point out reusable schemas under `components`.
2. Return to `src\complete\Contacts.Api\Program.cs`.
   - `AddOpenApi` registers document generation.
   - The document transformer supplies workshop-specific metadata.
   - `MapOpenApi` and `MapScalarApiReference` are inside the Development check.
3. Open <https://localhost:7113/scalar/v1>.
   - Confirm the nine operations.
   - Open `GET /api/Contacts/{id}` and send `1`.
   - Send `99999999` to show `404`.
   - Send the invalid POST body from `src\complete\Contacts-Sample-Requests.http` and inspect the validation problem.
4. Open the POST operation.
   - Explain the `201` response schema.
   - Point out that the controller's `ProducesResponseType` metadata and XML comments shape the document.
5. If time permits, temporarily change an operation summary or response annotation, rebuild, and refresh the document to show that code and contract move together.

## Key message

OpenAPI is the machine-readable contract. Scalar renders that contract, but `.http` files and automated tests remain the repeatable verification tools.

## Recovery

No persistent changes are required. If code was edited during the optional step, undo only that edit and rebuild before the hands-on lab.
