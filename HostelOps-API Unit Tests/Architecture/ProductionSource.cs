using System.Text.RegularExpressions;

namespace HostelOps_API_Unit_Tests.Architecture;

/// <summary>
/// Production C# sources under the solution (excludes tests, bin, and obj).
/// </summary>
internal static class ProductionSource
{
    public static IReadOnlyList<string> CSharpFiles { get; } = Discover();

    public static IEnumerable<(string Path, string Contents)> ReadAll() =>
        CSharpFiles.Select(path => (path, File.ReadAllText(path)));

    public static IReadOnlyList<string> FindLineMatches(Regex pattern)
    {
        var violations = new List<string>();

        foreach (var (path, contents) in ReadAll())
        {
            var relative = Path.GetRelativePath(ArchitectureSolution.Root, path);
            var lines = contents.Replace("\r\n", "\n").Split('\n');
            for (var index = 0; index < lines.Length; index++)
            {
                var line = lines[index];
                var trimmed = line.TrimStart();
                if (trimmed.StartsWith("//", StringComparison.Ordinal) ||
                    trimmed.StartsWith("*", StringComparison.Ordinal))
                {
                    continue;
                }

                if (pattern.IsMatch(line))
                    violations.Add($"{relative}:{index + 1}: {trimmed.Trim()}");
            }
        }

        return violations;
    }

    public static void AssertNoMatches(Regex pattern, string rule)
    {
        var violations = FindLineMatches(pattern);
        if (violations.Count == 0)
            return;

        Assert.Fail($"{rule}{Environment.NewLine}Violations:{Environment.NewLine}{string.Join(Environment.NewLine, violations)}");
    }

    private static IReadOnlyList<string> Discover()
    {
        var separator = Path.DirectorySeparatorChar;
        return Directory.EnumerateFiles(ArchitectureSolution.Root, "*.cs", SearchOption.AllDirectories)
            .Where(path =>
                !path.Contains($"{separator}bin{separator}", StringComparison.OrdinalIgnoreCase) &&
                !path.Contains($"{separator}obj{separator}", StringComparison.OrdinalIgnoreCase))
            .Where(path => !IsTestPath(path))
            .OrderBy(path => path, StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    private static bool IsTestPath(string path)
    {
        var relative = Path.GetRelativePath(ArchitectureSolution.Root, path);
        return relative.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
            .Any(segment => segment.Contains("Test", StringComparison.OrdinalIgnoreCase));
    }
}
