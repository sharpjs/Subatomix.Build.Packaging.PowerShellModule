# Changes in Subatomix.Build.Packaging.PowerShellModule
This file documents all notable changes.

Most lines should begin with one of these words:
*Add*, *Fix*, *Update*, *Change*, *Deprecate*, *Remove*.

<!--
## [Unreleased](https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/compare/release/1.1.1..HEAD)
(none)
-->

## [Unreleased](https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/compare/release/1.1.1..HEAD)
- Updated Psd1 generation to be more extensible and accept more MSBuild Properties. Properties need to be added as item Psd1ReplacementProperty.

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
