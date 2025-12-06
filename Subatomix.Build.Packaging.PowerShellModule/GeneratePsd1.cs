// Copyright Subatomix Research Inc.
// SPDX-License-Identifier: MIT

namespace Subatomix.Build.Packaging.PowerShellModule;

public class GeneratePsd1 : Microsoft.Build.Utilities.Task
{
    [Required]
    public string FileName { get => field ?? throw new InvalidOperationException(); set; }

    public string? ModuleGuid { get; set; }

    public string? RootModule { get; set; }

    [Required]
    public string VersionPrefix { get => field ?? throw new InvalidOperationException(); set; }

    public string? VersionSuffix { get; set; }

    public string? Description { get; set; }

    public ITaskItem[]? Tags { get; set; }

    public string? Author { get; set; }

    public string? Company { get; set; }

    public string? Copyright { get; set; }

    public string? LicenseExpression { get; set; }

    public string? LicenseUri { get; set; }

    public bool RequireLicenseAcceptance { get; set; }

    public string? ProjectUri { get; set; }

    public string? IconUri { get; set; }

    public string? ReleaseNotes { get; set; }

    public ITaskItem[]? CompatibleEditions { get; set; }

    public string? PowerShellVersion { get; set; }

    public string? PowerShellHostName { get; set; }

    public string? PowerShellHostVersion { get; set; }

    public string? DotNetFrameworkVersion { get; set; }

    public string? ClrVersion { get; set; }

    public string? ProcessorArchitecture { get; set; }

    public ITaskItem[]? RequiredModules { get; set; }

    public ITaskItem[]? RequiredAssemblies { get; set; }

    public ITaskItem[]? ScriptsToProcess { get; set; }

    public ITaskItem[]? TypesToProcess { get; set; }

    public ITaskItem[]? FormatsToProcess { get; set; }

    public ITaskItem[]? NestedModules { get; set; }

    public ITaskItem[]? FunctionsToExport { get; set; }

    public ITaskItem[]? VariablesToExport { get; set; }

    public ITaskItem[]? AliasesToExport { get; set; }

    public ITaskItem[]? DscResourcesToExport { get; set; }

    public ITaskItem[]? CmdletsToExport { get; set; }

    public string? DefaultCommandPrefix { get; set; }

    // How to use ExternalModuleDependencies
    // https://github.com/OneGet/oneget/issues/164#issuecomment-173436617
    public ITaskItem[]? ExternalModuleDependencies { get; set; }

    internal IIoStrategy Io { get; set; } = FileIoStrategy.Instance;

    /// <inheritdoc/>
    public override bool Execute()
    {
        // Uncomment to debug task execution
        //System.Diagnostics.Debugger.Launch();

        using var writer = Io.CreateTextWriter(FileName);

        CreatePsd1Document().WriteLineTo(writer);

        writer.Flush();
        return true;
    }

    private Psd1Document CreatePsd1Document()
    {
        // Documentation:
        // https://learn.microsoft.com/en-us/powershell/scripting/developer/module/how-to-write-a-powershell-module-manifest

        return new(
            header: [
                Comment(Copyright),
                Comment("SPDX-License-Identifier: ", LicenseExpression),
            ],
            HashTable(
                Group(
                    "Identity",
                    P("GUID",          ModuleGuid),
                    P("RootModule",    RootModule),
                    P("ModuleVersion", VersionPrefix)
                ),
                Group(
                    "General",
                    P("Description", Description),
                    P("Author",      Author),
                    P("CompanyName", Company),
                    P("Copyright",   Copyright)
                ),
                Group(
                    "Requirements",
                    P("CompatiblePSEditions",   CompatibleEditions),
                    P("PowerShellVersion",      PowerShellVersion),
                    P("PowerShellHostName",     PowerShellHostName),
                    P("PowerShellHostVersion",  PowerShellHostVersion),
                    P("DotNetFrameworkVersion", DotNetFrameworkVersion),
                    P("CLRVersion",             ClrVersion),
                    P("ProcessorArchitecture",  ProcessorArchitecture)
                ),
                Group(
                    "Load Before Import",
                    P("RequiredModules",    RequiredModules),
                    P("RequiredAssemblies", RequiredAssemblies)
                ),
                Group(
                    "Run On Import",
                    P("ScriptsToProcess", ScriptsToProcess),
                    P("TypesToProcess",   TypesToProcess),
                    P("FormatsToProcess", FormatsToProcess),
                    P("NestedModules",    NestedModules)
                ),
                Group(
                    "Exports",
                    P("CmdletsToExport",      CmdletsToExport,   isRequired: true),
                    P("FunctionsToExport",    FunctionsToExport, isRequired: true),
                    P("AliasesToExport",      AliasesToExport,   isRequired: true),
                    P("VariablesToExport",    VariablesToExport, isRequired: true),
                    P("DscResourcesToExport", DscResourcesToExport)
                ),
                Group(
                    "Override using Import-Module -Prefix",
                    P("DefaultCommandPrefix", DefaultCommandPrefix)
                ),
                Group(
                    "Private data to pass to the RootModule",
                    P("PrivateData", HashTable(
                        Group(
                            P("PSData", HashTable(
                                Group(
                                    "Additional metadata",
                                    P("Prerelease",   VersionSuffix),
                                    P("ProjectUri",   ProjectUri),
                                    P("ReleaseNotes", ReleaseNotes),
                                    P("LicenseUri",   LicenseUri),
                                    P("IconUri",      IconUri),
                                    P("Tags",         Tags, inline: true)
                                ),
                                Group(
                                    P("ExternalModuleDependencies", ExternalModuleDependencies)
                                )
                            ))
                        )
                    ))
                )
            )
        );
    }

    private static Psd1Comment Comment(string? text)
        => new("", text);

    private static Psd1Comment Comment(string prefix, string? text)
        => new(prefix, text);

    private static Psd1HashTable HashTable(params Psd1PropertyGroup[] groups)
        => new(groups);

    private static Psd1PropertyGroup Group(params Psd1Property[] properties)
        => new(title: null, properties);

    private static Psd1PropertyGroup Group(string title, params Psd1Property[] properties)
        => new(Comment(title), properties);

    private static Psd1Property P(string name, Psd1Value value)
        => new(name, value, isRequired: false);

    private static Psd1Property P(string name, string? text)
        => new(name, new Psd1String(text), isRequired: false);

    private static Psd1Property P(string name, ITaskItem[]? items, bool isRequired = false, bool inline = false)
        => new(name, new Psd1Array(items, inline), isRequired);
}
