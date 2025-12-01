# Subatomix.Build.Packaging.PowerShellModule

PowerShell module packaging support for .NET
[SDK-style](https://docs.microsoft.com/en-us/dotnet/core/tools/csproj)
projects.

## Status

[![Build](https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/workflows/Build/badge.svg)](https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/actions)
[![NuGet](https://img.shields.io/nuget/v/Subatomix.Build.Packaging.PowerShellModule.svg)](https://www.nuget.org/packages/Subatomix.Build.Packaging.PowerShellModule)
[![NuGet](https://img.shields.io/nuget/dt/Subatomix.Build.Packaging.PowerShellModule.svg)](https://www.nuget.org/packages/Subatomix.Build.Packaging.PowerShellModule)

In use by a handful of modules.

## Features

- Support for script, binary, or mixed modules
- Module manifest templates with `{VersionPrefix}`, `{VersionSuffix}`, and `{Copyright}` placeholders
- Run and debug with F5 in Visual Studio
- Support for automated build, test, and publish

## Usage

Just add a reference to this package.  Now `dotnet pack` and Visual Studio Pack
will produce a PowerShell module.  Here is a minimal example `.csproj` file:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <CopyLocalLockFileAssemblies>true</CopyLocalLockFileAssemblies>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="System.Management.Automation"
                      Version="7.4.0" PrivateAssets="All" />
    <PackageReference Include="Subatomix.Build.Packaging.PowerShellModule"
                      Version="1.1.1" PrivateAssets="All" />
  </ItemGroup>

  <ItemGroup>
    <Content Include="$(PowerShellItemIncludes)"
             Exclude="$(DefaultItemExcludes);$(DefaultExcludesInProjectFolder)" />
  </ItemGroup>

</Project>
```

For a simple example, see this repository's
[test project](https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/tree/main/test/current).
It can serve as a template for new PowerShell module projects.
For a more complete, real-world example with automated tests, see my
[PSql](https://github.com/sharpjs/PSql) module.

Ready to automate build-and-publish to PowerShell Gallery?  See this repository's
[GitHub Actions workflow](https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/blob/main/.github/workflows/build.yaml)
for an example.

<!--
  Copyright Subatomix Research Inc.
  SPDX-License-Identifier: MIT
-->
