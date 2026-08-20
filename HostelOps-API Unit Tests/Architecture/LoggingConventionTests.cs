using System.Text.RegularExpressions;

namespace HostelOps_API_Unit_Tests.Architecture;

/// <summary>
/// Objectively checkable logging.md rules (console usage, structured templates,
/// obvious secret placeholders).
/// </summary>
public class LoggingConventionTests
{
    private static readonly Regex _consoleWrite = new(
        @"\bConsole\.(WriteLine|Write)\s*\(",
        RegexOptions.Compiled);

    private static readonly Regex _interpolatedLog = new(
        @"\.(Log(?:Trace|Debug|Information|Warning|Error|Critical)|Log)\s*\(\s*\$""",
        RegexOptions.Compiled);

    private static readonly Regex _sensitivePlaceholder = new(
        @"\{(Password|Passwords|AccessToken|RefreshToken|ApiKey|APIKey|Secret|Secrets|ConnectionString|ConnectionStrings|EncryptionKey)\}",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    [Fact(DisplayName = "Production code must not use Console.Write or Console.WriteLine")]
    public void Production_MustNotUse_ConsoleWrite()
    {
        ProductionSource.AssertNoMatches(
            _consoleWrite,
            "Use ILogger instead of Console (logging.md).");
    }

    [Fact(DisplayName = "Production code must not use Debug.WriteLine or Trace.WriteLine")]
    public void Production_MustNotUse_DebugOrTraceWriteLine()
    {
        ProductionSource.AssertNoMatches(
            new Regex(@"\b(Debug|Trace)\.Write(Line)?\s*\(", RegexOptions.Compiled),
            "Use ILogger instead of Debug/Trace.WriteLine (logging.md).");
    }

    [Fact(DisplayName = "Log calls must not use interpolated string templates")]
    public void LogCalls_MustUse_StructuredTemplates()
    {
        ProductionSource.AssertNoMatches(
            _interpolatedLog,
            "Use structured logging templates with named placeholders, not string interpolation (logging.md).");
    }

    [Fact(DisplayName = "Log templates must not include secret property names")]
    public void LogTemplates_MustNotInclude_SecretPlaceholders()
    {
        ProductionSource.AssertNoMatches(
            _sensitivePlaceholder,
            "Never log passwords, tokens, API keys, secrets, or connection strings (logging.md).");
    }

    [Fact(DisplayName = "Shared appsettings default log level must not be Debug or Trace")]
    public void SharedAppSettings_MustNotDefaultTo_DebugOrTrace()
    {
        var violations = Directory.EnumerateFiles(
                ArchitectureSolution.Root,
                "appsettings*.json",
                SearchOption.AllDirectories)
            .Where(path => !path.Contains($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}") &&
                           !path.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}"))
            .Where(path => Path.GetFileName(path) is string name &&
                           name.IndexOf("Development", StringComparison.OrdinalIgnoreCase) < 0 &&
                           name.IndexOf("Local", StringComparison.OrdinalIgnoreCase) < 0)
            .SelectMany(DescribeDebugDefault)
            .ToList();

        Assert.True(
            violations.Count == 0,
            "Avoid excessive Debug/Trace logging in production settings (logging.md):" +
            Environment.NewLine +
            string.Join(Environment.NewLine, violations));
    }

    private static IEnumerable<string> DescribeDebugDefault(string path)
    {
        var json = System.Text.Json.JsonDocument.Parse(File.ReadAllText(path));
        if (!json.RootElement.TryGetProperty("Logging", out var logging) ||
            !logging.TryGetProperty("LogLevel", out var logLevel) ||
            !logLevel.TryGetProperty("Default", out var defaultLevel))
        {
            yield break;
        }

        var value = defaultLevel.GetString();
        if (value is "Debug" or "Trace")
        {
            yield return $"{Path.GetRelativePath(ArchitectureSolution.Root, path)}: Default={value}";
        }
    }
}
