# Copyright Subatomix Research Inc.
# SPDX-License-Identifier: MIT-0
# ^ Remove this header when using this file as a template in other projects.
@{
    # Identity
    GUID          = '1c5bcb27-a606-4bd5-addb-65bd864560c4'
    RootModule    = 'Subatomix.ExampleModule.Sdk8.psm1'
    ModuleVersion = '{VersionPrefix}'

    # General
    Description = 'Example module built with Subatomix.Build.Packaging.PowerShellModule'
    Author      = 'Jeffrey Sharp'
    CompanyName = 'Subatomix Research Inc.'
    Copyright   = '{Copyright}'

    # Requirements
    CompatiblePSEditions = 'Core'
    PowerShellVersion    = '7.4'

    # Load Before Import
    #RequiredModules   = @()
    RequiredAssemblies = @('Subatomix.ExampleModule.Sdk8.dll')

    # Run On Import
    #ScriptsToProcess = @(...) # Script files (.ps1)    run in caller's environment
    #TypesToProcess   = @(...) # Type   files (.ps1xml) to be loaded
    #FormatsToProcess = @(...) # Format files (.ps1xml) to be loaded
    #NestedModules    = @(...) # Modules to import as nested modules of RootModule

    # Exports
    # NOTE: Use @() (empty array) to indicate no exports.
    #       Use '*' (star string) to export everything.
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
            LicenseUri   = 'https://github.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/blob/main/test/LICENSE.txt'
            IconUri      = 'https://raw.githubusercontent.com/sharpjs/Subatomix.Build.Packaging.PowerShellModule/refs/heads/main/test/icon.png'

            # Required modules not hosted in this module's repository
            #ExternalModuleDependencies = @()
        }
    }
}

