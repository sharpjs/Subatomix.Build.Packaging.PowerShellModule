# Changes in Subatomix.Build.Packaging.PowerShellModule
This file documents all notable changes.

## [Unreleased](https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/compare/release/1.2.0..HEAD)
- :warning: **Breaking Change:** This package now includes PowerShell files
  as `Content` items by default.  Project files should remove the XML fragment
  mentioned in the [1.1.0](#110) release note if previously added.

- :warning: **Breaking Change:** Project property `CopyLocalLockFileAssemblies`
  now defaults to `true`, which makes sense for most PowerShell modules.
  Module projects requiring `false` must now set the property explicitly.

- :warning: **Breaking Change:** `.deps.json` files are no longer packed in
  PowerShell modules, becuase PowerShell module loading does not use them.  To
  use them, isolate dependencies in a private load context as described
  [here](https://learn.microsoft.com/en-us/powershell/scripting/dev-cross-plat/resolving-dependency-conflicts).

- Add module manifest generation from project file properties and items.
- Add many new placeholders for use in `.psd1.t` files.
- Add `.dll-help.xml` and `.help.txt` as included PowerShell file extensions.

## [1.2.0](https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/compare/release/1.1.1..release/1.2.0)
- Change license to MIT for most content.
- Change license to MIT No Attribution (MIT-0) for the example module.
- Fix build error when using .NET 10 SDK.

## [1.1.1](https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/compare/release/1.1.0..release/1.1.1)
- Fix build error when using pre-release .NET SDK versions.

## [1.1.0](https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/compare/release/1.0.0..release/1.1.0)
- Change: PowerShell files are now `Content` items instead of None items.
  Included file extensions:
  `.ps1`, `.psm1`, `.psd1`, `.ps1xml`, `.pssc`, `.psrc`, `.cdxml`.

  Additionally, the following fragment is now required in project files:

  ```xml
  <ItemGroup>
    <Content Include="$(PowerShellItemIncludes)"
             Exclude="$(DefaultItemExcludes);$(DefaultExcludesInProjectFolder)" />
  </ItemGroup>
  ```

- Change: `Content` items are now copied to the output and publish directories
  by default.

- Fix: Visual Studio now knows to rebuild the project when a `.psd1.t` file changes.

## [1.0.0](https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/tree/release/1.0.0)
Initial release.

<!--
  Copyright Subatomix Research Inc.
  SPDX-License-Identifier: MIT
-->
