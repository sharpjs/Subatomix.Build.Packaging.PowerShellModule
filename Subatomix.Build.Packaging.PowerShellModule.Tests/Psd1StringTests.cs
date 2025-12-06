// Copyright Subatomix Research Inc.
// SPDX-License-Identifier: MIT

namespace Subatomix.Build.Packaging.PowerShellModule;

[TestFixture]
public class Psd1StringTests
{
    // This fixture tests only paths not coverable by GeneratePsd1Tests.

    [Test]
    public void WriteTo_NullText()
    {
        using var writer = new StringWriter();

        new Psd1String(null).WriteTo(writer, indent: 0);

        writer.Flush();
        writer.ToString().ShouldBe("''");
    }
}
