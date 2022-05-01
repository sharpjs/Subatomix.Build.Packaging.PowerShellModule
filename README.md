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
    <TargetFramework>netcoreapp3.1</TargetFramework>
    <CopyLocalLockFileAssemblies>true</CopyLocalLockFileAssemblies>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="System.Management.Automation"
                      Version="7.0.0" PrivateAssets="All" />
    <PackageReference Include="Subatomix.Build.Packaging.PowerShellModule"
                      Version="1.1.0" PrivateAssets="All" />
  </ItemGroup>

  <ItemGroup>
    <Content Include="$(PowerShellItemIncludes)"
             Exclude="$(DefaultItemExcludes);$(DefaultExcludesInProjectFolder)" />
  </ItemGroup>

</Project>
```

For a simple example, see this repository's
[test project](https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/tree/master/test).
It can serve as a template for new PowerShell module projects.
For a more complete, real-world example with automated tests, see my
[PSql](https://github.com/sharpjs/PSql) module.

Ready to automate build-and-publish to PowerShell Gallery?  See this repository's
[GitHub Actions workflow](https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/blob/master/.github/workflows/build.yaml)
for an example.

<!--
  Copyright 2022 Subatomix Research Inc.

  Permission to use, copy, modify, and distribute this software for any
  purpose with or without fee is hereby granted, provided that the above
  copyright notice and this permission notice appear in all copies.

  THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
  WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
  MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR
  ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
  WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN
  ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF
  OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
-->
