# Subatomix.Build.Packaging.PowerShellModule

PowerShell module packaging support for .NET
[SDK-style](https://docs.microsoft.com/en-us/dotnet/core/tools/csproj)
projects.

## Status

In use by a handful of modules.

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
    <PackageReference
      Include="System.Management.Automation"
      Version="7.0.3"
      PrivateAssets="All" />
    <PackageReference
      Include="Subatomix.Build.Packaging.PowerShellModule"
      Version="1.0.0" />
  </ItemGroup>

</Project>
```

For a complete example, see this repository's
[test project](https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/tree/master/test).
It can serve as a template for new PowerShell module projects.
