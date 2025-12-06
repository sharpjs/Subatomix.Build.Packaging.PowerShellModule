// Copyright Subatomix Research Inc.
// SPDX-License-Identifier: MIT

namespace Subatomix.Build.Packaging.PowerShellModule;

[TestFixture]
public class GeneratePsd1Tests
{
    [Test]
    public void FileName_GetBeforeSet()
    {
        var task = new GeneratePsd1();

        Should.Throw<InvalidOperationException>(() =>
        {
            var _ = task.FileName;
        });
    }

    [Test]
    public void VersionPrefix_GetBeforeSet()
    {
        var task = new GeneratePsd1();

        Should.Throw<InvalidOperationException>(() =>
        {
            var _ = task.VersionPrefix;
        });
    }

    [Test]
    public void Execute_Minimal()
    {
        var io = new InMemoryIoStrategy();

        var task = new GeneratePsd1
        {
            Io            = io,
            FileName      = "Module.psd1",
            VersionPrefix = "1.2.3",
        };

        task.Execute().ShouldBeTrue();

        io.ReadText("Module.psd1").ShouldBe(
            """
            @{
                # Identity
                ModuleVersion = '1.2.3'

                # Exports
                CmdletsToExport   = @()
                FunctionsToExport = @()
                AliasesToExport   = @()
                VariablesToExport = @()
            }

            """
        );
    }

    [Test]
    public void Execute_Maximal()
    {
        var io = new InMemoryIoStrategy();

        var task = new GeneratePsd1
        {
            Io                         = io,
            FileName                   = "Module.psd1",
            ModuleGuid                 = "01234567-0123-4567-89ab-0123456789ab",
            RootModule                 = "Module.psm1",
            VersionPrefix              = "1.2.3",
            VersionSuffix              = "test",

            Description                = "A test module.",
            Author                     = "Maud U. L. Arthur",
            Company                    = "ModuleCo",
            Copyright                  = "© Maud U. L. Arthur",
            LicenseExpression          = "MIT-0",
            LicenseUri                 = "https://opensource.org/license/mit-0/",
            RequireLicenseAcceptance   = true,
            ProjectUri                 = "https://example.com/project",
            IconUri                    = "https://example.com/icon.png",
            ReleaseNotes               = "https://example.com/release-notes.md",
            Tags                       = [new TaskItem("test"), new TaskItem("fake")],

            CompatibleEditions         = [new TaskItem("Core"), new TaskItem("Desktop")],
            PowerShellVersion          = "5.1",
            PowerShellHostName         = "TestHost",
            PowerShellHostVersion      = "1.0",
            DotNetFrameworkVersion     = "4.5",
            ClrVersion                 = "4.0",
            ProcessorArchitecture      = "Amd64",

            RequiredAssemblies         = [new TaskItem("A.dll"), new TaskItem("B.dll")],
            RequiredModules            = [
                new TaskItem("ModuleA"),
                new TaskItem("ModuleB")
                    .WithMetadata("Guid",    "12345678-0123-4567-89ab-123456789abc")
                    .WithMetadata("Version", "2.3.4")
            ],

            ScriptsToProcess           = [new TaskItem("Script.ps1")],
            TypesToProcess             = [new TaskItem("Type.ps1xml")],
            FormatsToProcess           = [new TaskItem("Format.ps1xml")],
            NestedModules              = [
                new TaskItem("NestedModuleA"),
                new TaskItem("NestedModuleB")
                    .WithMetadata("Guid",    "23456789-0123-4567-89ab-23456789abcd")
                    .WithMetadata("Version", "3.4.5")
            ],

            CmdletsToExport            = [new TaskItem("Do-Cmdlet")],
            FunctionsToExport          = [new TaskItem("Do-Function")],
            VariablesToExport          = [new TaskItem("TestVariable")],
            AliasesToExport            = [new TaskItem("TestAlias")],
            DscResourcesToExport       = [new TaskItem("TestDscResource")],
            DefaultCommandPrefix       = "Test",

            ExternalModuleDependencies = [new TaskItem("ModuleB"), new TaskItem("NestedModuleB")],
        };

        task.Execute().ShouldBeTrue();

        io.ReadText("Module.psd1").ShouldBe(
            """
            # © Maud U. L. Arthur
            # SPDX-License-Identifier: MIT-0
            @{
                # Identity
                GUID          = '01234567-0123-4567-89ab-0123456789ab'
                RootModule    = 'Module.psm1'
                ModuleVersion = '1.2.3'
            
                # General
                Description = 'A test module.'
                Author      = 'Maud U. L. Arthur'
                CompanyName = 'ModuleCo'
                Copyright   = '© Maud U. L. Arthur'
            
                # Requirements
                CompatiblePSEditions = @(
                    'Core'
                    'Desktop'
                )
                PowerShellVersion      = '5.1'
                PowerShellHostName     = 'TestHost'
                PowerShellHostVersion  = '1.0'
                DotNetFrameworkVersion = '4.5'
                CLRVersion             = '4.0'
                ProcessorArchitecture  = 'Amd64'
            
                # Load Before Import
                RequiredModules = @(
                    'ModuleA'
                    'ModuleB'
                )
                RequiredAssemblies = @(
                    'A.dll'
                    'B.dll'
                )
            
                # Run On Import
                ScriptsToProcess = @('Script.ps1')
                TypesToProcess   = @('Type.ps1xml')
                FormatsToProcess = @('Format.ps1xml')
                NestedModules    = @(
                    'NestedModuleA'
                    'NestedModuleB'
                )
            
                # Exports
                CmdletsToExport      = @('Do-Cmdlet')
                FunctionsToExport    = @('Do-Function')
                AliasesToExport      = @('TestAlias')
                VariablesToExport    = @('TestVariable')
                DscResourcesToExport = @('TestDscResource')
            
                # Override using Import-Module -Prefix
                DefaultCommandPrefix = 'Test'
            
                # Private data to pass to the RootModule
                PrivateData = @{
                    PSData = @{
                        # Additional metadata
                        Prerelease   = 'test'
                        ProjectUri   = 'https://example.com/project'
                        ReleaseNotes = 'https://example.com/release-notes.md'
                        LicenseUri   = 'https://opensource.org/license/mit-0/'
                        IconUri      = 'https://example.com/icon.png'
                        Tags         = @('test', 'fake')
            
                        ExternalModuleDependencies = @(
                            'ModuleB'
                            'NestedModuleB'
                        )
                    }
                }
            }

            """
        );
    }
}
