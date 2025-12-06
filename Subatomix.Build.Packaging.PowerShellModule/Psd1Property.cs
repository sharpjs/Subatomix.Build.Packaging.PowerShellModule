// Copyright Subatomix Research Inc.
// SPDX-License-Identifier: MIT

namespace Subatomix.Build.Packaging.PowerShellModule;

internal sealed class Psd1Property
{
    public string    Name       { get; }
    public Psd1Value Value      { get; }
    public bool      IsRequired { get; }

    public bool HasContent  => Value.HasContent || IsRequired;
    public bool IsMultiLine => Value.IsMultiLine;

    public Psd1Property(string name, Psd1Value value, bool isRequired)
    {
        Name       = name;
        Value      = value;
        IsRequired = isRequired;
    }

    public void WriteLineTo(TextWriter writer, int indent, int nameWidth)
    {
        writer.WriteIndent(indent);
        writer.Write(Name);
        writer.WritePadding(Name, nameWidth);
        writer.Write(" = ");
        Value.WriteTo(writer, indent);
        writer.WriteLine();
    }
}
