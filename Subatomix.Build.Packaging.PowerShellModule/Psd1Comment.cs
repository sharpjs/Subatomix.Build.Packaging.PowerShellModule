// Copyright Subatomix Research Inc.
// SPDX-License-Identifier: MIT

namespace Subatomix.Build.Packaging.PowerShellModule;

internal sealed class Psd1Comment
{
    public string  Prefix { get; }
    public string? Text   { get; }

    public Psd1Comment(string prefix, string? text)
    {
        Prefix = prefix;
        Text   = text;
    }

    public void WriteLineTo(TextWriter writer, int indent)
    {
        if (string.IsNullOrEmpty(Text))
            return;

        writer.WriteIndent(indent);
        writer.Write("# ");
        writer.Write(Prefix);
        writer.WriteLine(Text);
    }
}
