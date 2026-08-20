using System.Reflection;
using System.Text.RegularExpressions;

namespace HostelOps_API_Unit_Tests.Architecture;

/// <summary>
/// Technical constraints from architecture.md that can be checked without
/// interpreting business meaning (sections 24, 39, 47, 55).
/// </summary>
public class TechnicalConstraintTests
{
    private static readonly Regex _financialPropertyName = new(
        @"(Amount|Price|Fee|Balance|Salary|Wage|Rent|Premium|Money)",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    [Fact(DisplayName = "Financial properties must not use float or double")]
    public void FinancialProperties_MustNotUse_FloatingPoint()
    {
        var violations = ArchitectureAssert.ProductionTypes()
            .SelectMany(type => type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(property => _financialPropertyName.IsMatch(property.Name))
                .Where(property => IsFloatingPoint(property.PropertyType))
                .Select(property => $"{type.FullName}.{property.Name} is {property.PropertyType.Name}"))
            .ToList();

        Assert.True(
            violations.Count == 0,
            "Never use floating-point types for monetary values (architecture.md §47). Use decimal:" +
            Environment.NewLine +
            string.Join(Environment.NewLine, violations));
    }

    [Fact(DisplayName = "Domain test projects must not reference infrastructure packages")]
    public void DomainTestProjects_MustNotReference_Infrastructure()
    {
        var domainTestProjects = Directory.EnumerateFiles(
                ArchitectureSolution.Root,
                "*.csproj",
                SearchOption.AllDirectories)
            .Where(path => !path.Contains($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}") &&
                           !path.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}"))
            .Where(path =>
            {
                var name = Path.GetFileNameWithoutExtension(path);
                return name.Contains("Test", StringComparison.OrdinalIgnoreCase) &&
                       Regex.IsMatch(
                           name.Replace(' ', '.').Replace('-', '.'),
                           @"\.Domain(\.|$)|(^|\.)Domain\.",
                           RegexOptions.IgnoreCase);
            })
            .ToList();

        if (domainTestProjects.Count == 0)
            return;

        var violations = new List<string>();
        foreach (var path in domainTestProjects)
        {
            var project = ParsePackages(path);
            foreach (var package in project)
            {
                if (ForbiddenDependencies.MatchesPrefix(package, ForbiddenDependencies.DomainPackagePrefixes))
                    violations.Add($"{Path.GetFileNameWithoutExtension(path)} references '{package}'");
            }
        }

        Assert.True(
            violations.Count == 0,
            "Domain tests must not require databases, HTTP, or cloud SDKs (architecture.md §55):" +
            Environment.NewLine +
            string.Join(Environment.NewLine, violations));
    }

    private static bool IsFloatingPoint(Type type)
    {
        var current = Nullable.GetUnderlyingType(type) ?? type;
        return current == typeof(float) ||
               current == typeof(double) ||
               current == typeof(Half);
    }

    private static IEnumerable<string> ParsePackages(string csprojPath)
    {
        var document = System.Xml.Linq.XDocument.Load(csprojPath);
        return document.Descendants()
            .Where(e => e.Name.LocalName == "PackageReference")
            .Select(e => e.Attribute("Include")?.Value)
            .Where(v => !string.IsNullOrWhiteSpace(v))
            .Select(v => v!);
    }
}
