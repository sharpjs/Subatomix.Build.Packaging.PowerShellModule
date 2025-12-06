// Copyright Subatomix Research Inc.
// SPDX-License-Identifier: MIT

namespace Subatomix.Build.Packaging.PowerShellModule;

internal static class TextWriterExtensions
{
    private const string Indent = "    ";

    private static readonly string[] Indents =
    [
        /*[0]*/ "",
        /*[1]*/ Indent,
        /*[2]*/ Indent + Indent,
        /*[3]*/ Indent + Indent + Indent,
        /*[4]*/ Indent + Indent + Indent + Indent,
    ];

    extension (TextWriter writer)
    {
        public void WriteIndent(int level)
        {
            writer.Write(Indents[level]);
        }

        public void WritePadding(string s, int width)
        {
            for (var i = s.Length; i < width; i++)
                writer.Write(' ');
        }

        public void WriteSingleQuoted(string s)
        {
            writer.Write('\'');
            writer.Write(s.EscapeSingleQuote()); // quotes are infrequent
            writer.Write('\'');
        }
    }
}
