[CmdletBinding()]
param(
    [string]$WorkshopRepo = $(if ($env:WORKSHOP_REPO) {
        $env:WORKSHOP_REPO
    }
    else {
        Split-Path $PSScriptRoot -Parent
    })
)

$ErrorActionPreference = "Stop"
$demoRoot = $PSScriptRoot
$dotnet = Get-Command dotnet -ErrorAction Stop

Push-Location $demoRoot
try {
    Write-Host "SDK:"
    & $dotnet.Source --version

    Write-Host "`nBuilding the Razor Pages demo..."
    & $dotnet.Source build "$demoRoot\TodoWebApp\TodoWebApp.csproj" --configuration Release
    if ($LASTEXITCODE -ne 0) {
        throw "The Razor Pages demo failed to build."
    }

    Write-Host "`nBuilding the workshop starter solution..."
    & $dotnet.Source build "$WorkshopRepo\src\start\src\Contacts.sln" --configuration Release
    if ($LASTEXITCODE -ne 0) {
        throw "The workshop starter solution failed to build."
    }

    Write-Host "`nBuilding and testing the completed Contacts API..."
    & $dotnet.Source build "$WorkshopRepo\src\complete\Contacts.sln" --configuration Release
    if ($LASTEXITCODE -ne 0) {
        throw "The completed workshop solution failed to build."
    }

    & $dotnet.Source test "$WorkshopRepo\src\complete\Contacts.sln" --configuration Release --no-build
    if ($LASTEXITCODE -ne 0) {
        throw "The completed workshop tests failed."
    }

    Write-Host "`nAll demo assets are ready." -ForegroundColor Green
}
finally {
    Pop-Location
}
