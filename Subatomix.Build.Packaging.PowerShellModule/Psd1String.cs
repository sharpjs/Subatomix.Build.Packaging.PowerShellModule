// Copyright Subatomix Research Inc.
// SPDX-License-Identifier: MIT

using System.Diagnostics.CodeAnalysis;

namespace Subatomix.Build.Packaging.PowerShellModule;

internal sealed class Psd1String : Psd1Value
{
    public string? Text { get; }

    [MemberNotNullWhen(true, nameof(Text))]
    public override bool HasContent { get; }

    [MemberNotNullWhen(true, nameof(Text))]
    public override bool IsMultiLine { get; }

    public Psd1String(string? text)
    {
        Text = text;

        if (string.IsNullOrEmpty(text))
            return;

        HasContent  = true;
        IsMultiLine = text.Contains('\n');
    }

    public override void WriteTo(TextWriter writer, int indent)
    {
        writer.WriteSingleQuoted(Text ?? "");
    }
}
