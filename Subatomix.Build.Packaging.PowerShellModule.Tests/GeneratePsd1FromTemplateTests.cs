// Copyright Subatomix Research Inc.
// SPDX-License-Identifier: MIT

namespace Subatomix.Build.Packaging.PowerShellModule;

using static Path;

[TestFixture]
internal class GeneratePsd1FromTemplateTests
{
    [Test]
    public void InputFiles_Get()
    {
        var task = new GeneratePsd1FromTemplate();

        Should.Throw<InvalidOperationException>(() =>
        {
            var _ = task.InputFiles;
        });
    }

    [Test]
    public void InputFiles_Set()
    {
        var items = new[] { new TaskItem("foo") };

        var task = new GeneratePsd1FromTemplate
        {
            InputFiles = items
        };

        task.InputFiles.ShouldBeSameAs(items);
    }

    [Test]
    public void Replacements_Get()
    {
        var task = new GeneratePsd1FromTemplate();

        Should.Throw<InvalidOperationException>(() =>
        {
            var _ = task.Replacements;
        });
    }

    [Test]
    public void Replacements_Set()
    {
        var items = new[] { new TaskItem("foo") };

        var task = new GeneratePsd1FromTemplate
        {
            Replacements = items
        };

        task.Replacements.ShouldBeSameAs(items);
    }

    [Test]
    public void Execute_Empty()
    {
        var io = new InMemoryIoStrategy();

        var task = new GeneratePsd1FromTemplate
        {
            InputFiles    = [],
            Replacements  = [],
            Io            = io
        };

        task.Execute().ShouldBeTrue();

        io.Files.ShouldBeEmpty();
    }

    [Test]
    public void Execute_Typical()
    {
        var io = new InMemoryIoStrategy
        {
            Files =
            {
                [GetFullPath("Module.psd1.t")] =
                """
                {Foo}     is replaced verbatim
                '{Bar}'   is string-escaped
                @({Baz})  is split into an array of strings
                ' {Qux}'  is replaced verbatim; space prevents recognition of string
                @( {Xyz}) is replaced verbatim; space prevents recognition of array
                {Corge}   is not replaced because there is no Corge in Replacements
                """
            }
        };

        var task = new GeneratePsd1FromTemplate
        {
            InputFiles   = [new TaskItem("Module.psd1.t")],
            Replacements = [
                new TaskItem("Foo").WithMetadata("Value", "a's b"),
                new TaskItem("Bar").WithMetadata("Value", "c's d"),
                new TaskItem("Baz").WithMetadata("Value", "e's f;x"),
                new TaskItem("Qux").WithMetadata("Value", "g's h"),
                new TaskItem("Xyz").WithMetadata("Value", "i's j;y"),
                // No Corge here
            ],
            Io = io
        };

        task.Execute().ShouldBeTrue();

        io.Files.TryGetValue(GetFullPath("Module.psd1"), out var content).ShouldBeTrue();

        content.ShouldBe(
            """
            a's b     is replaced verbatim
            'c''s d'   is string-escaped
            @('e''s', 'f', 'x')  is split into an array of strings
            ' g's h'  is replaced verbatim; space prevents recognition of string
            @( i's j;y) is replaced verbatim; space prevents recognition of array
            {Corge}   is not replaced because there is no Corge in Replacements
            """
        );
    }

    [Test]
    [TestCase("Hi")]
    [TestCase("Module.unusual")]
    public void Execute_UnusualTemplateName(string name)
    {
        var io = new InMemoryIoStrategy
        {
            Files = { [GetFullPath(name)] = "{Foo}" }
        };

        var task = new GeneratePsd1FromTemplate
        {
            InputFiles   = [new TaskItem(name)],
            Replacements = [new TaskItem("Foo").WithMetadata("Value", "a")],
            Io           = io
        };

        task.Execute().ShouldBeTrue();

        io.Files.TryGetValue(GetFullPath($"{name}.generated"), out var content).ShouldBeTrue();

        content.ShouldBe("a");
    }

    [Test]
    public void Execute_MultipleTemplates()
    {
        var io = new InMemoryIoStrategy
        {
            Files =
            {
                [GetFullPath("ModuleA.psd1.t")] = "{Foo}",
                [GetFullPath("ModuleB.psd1.t")] = "{Bar}",
            }
        };

        var task = new GeneratePsd1FromTemplate
        {
            InputFiles = [
                new TaskItem("ModuleA.psd1.t"),
                new TaskItem("ModuleB.psd1.t"),
            ],
            Replacements = [
                new TaskItem("Foo").WithMetadata("Value", "a"),
                new TaskItem("Bar").WithMetadata("Value", "b"),
            ],
            Io = io
        };

        task.Execute().ShouldBeTrue();

        io.Files.TryGetValue(GetFullPath("ModuleA.psd1"), out var contentA).ShouldBeTrue();
        io.Files.TryGetValue(GetFullPath("ModuleB.psd1"), out var contentB).ShouldBeTrue();

        contentA.ShouldBe("a");
        contentB.ShouldBe("b");
    }

    [Test]
    public void Execute_ValuelessReplacement()
    {
        var io = new InMemoryIoStrategy
        {
            Files = { [GetFullPath("Module.psd1.t")] = ">{Foo}<" }
        };

        var task = new GeneratePsd1FromTemplate
        {
            InputFiles   = [new TaskItem("Module.psd1.t")],
            Replacements = [new TaskItem("Foo")], // No Value metadata
            Io           = io
        };

        task.Execute().ShouldBeTrue();

        io.Files.TryGetValue(GetFullPath("Module.psd1"), out var content).ShouldBeTrue();

        content.ShouldBe("><");
    }
}
