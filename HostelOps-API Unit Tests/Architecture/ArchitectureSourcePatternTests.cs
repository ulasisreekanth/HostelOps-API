using System.Text.RegularExpressions;

namespace HostelOps_API_Unit_Tests.Architecture;

/// <summary>
/// Source-level architecture.md rules that are easier to scan in text than in IL.
/// </summary>
public class ArchitectureSourcePatternTests
{
    [Fact(DisplayName = "Production code must use UTC timestamps instead of DateTime.Now")]
    public void Production_MustNotUse_DateTimeNow()
    {
        ProductionSource.AssertNoMatches(
            new Regex(@"\bDateTime\.(Now|Today)\b", RegexOptions.Compiled),
            "Use UTC internally; do not rely on DateTime.Now/Today (architecture.md section 46).");
    }

    [Fact(DisplayName = "Production code must not block on Task with Wait or GetResult")]
    public void Production_MustNotBlockOn_Task()
    {
        ProductionSource.AssertNoMatches(
            new Regex(@"GetAwaiter\s*\(\s*\)\s*\.\s*GetResult\s*\(|\bTask\.(WaitAll|WaitAny|Wait)\s*\(", RegexOptions.Compiled),
            "Avoid .Wait() / GetAwaiter().GetResult() (architecture.md section 38).");
    }
}
