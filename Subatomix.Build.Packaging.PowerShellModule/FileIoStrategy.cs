// Copyright Subatomix Research Inc.
// SPDX-License-Identifier: MIT

namespace Subatomix.Build.Packaging.PowerShellModule;

internal sealed class FileIoStrategy : IIoStrategy
{
    public static FileIoStrategy Instance { get; } = new();

    public string ReadText(string path)
        => File.ReadAllText(path);

    public void WriteText(string path, string content)
        => File.WriteAllText(path, content);

    public TextWriter CreateTextWriter(string path)
        => File.CreateText(path);
}
