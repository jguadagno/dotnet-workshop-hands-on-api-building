# ASP.NET Core Web API Workshop

## Introduction

This repository contains an eight-hour, hands-on workshop for building a controller-based ASP.NET Core Web API over an existing SQLite database. The starter solution is in `src/start`; the reference implementation is in `src/complete`.

> [!IMPORTANT]
> The workshop currently targets .NET 11 RC 1. .NET 11 general availability is expected in November 2026. `global.json` pins the tested SDK and the workshop instructions include a GA refresh checklist.

## Prerequisites

* .NET SDK `11.0.100-rc.1.26425.128`
* Visual Studio 2026 Insiders with the **ASP.NET and web development** workload, or Visual Studio Code with C# Dev Kit, or JetBrains Rider
* Git
* A working internet connection for package restore

## Getting started

Clone this repository and verify the pinned SDK from the repository root:

```console
dotnet --version
```

Open [`src/start/src/Contacts.sln`](src/start/src/Contacts.sln) in your IDE.

## Workshop

Follow [`src/start/src/workshop-instructions.md`](src/start/src/workshop-instructions.md). The finished result should match [`src/complete`](src/complete).

## Presenter demos

The [`demos`](demos) folder contains a self-contained Razor Pages demo, Contacts API presenter runbooks, and a verification script:

```powershell
.\demos\verify-demos.ps1
```

The PowerPoint deck and speaker notes are maintained separately from this source repository.

## Repository layout

| Path | Purpose |
| --- | --- |
| `src/start` | Attendee starter solution, workshop instructions, and SQLite setup |
| `src/complete` | Completed reference implementation |
| `demos` | Presenter demo source and runbooks |

## License

This project is licensed under the [MIT License](LICENSE).
