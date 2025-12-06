// Copyright Subatomix Research Inc.
// SPDX-License-Identifier: MIT

namespace Subatomix.Build.Packaging.PowerShellModule;

internal static class StringExtensions
{
    extension (string s)
    {
        public string EscapeSingleQuote()
            => s.Replace("'", "''");
    }
}
