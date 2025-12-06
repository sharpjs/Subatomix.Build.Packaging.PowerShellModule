# Subatomix.Build.Packaging.PowerShellModule

PowerShell module packaging support for .NET [SDK-style][sdk] projects.

## Status

[![Build](https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/workflows/Build/badge.svg)](https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/actions)
[![NuGet](https://img.shields.io/nuget/v/Subatomix.Build.Packaging.PowerShellModule.svg)](https://www.nuget.org/packages/Subatomix.Build.Packaging.PowerShellModule)
[![NuGet](https://img.shields.io/nuget/dt/Subatomix.Build.Packaging.PowerShellModule.svg)](https://www.nuget.org/packages/Subatomix.Build.Packaging.PowerShellModule)

In use by a few modules.

## Features

- Support for binary, script, and manifest modules.
- Module manifest (`.psd1`) generation from project properties.
- Custom module manifest generation from templates.
- Run and debug in Visual Studio or other IDE.
- Publish to the [PowerShell Gallery][psg] with `dotnet nuget push`.

## Usage

It's not difficult.  Add a couple project references and set some project
properties, and `dotnet pack` (or Visual Studio's Pack feature) will produce a
PowerShell module ready for publishing to the [PowerShell Gallery][psg].

See the [usage guide][ug] for full details.

A C# project file targeting PowerShell 7.4 or later might look like this:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="System.Management.Automation"
                      Version="7.4.0" PrivateAssets="All" />
    <PackageReference Include="Subatomix.Build.Packaging.PowerShellModule"
                      Version="2.0.0" PrivateAssets="All" />
  </ItemGroup>

  <PropertyGroup>
    <Description>My PowerShell module.</Description>
    <Authors>My Name</Authors>
    <Copyright>© My Name</Copyright>
    <PackageLicenseExpression>MIT</PackageLicenseExpression>
    <PackageProjectUrl>https://example.com</PackageProjectUrl>
  </PropertyGroup>

  <ItemGroup>
    <CmdletsToExport Include="New-ExampleThing" />
    <CmdletsToExport Include="Get-ExampleThing" />
    <CmdletsToExport Include="Remove-ExampleThing" />
  </ItemGroup>

</Project>
```

<!------------------------------------------------------------------------------------------------>

[psg]: https://www.powershellgallery.com/
[sdk]: https://docs.microsoft.com/en-us/dotnet/core/tools/csproj
[ug]:  https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/tree/main/doc/Guide.md

<!--
  Copyright Subatomix Research Inc.
  SPDX-License-Identifier: MIT
-->
