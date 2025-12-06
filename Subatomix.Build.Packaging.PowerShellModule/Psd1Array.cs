// Copyright Subatomix Research Inc.
// SPDX-License-Identifier: MIT

using System.Diagnostics.CodeAnalysis;

namespace Subatomix.Build.Packaging.PowerShellModule;

internal sealed class Psd1Array : Psd1Value
{
    public ITaskItem[]? Items { get; }

    [MemberNotNullWhen(true, nameof(Items))]
    public override bool HasContent { get; }

    [MemberNotNullWhen(true, nameof(Items))]
    public override bool IsMultiLine { get; }

    public Psd1Array(ITaskItem[]? items, bool inline = false)
    {
        Items = items;

        if (items is null || items.Length is 0)
            return;

        HasContent  = true;
        IsMultiLine = items.Length > 1 && !inline;
    }

    public override void WriteTo(TextWriter writer, int indent)
    {
        writer.Write("@(");

        if (!HasContent)
        {
            // Empty array; always inline
        }
        else if (!IsMultiLine)
        {
            WriteItemsInline(writer, Items);
        }
        else // (HasContent && IsMultiLine)
        {
            WriteItemsOnePerLine(writer, Items, indent);
        }

        writer.Write(')');
    }

    private void WriteItemsInline(TextWriter writer, ITaskItem[] items)
    {
        var separator = "";

        foreach (var item in items)
        {
            writer.Write(separator);
            writer.WriteSingleQuoted(item.ItemSpec);
            separator = ", ";
        }
    }

    private void WriteItemsOnePerLine(TextWriter writer, ITaskItem[] items, int indent)
    {
        writer.WriteLine();
        indent++;

        foreach (var item in items)
        {
            writer.WriteIndent(indent);
            writer.WriteSingleQuoted(item.ItemSpec);
            writer.WriteLine();
        }

        indent--;
        writer.WriteIndent(indent);
    }
}
