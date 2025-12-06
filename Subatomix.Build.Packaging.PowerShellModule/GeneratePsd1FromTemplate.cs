// Copyright Subatomix Research Inc.
// SPDX-License-Identifier: MIT

using System.Text.RegularExpressions;

namespace Subatomix.Build.Packaging.PowerShellModule;

public class GeneratePsd1FromTemplate : Microsoft.Build.Utilities.Task
{
    [Required]
    public ITaskItem[] InputFiles
    {
        get => field ?? throw new InvalidOperationException();
        set;
    }

    [Required]
    public ITaskItem[] Replacements
    {
        get => field ?? throw new InvalidOperationException();
        set;
    }

    internal IIoStrategy Io { get; set; } = FileIoStrategy.Instance;

    private readonly Dictionary<string, string>
        _replacements = new(StringComparer.OrdinalIgnoreCase);

    private static readonly Regex
        PlaceholderRegex = new(
            @"(?<= (?<Mode>'|@\() )? \{ (?<Name>\w+) \}",
            RegexOptions.Compiled         |
            RegexOptions.CultureInvariant |
            RegexOptions.ExplicitCapture  |
            RegexOptions.IgnorePatternWhitespace
        );

    public override bool Execute()
    {
        // Uncomment to debug task execution
        //Debugger.Launch();

        CollectReplacements();

        foreach (var inputFile in InputFiles)
        {
            var inputPath  = inputFile.GetMetadata("FullPath"); // never null
            var outputPath = GetOutputPath(inputPath);

            Transform(inputPath, outputPath);
        }

        return true;
    }

    private void CollectReplacements()
    {
        foreach (var item in Replacements)
        {
            var name  = item.ItemSpec;              // never null
            var value = item.GetMetadata("Value");  // never null; empty if no such metadata

            _replacements[name] = value;
        }
    }

    private static string GetOutputPath(string inputPath)
    {
        return inputPath.Length > 2
            && inputPath.EndsWith(".t", StringComparison.OrdinalIgnoreCase)
            ?  inputPath.Substring(0, inputPath.Length - 2)
            :  inputPath + ".generated";
    }

    private void Transform(string inputPath, string outputPath)
    {
        var content = Io.ReadText(inputPath);

        content = PlaceholderRegex.Replace(content, GetReplacement);

        Io.WriteText(outputPath, content);
    }

    private string GetReplacement(Match match)
    {
        var name = match.Groups["Name"].Value;
        var mode = match.Groups["Mode"].Value;

        if (!_replacements.TryGetValue(name, out var value))
            return match.Value; // leave placeholder unchanged

        return mode switch
        {
            "'"  => FormatForPsd1String(value),
            "@(" => FormatForPsd1Array (value),
            _    => value,
        };
    }

    private static string FormatForPsd1String(string value)
    {
        return value.EscapeSingleQuote();
    }

    private static string FormatForPsd1Array(string value)
    {
        return string.Join(
            separator: ", ",
            value
                .Split([';', ' '], StringSplitOptions.RemoveEmptyEntries)
                .Select(s => string.Concat("'", s.EscapeSingleQuote(), "'"))
        );
    }
}
