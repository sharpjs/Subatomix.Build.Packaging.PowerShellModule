// Copyright Subatomix Research Inc.
// SPDX-License-Identifier: MIT

namespace Subatomix.Build.Packaging.PowerShellModule;

internal sealed class Psd1Document
{
    public Psd1Comment[] Header { get; }
    public Psd1HashTable Data   { get; }

    public Psd1Document(Psd1Comment[] header, Psd1HashTable data)
    {
        Header = header;
        Data   = data;
    }

    public void WriteLineTo(TextWriter writer)
    {
        foreach (var comment in Header)
            comment.WriteLineTo(writer, 0);

        Data.WriteTo(writer, 0);
        writer.WriteLine();
    }
}
