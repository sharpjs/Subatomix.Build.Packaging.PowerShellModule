// Copyright Subatomix Research Inc.
// SPDX-License-Identifier: MIT

namespace Subatomix.Build.Packaging.PowerShellModule;

[TestFixture]
public class FileIoStrategyTests
{
    [Test]
    public void Instance()
    {
        FileIoStrategy.Instance
            .ShouldNotBeNull()
            .ShouldBeSameAs(FileIoStrategy.Instance);
    }

    [Test]
    public void WriteText()
    {
        var filename = Guid.NewGuid().ToString("N") + ".tmp";

        try
        {
            FileIoStrategy.Instance.WriteText(filename, "foo");

            File.ReadAllText(filename).ShouldBe("foo");
        }
        finally
        {
            File.Delete(filename);
        }
    }

    [Test]
    public void ReadText()
    {
        var filename = Guid.NewGuid().ToString("N") + ".tmp";

        try
        {
            File.WriteAllText(filename, "bar");

            FileIoStrategy.Instance.ReadText(filename).ShouldBe("bar");
        }
        finally
        {
            File.Delete(filename);
        }
    }

    [Test]
    public void CreateTextWriter()
    {
        var filename = Guid.NewGuid().ToString("N") + ".tmp";

        try
        {
            using (var writer = FileIoStrategy.Instance.CreateTextWriter(filename))
            {
                writer.Write("baz");
                writer.Flush();
            }

            File.ReadAllText(filename).ShouldBe("baz");
        }
        finally
        {
            File.Delete(filename);
        }
    }
}
