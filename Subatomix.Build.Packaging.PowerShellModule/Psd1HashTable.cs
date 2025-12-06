// Copyright Subatomix Research Inc.
// SPDX-License-Identifier: MIT

namespace Subatomix.Build.Packaging.PowerShellModule;

internal sealed class Psd1HashTable : Psd1Value
{
    public Psd1PropertyGroup[] Groups { get; }

    public override bool HasContent { get; }
    public override bool IsMultiLine => HasContent;

    public Psd1HashTable(Psd1PropertyGroup[] groups)
    {
        Groups     = groups;
        HasContent = Array.Exists(groups, g => g.HasContent);
    }

    public override void WriteTo(TextWriter writer, int indent)
    {
        writer.Write("@{");

        if (HasContent)
        {
            WriteGroupsTo(writer, indent + 1);
            writer.WriteIndent(indent);
        }

        writer.Write('}');
    }

    private void WriteGroupsTo(TextWriter writer, int indent)
    {
        foreach (var group in Groups)
        {
            if (!group.HasContent)
                continue;

            writer.WriteLine();
            group.WriteLineTo(writer, indent);
        }
    }
}
