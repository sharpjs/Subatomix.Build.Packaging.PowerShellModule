# Subatomix.Build.Packaging.PowerShellModule Usage

## Getting Started

In the project file, set the target framework to the .NET version that
[corresponds to](https://learn.microsoft.com/en-us/powershell/scripting/install/powershell-support-lifecycle)
the minimum PowerShell version the module will support.  For example, if the
module will support PowerShell 7.4 or later, set the target framework to .NET
8.0 (`net8.0`).

Then, add NuGet package references to:
- [Subatomix.Build.Packaging.PowerShellModule](https://www.nuget.org/packages/Subatomix.Build.Packaging.PowerShellModule)
  (this package).
- [System.Management.Automation](https://www.nuget.org/packages/System.Management.Automation),
  matching the minimum PowerShell version the module supports.

Here is a minimal `.csproj` file targeting PowerShell 7.4 or later:

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
    <!-- Add module metadata properties here -->
  </PropertyGroup>

  <ItemGroup>
    <!-- Add module content items here -->
  </ItemGroup>

</Project>
```

Now, `dotnet pack` and the Pack feature in Visual Studio will produce a
specially-structured NuGet `.nupkg` file containing a PowerShell module,
ready for distribution via a module repository like [PowerShell Gallery][psg].
By default, the module includes:

- A module manifest (`.psd1` file) generated from project properties and items.
- The compiled assembly, which can be either a `.dll` file or an executable.
- Other PowerShell files, including `.ps1`, `.psm1`, `.psd1`, `.ps1xml`,
  `.pssc`, `.psrc`, `.cdxml`, `.help.txt`, and `.dll-help.xml`.
- Any additional files included as `Content` items.

## Module Types

Choose what [type of module][mt] to build.

- **Binary Module**: The module includes a .NET assembly that provides the
  module's content.  This is the default module type for projects using this
  package.

- **Script Module**: The module includes PowerShell one or more script files
  that provide the module's content.  To create a script module, add a `.psm1`
  file whose name matches the module name.  In the file, define the module's
  functions and other content.  The file can dot-source other `.ps1` files as
  required.

  By default, the module still includes the compiled assembly and configures
  the assembly to load whenever a PowerShell session imports the module.  This
  arrangement enables the script module to use types defined in the assembly,
  resulting in a form of hybrid module.

  To create a pure script module with no binary content, set the appropriate
  project property to exclude the built assembly from the module:

  ```xml
  <PropertyGroup>
    <IncludeBuildOutput>false</IncludeBuildOutput>
  </PropertyGroup>
  ```

- **Manifest Module**: The module does not include a .NET assembly or script to
  provide content.  Instead, the module's content is described entirely within
  a module manifest.  This type of module often is used to package one or more
  nested modules.  To create a manifest module, set the appropriate project
  project property to exclude the built assembly from the module:

  ```xml
  <PropertyGroup>
    <IncludeBuildOutput>false</IncludeBuildOutput>
  </PropertyGroup>
  ```

  In a manifest module, do not add a `.psm1` file to the project.

## Module Manifest Generation

By default, this package generates a module manifest (`.psd1` file) from
project properties and items.  Generally, each supported property or item
corresponds to a similarly-named manifest element.  This package recognizes
many of the usual [NuGet packaging properties][npp] and provides additional
properties and items to cover [PowerShell-specific metadata](mme).

For example:

```xml
<Project>
  ...
  <PropertyGroup>
    <VersionPrefix>1.0.0</VersionPrefix>
    <VersionSuffix>alpha</VersionSuffix>

    <Description>My PowerShell module</Description>
    <Authors>My Name</Authors>
    <Company>My Company</Company>
    <Copyright>© 2026 My Company</Copyright>

    <ModuleGuid>...your GUID here...</ModuleGuid>
    <MinimumPowerShellVersion>7.4</MinimumPowerShellVersion>
  </PropertyGroup>

  <ItemGroup>
    <CmdletsToExport Include="Invoke-MyThing" />
  </ItemGroup>
  ...
</Project>
```

A generated manifest is sufficient for most modules.  Module authors with
advanced scenarios (or picky taste) can write custom module manifests using a
simple templating system, described in [Module Manifest Templating][mmt].

There is no need to commit the generated manifest to source control.  To
exclude all `.psd1` files when using Git, add `*.psd1` to the appropriate
`.gitignore` file.  For example, using PowerShell:

```powershell
Add-Content .gitignore @(
  ''
  '# PowerShell module manifests'
  '*.psd1'
)
```

### Properties

Manifest generation uses the following project properties:

| Project Property                        | In Module Manifest                                  |
|-----------------------------------------|-----------------------------------------------------|
| *Descriptive*                           |                                                     |
| &emsp;`Description`                     | `Description`                                       |
| &emsp;`Authors`                         | `Author`                                            |
| &emsp;`Company`                         | `CompanyName`                                       |
| &emsp;`Copyright`                       | file header comment<br>`Copyright`                  |
| *Packaging*                             |                                                     |
| &emsp;`PackageId`                       | file name                                           |
| &emsp;`PackageVersion`                  | `ModuleVersion`<br/>`PrivateData.PSData.Prerelease` |
| &emsp;`PackageProjectUrl`               | `PrivateData.PSData.ProjectUri`                     |
| &emsp;`PackageLicenseExpression`        | file header comment                                 |
| &emsp;`PackageRequireLicenseAcceptance` | `PrivateData.PSData.RequireLicenseAcceptance`       |
| &emsp;`PackageIconUrl`                  | `PrivateData.PSData.IconUri`                        |
| &emsp;`PackageReleaseNotes`             | `PrivateData.PSData.ReleaseNotes`                   |
| &emsp;`PackageTags`                     | `PrivateData.PSData.Tags`                           |
| *Module-specific properties*            |                                                     |
| &emsp;`ModuleGuid`                      | `GUID`                                              |
| &emsp;`RootModule`                      | `RootModule`                                        |
| &emsp;`ModuleLicenseUrl`                | `PrivateData.PSData.LicenseUri`                     |
| &emsp;`MinimumPowerShellVersion`        | `PowerShellVersion`                                 |
| &emsp;`RequiredPowerShellHostName`      | `PowerShellHostName`                                |
| &emsp;`MinimumPowerShellHostVersion`    | `PowerShellHostVersion`                             |
| &emsp;`MinimumDotNetFrameworkVersion`   | `DotNetFrameworkVersion`                            |
| &emsp;`MinimumClrVersion`               | `CLRVersion`                                        |
| &emsp;`RequiredProcessorArchitecture`   | `ProcessorArchitecture`                             |
| &emsp;`DefaultCommandPrefix`            | `DefaultCommandPrefix`                              |
| &emsp;`HelpInfoUri`                     | `HelpInfoURI`                                       |

A few of these properties deserve special note:

#### `PackageId`

Defaults to the assembly name.  Most projects do not set this property
explicitly.

#### `PackageVersion`

Defaults to `Version`, which by default is derived from `VersionPrefix` and
`VersionSuffix`.  Most projects do not set `PackageVersion` explicitly.

#### `PackageRequireLicenseAcceptance` and `PackageTags`

Automatically converted to formats appropriate for PowerShell module manifests.

#### `RootModule`

The default depends on module type detection.  Most projects do not need
to set this property explicitly.  Detection works as follows:

1. If a file named `<PackageId>.psm1` exists, `RootModule` defaults to that
   file's name.  The module is a script module.

2. Else, if `IncludeBuildOutput` is `false`, `RootModule` defaults to empty.
   The module is a manifest module.

3. Else, `RootModule` defaults to the compiled assembly file name.  The module
   is a binary module.

#### `ModuleLicenseUrl`

This property replaces the deprecated `PackageLicenseUrl` property, which now
generates a warning when used.

### Items

Manifest generation uses the following project items:

| MSBuild Item           | In Module Manifest     |
|------------------------|------------------------|
| `CompatiblePSEditions` | `CompatiblePSEditions` |
| `RequiredModules`      | `RequiredModules`<br>`ExternalModuleDependencies`[<sup>[1]</sup>](#if1) |
| `RequiredAssemblies`   | `RequiredAssemblies`   |
| `TypesToProcess`       | `TypesToProcess`       |
| `FormatsToProcess`     | `FormatsToProcess`     |
| `ScriptsToProcess`     | `ScriptsToProcess`     |
| `NestedModules`        | `NestedModules`<br>`ExternalModuleDependencies`[<sup>[1]</sup>](#if1) |
| `CmdletsToExport`      | `CmdletsToExport`      |
| `FunctionsToExport`    | `FunctionsToExport`    |
| `AliasesToExport`      | `AliasesToExport`      |
| `VariablesToExport`    | `VariablesToExport`    |
| `DscResourcesToExport` | `DscResourcesToExport` |

<a name="if1">1.</a> if item metadata `External` is `true`.

### Unsupported Module Manifest Elements

This package does not populate the `ModuleList` or `FileList` module manifest
elements in generated manifests.  These elements are informational only and
often are omitted or empty in module manifests in the wild.  Manifests
requiring these elements can be created using templating, described below.

## Module Manifest Templating

To create a custom module manifest using a template, add a `.psd1.t` template
file to the project.  During build, this package processes the template to
produce a matching module manifest (`.psd1` file).  Using a template, module
authors can support advanced scenarios, such as nested modules or conditional
logic within the module manifest.

Such a template may have any valid file name (with the `.psd1.t` extension) and
may be located in any folder within the project.  However, if the template file
is named like `<module-name>.psd1.t` and is located in the project root, the
resulting `.psd1` file becomes the main manifest of the module.  In that case,
this package no longer generates a manifest from project properties and items.

There is no need to commit manifests created from templates to source control.
Commit the templates, but feel free to ignore the resulting manifests.

### Placeholders

A `.psd1.t` template file should contain a [PowerShell module manifest][mm] but
also may include placeholders that are replaced during build.  A placeholder
takes the form `{Name}`, where `Name` may be any non-empty alphanumeric word.
Spaces are not allowed within the curly braces.

For example, this snippet from a module manifest template contains several
placeholders:

```powershell
# {Copyright}
@{
    ...
    ModuleVersion = '{PackageVersion}'
    ...
    CmdletsToExport = @({CmdletsToExport})
    ...
}
```

Placeholders have a very limited degree of smartness:

- If the character `'` (a single quote) immediately precedes a placeholder,
  template processing assumes that the placeholder is within a single-quoted
  string and escapes the replacement text by doubling any single quotes.

  Example: `'{Name}'` with replacement `O'Brien` becomes `'O''Brien'`.

- If the characters `@(` immediately precede a placeholder, template processing
  assumes that the placeholder is within an array literal and converts the
  replacement text from a space-or-semicolon-separated list into a
  comma-separated list of single-quoted (and escaped) strings.

  Example: `@({Names})` with replacement `Smith; O'Brien` becomes
  `@('Smith', 'O''Brien')`.

- Otherwise, the replacement text is inserted verbatim.

### Replacements

The following project properties are available as placeholder replacements:

- Common properties:
  `AssemblyName`, `AssemblyTitle`, `Configuration`

- Version properties:
  `VersionPrefix`, `VersionSuffix`, `Version`, `AssemblyVersion`,
  `FileVersion`, `InformationalVersion`

- Descriptive properties:
  `Description`, `Title`, `Product`, `Authors`, `Company`, `Copyright`,
  `Trademark`

- Packaging properties:
  `PackageId`, `PackageVersion`, `PackageProjectUrl`,
  `PackageLicenseExpression`, `PackageLicenseFile`, `PackageIcon`,
  `PackageIconUrl`, `PackageReadmeFile`, `PackageReleaseNotes`, `PackageTags`,
  `RepositoryUrl`, `RepositoryType`, `RepositoryCommit`

- Module properties:
  `ModuleGuid`, `ModuleLicenseUrl`, `ModuleRequireLicenseAcceptance`,
  `MinimumPowerShellVersion`, `RequiredPowerShellHostName`,
  `MinimumPowerShellHostVersion`, `MinimumDotNetFrameworkVersion`,
  `MinimumClrVersion`, `RequiredProcessorArchitecture`

To define custom replacements or override the default ones, add
`Psd1Replacement` project items, providing the replacement text in `Value`
metadata.

```xml
  <ItemGroup>
    <Psd1Replacement Include="ShoeSize" Value="9" />
  </ItemGroup>
```

<!------------------------------------------------------------------------------------------------>

[mm]:  https://learn.microsoft.com/en-us/powershell/scripting/developer/module/how-to-write-a-powershell-module-manifest
[mme]: https://learn.microsoft.com/en-us/powershell/scripting/developer/module/how-to-write-a-powershell-module-manifest#module-manifest-elements
[mmt]: #module-manifest-templating
[mt]:  https://learn.microsoft.com/en-us/powershell/scripting/developer/module/understanding-a-windows-powershell-module
[npp]: https://learn.microsoft.com/en-us/nuget/reference/msbuild-targets#pack-target
[psg]: https://www.powershellgallery.com/
[sv]:  https://semver.org/

