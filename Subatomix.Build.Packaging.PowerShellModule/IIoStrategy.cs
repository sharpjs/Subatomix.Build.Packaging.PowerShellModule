// Copyright Subatomix Research Inc.
// SPDX-License-Identifier: MIT

namespace Subatomix.Build.Packaging.PowerShellModule;

internal interface IIoStrategy
{
    TextWriter CreateTextWriter(string path);

    void WriteText(string path, string content);

    string ReadText(string path);
}
