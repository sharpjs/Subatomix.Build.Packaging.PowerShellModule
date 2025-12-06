// Copyright Subatomix Research Inc.
// SPDX-License-Identifier: MIT

namespace Subatomix.Build.Packaging.PowerShellModule;

[TestFixture]
public class TextWriterExtensionsTests
{
    [Test]
    [TestCase(0, "")]
    [TestCase(1, "    ")]
    [TestCase(2, "        ")]
    [TestCase(3, "            ")]
    [TestCase(4, "                ")]
    public void WriteIndent(int level, string expected)
    {
        using var writer = new StringWriter();

        writer.WriteIndent(level);

        writer.ToString().ShouldBe(expected);
    }

    [Test]
    [TestCase(-1)]
    [TestCase(+5)]
    public void WriteIndent_OutOfRange(int level)
    {
        using var writer = new StringWriter();

        Should.Throw<IndexOutOfRangeException>(() =>
        {
            writer.WriteIndent(level);
        });
    }

    [Test]
    [TestCase("",    -1 , "")]
    [TestCase("",     0,  "")]
    [TestCase("",     1,  " ")]
    [TestCase("abc", -1,  "")]
    [TestCase("abc",  3,  "")]
    [TestCase("abc",  4,  " ")]
    [TestCase("abc",  5,  "  ")]
    public void WritePadding(string s, int width, string expected)
    {
        using var writer = new StringWriter();

        writer.WritePadding(s, width);

        writer.ToString().ShouldBe(expected);
    }

    [Test]
    [TestCase("",    "''")]
    [TestCase("a",   "'a'")]
    [TestCase("a'b", "'a''b'")]
    public void WriteSingleQuoted(string s, string expected)
    {
        using var writer = new StringWriter();

        writer.WriteSingleQuoted(s);

        writer.ToString().ShouldBe(expected);
    }
}
