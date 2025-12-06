// Copyright Subatomix Research Inc.
// SPDX-License-Identifier: MIT

namespace Subatomix.Build.Packaging.PowerShellModule;

internal class InMemoryIoStrategy : IIoStrategy
{
    public Dictionary<string, object> Files { get; } = [];

    public TextWriter CreateTextWriter(string path)
    {
        var writer = new StringWriter();
        Files[path] = writer;
        return writer;
    }

    public string ReadText(string path)
    {
        return Files[path].ToString()!;
    }

    public void WriteText(string path, string content)
    {
        Files[path] = content;
    }
}
