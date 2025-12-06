// Copyright Subatomix Research Inc.
// SPDX-License-Identifier: MIT

namespace Subatomix.Build.Packaging.PowerShellModule;

internal abstract class Psd1Value
{
    public abstract bool HasContent  { get; }
    public abstract bool IsMultiLine { get; }

    public abstract void WriteTo(TextWriter writer, int indent);
}
