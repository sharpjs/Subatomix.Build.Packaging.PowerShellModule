<#
    Copyright 2020 Subatomix Research, Inc.

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
#>
@{
    # Identity
    GUID          = 'ae120094-bc65-4ad6-8550-08d4e7eff79a'
    RootModule    = 'Subatomix.ExampleModule.psm1'
    ModuleVersion = '{VersionPrefix}'

    # General
    Description = 'Example module built with Subatomix.Build.Packaging.PowerShellModule'
    Author      = 'Jeffrey Sharp'
    CompanyName = 'Subatomix Research Inc.'
    Copyright   = '{Copyright}'

    # Requirements
    CompatiblePSEditions = 'Core'
    PowerShellVersion    = '7.0'

    # Load Before Import
    #RequiredModules   = @()
    RequiredAssemblies = @('Subatomix.ExampleModule.dll')

    # Run On Import
    #ScriptsToProcess = @(...) # Script files (.ps1)    run in caller's environment
    #TypesToProcess   = @(...) # Type   files (.ps1xml) to be loaded
    #FormatsToProcess = @(...) # Format files (.ps1xml) to be loaded
    #NestedModules    = @(...) # Modules to import as nested modules of RootModule

    # Exports
    # NOTE: Use @() (empty array) to indicate no exports.
    #       Use '*' (star string) to indicate everything exported.
    FunctionsToExport = @(
        'Test-PowerShellModulePackaging'
    )
    VariablesToExport    = @()
    AliasesToExport      = @()
    DscResourcesToExport = @()
    CmdletsToExport      = @()
    DefaultCommandPrefix = '' # Override using Import-Module -Prefix

    # Private data to pass to the RootModule
    PrivateData = @{
        PSData = @{
            # Additional metadata
            Prerelease   = '{VersionSuffix}'
            Tags         = @('Subatomix', 'Packaging', 'Example')
            ProjectUri   = 'https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule'
            ReleaseNotes = 'https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule.git'
            LicenseUri   = 'https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/blob/master/LICENSE.txt'
            IconUri      = 'https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/raw/master/src/icon.png'

            # Required modules not hosted in this module's repository
            #ExternalModuleDependencies = @()
        }
    }
}

