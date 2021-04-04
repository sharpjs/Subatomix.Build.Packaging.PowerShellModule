# Changes in Subatomix.Build.Packaging.PowerShellModule
This file documents all notable changes.

Most lines should begin with one of these words:
*Add*, *Fix*, *Update*, *Change*, *Deprecate*, *Remove*.

<!--
## [Unreleased](https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/compare/release/1.1.0..HEAD)
(none)
-->

## [1.1.0](https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/compare/release/1.0.0..release/1.1.0)
- Change: `.psd1.t` templates now generate `.psd1` files directly in the output
  directory.  Generated `.psd1` files no longer appear as a nested file under
  each `.psd1.t` file.
- Fix: Visual Studio now knows to rebuild the project when a `.psd1.t` file changes.

## [1.0.0](https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/tree/release/1.0.0)
Initial release.
