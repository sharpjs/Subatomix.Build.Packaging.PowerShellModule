// Copyright Subatomix Research Inc.
// SPDX-License-Identifier: MIT

namespace Subatomix.Build.Packaging.PowerShellModule;

internal sealed class Psd1PropertyGroup
{
    public Psd1Comment?   Title      { get; }
    public Psd1Property[] Properties { get; }

    public bool HasContent { get; }

    public Psd1PropertyGroup(Psd1Comment? title, Psd1Property[] properties)
    {
        Title      = title;
        Properties = properties;
        HasContent = Array.Exists(properties, p => p.HasContent);
    }

    public void WriteLineTo(TextWriter writer, int indent)
    {
        Title?.WriteLineTo(writer, indent);

        for (int start = 0, end = 0; start < Properties.Length; start = end)
        {
            var nameWidth = 0;

            while (end < Properties.Length)
            {
                var property = Properties[end++];

                if (!property.HasContent)
                    continue;

                if (property.IsMultiLine)
                    break;

                nameWidth = Math.Max(nameWidth, property.Name.Length);
            }

            for (var i = start; i < end; i++)
            {
                var property = Properties[i];

                if (!property.HasContent)
                    continue;

                property.WriteLineTo(writer, indent, nameWidth);
            }
        }
    }
}
