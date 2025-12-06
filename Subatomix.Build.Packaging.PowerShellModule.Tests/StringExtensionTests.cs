// Copyright Subatomix Research Inc.
// SPDX-License-Identifier: MIT

namespace Subatomix.Build.Packaging.PowerShellModule;

[TestFixture]
public class StringExtensionTests
{
    [Test]
    [TestCase("",      "")]
    [TestCase("a",     "a")]
    [TestCase("'a'b'", "''a''b''")]
    public void EscapeSingleQuote(string input, string expected)
    {
        input.EscapeSingleQuote().ShouldBe(expected);
    }
}
