# Changes in Subatomix.Build.Packaging.PowerShellModule
This file documents all notable changes.

Most lines should begin with one of these words:
*Add*, *Fix*, *Update*, *Change*, *Deprecate*, *Remove*.

<!--
## [Unreleased](https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/compare/release/1.2.0..HEAD)
-->

## [1.2.0](https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/compare/release/1.1.1..release/1.2.0)
- Change license to MIT for most content.
- Change license to MIT No Attribution (MIT-0) for the example module.
- Fix build error when using .NET 10 SDK.

## [1.1.1](https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/compare/release/1.1.0..release/1.1.1)
- Fix build error when using pre-release .NET SDK versions.

## [1.1.0](https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/compare/release/1.0.0..release/1.1.0)
- Change: PowerShell files are now Content items instead of None items.
  Included file extensions:
  `.ps1`, `.psm1`, `.psd1`, `.ps1xml`, `.pssc`, `.psrc`, `.cdxml`.

  Additionally, the following fragment is now required in project files:

  ```xml
  <ItemGroup>
    <Content Include="$(PowerShellItemIncludes)"
             Exclude="$(DefaultItemExcludes);$(DefaultExcludesInProjectFolder)" />
  </ItemGroup>
  ```

- Change: Content items are now copied to the output and publish directories
  by default.

- Fix: Visual Studio now knows to rebuild the project when a `.psd1.t` file changes.

## [1.0.0](https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/tree/release/1.0.0)
Initial release.

<!--
  Copyright Subatomix Research Inc.
  SPDX-License-Identifier: MIT
-->
