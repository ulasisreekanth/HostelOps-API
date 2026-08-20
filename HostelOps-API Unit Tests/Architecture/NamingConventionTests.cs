using System.Reflection;
using System.Text.RegularExpressions;

namespace HostelOps_API_Unit_Tests.Architecture;

/// <summary>
/// Objectively checkable naming-conventions.md rules that Roslyn naming styles
/// do not cover (suffixes, async, file names, ID vs Id).
/// </summary>
public class NamingConventionTests
{
    private static readonly Regex _endsWithUppercaseId = new(@"[a-z0-9]ID$", RegexOptions.Compiled);
    private static readonly Regex _httpVerbInRoute = new(
        @"\b(Get|Post|Put|Patch|Delete|Create|Update|Remove)[A-Z/]",
        RegexOptions.Compiled);

    [Fact(DisplayName = "Types inheriting ControllerBase must end with Controller")]
    public void Controllers_MustEndWith_Controller()
    {
        var violations = ArchitectureAssert.ProductionTypes()
            .Where(type => ArchitectureAssert.Inherits(type, "Microsoft.AspNetCore.Mvc.ControllerBase"))
            .Where(type => !type.Name.EndsWith("Controller", StringComparison.Ordinal))
            .Select(type => type.FullName ?? type.Name)
            .ToList();

        AssertNoViolations(violations, "Controller types must end with Controller.");
    }

    [Fact(DisplayName = "Custom exception types must end with Exception")]
    public void Exceptions_MustEndWith_Exception()
    {
        var violations = ArchitectureAssert.ProductionTypes()
            .Where(type => typeof(Exception).IsAssignableFrom(type))
            .Where(type => type is { IsClass: true, IsPublic: true })
            .Where(type => type.Namespace is not null &&
                           !type.Namespace.StartsWith("System", StringComparison.Ordinal) &&
                           !type.Namespace.StartsWith("Microsoft", StringComparison.Ordinal))
            .Where(type => !type.Name.EndsWith("Exception", StringComparison.Ordinal))
            .Select(type => type.FullName ?? type.Name)
            .ToList();

        AssertNoViolations(violations, "Custom exception names must end with Exception.");
    }

    [Fact(DisplayName = "Custom attribute types must end with Attribute")]
    public void Attributes_MustEndWith_Attribute()
    {
        var violations = ArchitectureAssert.ProductionTypes()
            .Where(type => typeof(Attribute).IsAssignableFrom(type))
            .Where(type => type is { IsClass: true, IsPublic: true })
            .Where(type => type.Namespace is not null &&
                           !type.Namespace.StartsWith("System", StringComparison.Ordinal) &&
                           !type.Namespace.StartsWith("Microsoft", StringComparison.Ordinal))
            .Where(type => !type.Name.EndsWith("Attribute", StringComparison.Ordinal))
            .Select(type => type.FullName ?? type.Name)
            .ToList();

        AssertNoViolations(violations, "Custom attribute classes must end with Attribute.");
    }

    [Fact(DisplayName = "Extension method classes must end with Extensions")]
    public void ExtensionClasses_MustEndWith_Extensions()
    {
        var violations = ArchitectureAssert.ProductionTypes()
            .Where(type => type is { IsClass: true, IsAbstract: true, IsSealed: true })
            .Where(HasExtensionMethod)
            .Where(type => !type.Name.EndsWith("Extensions", StringComparison.Ordinal))
            .Select(type => type.FullName ?? type.Name)
            .ToList();

        AssertNoViolations(violations, "Extension classes must use the Extensions suffix.");
    }

    [Fact(DisplayName = "Production Task-returning methods must use the Async suffix")]
    public void TaskReturningMethods_MustEndWith_Async()
    {
        var violations = ArchitectureAssert.ProductionTypes()
            .SelectMany(type => type.GetMethods(
                BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
            .Where(method => !method.IsSpecialName)
            .Where(method => method.DeclaringType?.FullName != "Program" || method.Name != "<Main>$")
            .Where(ReturnsTask)
            .Where(method => !method.Name.EndsWith("Async", StringComparison.Ordinal))
            .Where(method => method.Name is not "Main" and not "DisposeAsync" and not "InitializeAsync" and not "Dispose")
            .Select(method => $"{method.DeclaringType?.FullName}.{method.Name}")
            .ToList();

        AssertNoViolations(violations, "Methods returning Task or Task<T> must use the Async suffix.");
    }

    [Fact(DisplayName = "Synchronous methods must not use the Async suffix")]
    public void SynchronousMethods_MustNotEndWith_Async()
    {
        var violations = ArchitectureAssert.ProductionTypes()
            .SelectMany(type => type.GetMethods(
                BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
            .Where(method => !method.IsSpecialName)
            .Where(method => !ReturnsTask(method) && method.ReturnType != typeof(ValueTask) &&
                             !(method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition().Name.StartsWith("ValueTask", StringComparison.Ordinal)))
            .Where(method => method.Name.EndsWith("Async", StringComparison.Ordinal))
            .Select(method => $"{method.DeclaringType?.FullName}.{method.Name}")
            .ToList();

        AssertNoViolations(violations, "Do not use the Async suffix on synchronous methods.");
    }

    [Fact(DisplayName = "Identifiers must use Id rather than ID")]
    public void Identifiers_MustUse_Id_Not_ID()
    {
        var violations = ArchitectureAssert.ProductionTypes()
            .SelectMany(type => type.GetMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)
                .Where(member => _endsWithUppercaseId.IsMatch(member.Name))
                .Select(member => $"{type.FullName}.{member.Name}"))
            .ToList();

        AssertNoViolations(violations, "Use Id, not ID (naming-conventions.md).");
    }

    [Fact(DisplayName = "Public type names should match their file names")]
    public void PublicTypes_ShouldMatch_FileNames()
    {
        var fileNames = ProductionSource.CSharpFiles
            .Select(Path.GetFileNameWithoutExtension)
            .ToHashSet(StringComparer.Ordinal);

        var violations = ArchitectureAssert.ProductionTypes()
            .Where(type => type is { IsPublic: true, IsNested: false })
            .Where(type => type.Name is not "Program")
            .Where(type => !fileNames.Contains(type.Name))
            .Select(type => type.FullName ?? type.Name)
            .ToList();

        AssertNoViolations(violations, "File names should match the primary public type.");
    }

    [Fact(DisplayName = "MVC route templates must not embed HTTP verbs")]
    public void Routes_MustNotContain_HttpVerbs()
    {
        var routeName = "Microsoft.AspNetCore.Mvc.RouteAttribute";
        var httpMethodName = "Microsoft.AspNetCore.Mvc.HttpMethodAttribute";

        var violations = new List<string>();
        foreach (var type in ArchitectureAssert.ProductionTypes())
        {
            foreach (var attribute in type.GetCustomAttributes(inherit: true).Concat(
                         type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                             .SelectMany(method => method.GetCustomAttributes(inherit: true))))
            {
                var attributeType = attribute.GetType();
                if (attributeType.FullName != routeName &&
                    attributeType.BaseType?.FullName != httpMethodName &&
                    attributeType.FullName != "Microsoft.AspNetCore.Mvc.HttpGetAttribute" &&
                    attributeType.FullName != "Microsoft.AspNetCore.Mvc.HttpPostAttribute" &&
                    attributeType.FullName != "Microsoft.AspNetCore.Mvc.HttpPutAttribute" &&
                    attributeType.FullName != "Microsoft.AspNetCore.Mvc.HttpPatchAttribute" &&
                    attributeType.FullName != "Microsoft.AspNetCore.Mvc.HttpDeleteAttribute")
                {
                    continue;
                }

                var template = attributeType.GetProperty("Template")?.GetValue(attribute) as string;
                if (string.IsNullOrWhiteSpace(template) || template.Contains("[controller]", StringComparison.OrdinalIgnoreCase))
                    continue;

                if (_httpVerbInRoute.IsMatch(template.Replace("-", string.Empty, StringComparison.Ordinal)))
                    violations.Add($"{type.FullName}: {template}");
            }
        }

        AssertNoViolations(violations, "Avoid HTTP verbs in route templates; use REST resource paths.");
    }

    [Fact(DisplayName = "Production C# file names must be PascalCase")]
    public void ProductionCsFiles_MustBe_PascalCase()
    {
        var violations = ProductionSource.CSharpFiles
            .Select(Path.GetFileNameWithoutExtension)
            .Where(name => !Regex.IsMatch(name!, @"^[A-Z][A-Za-z0-9]*$"))
            .Select(name => name!)
            .ToList();

        AssertNoViolations(violations, "C# file names should be PascalCase and match the primary type (naming-conventions.md).");
    }

    [Fact(DisplayName = "Private fields must not use the m_ Hungarian prefix")]
    public void Fields_MustNotUse_MPrefix()
    {
        ProductionSource.AssertNoMatches(
            new Regex(@"\bm_[A-Za-z]", RegexOptions.Compiled),
            "Private fields use _camelCase, not m_ prefixes (naming-conventions.md).");
    }

    [Fact(DisplayName = "Constants must not use UPPER_SNAKE_CASE")]
    public void Constants_MustNotUse_UpperSnakeCase()
    {
        ProductionSource.AssertNoMatches(
            new Regex(@"\bconst\s+[^=;\n]+?\s+[A-Z][A-Z0-9]*_[A-Z0-9_]+\s*=", RegexOptions.Compiled),
            "Constants use PascalCase, not uppercase snake case (naming-conventions.md).");
    }

    [Fact(DisplayName = "Test classes that contain facts must end with Tests")]
    public void TestClasses_MustEndWith_Tests()
    {
        var violations = GetType().Assembly.GetTypes()
            .Where(type => type is { IsClass: true, IsPublic: true, IsAbstract: false })
            .Where(type => type.GetMethods().Any(method =>
                method.GetCustomAttributes(inherit: true).Any(attribute =>
                    attribute.GetType().Name is "FactAttribute" or "TheoryAttribute")))
            .Where(type => !type.Name.EndsWith("Tests", StringComparison.Ordinal))
            .Select(type => type.FullName ?? type.Name)
            .ToList();

        AssertNoViolations(violations, "Test classes should use the Tests suffix (naming-conventions.md).");
    }

    private static bool HasExtensionMethod(Type type) =>
        type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Any(method => method.IsDefined(typeof(System.Runtime.CompilerServices.ExtensionAttribute), inherit: false));

    private static bool ReturnsTask(MethodInfo method)
    {
        var returnType = method.ReturnType;
        if (returnType == typeof(Task) || returnType == typeof(ValueTask))
            return true;

        return returnType.IsGenericType &&
               (returnType.GetGenericTypeDefinition() == typeof(Task<>) ||
                returnType.GetGenericTypeDefinition() == typeof(ValueTask<>));
    }

    private static void AssertNoViolations(IReadOnlyCollection<string> violations, string rule)
    {
        if (violations.Count == 0)
            return;

        Assert.Fail($"{rule}{Environment.NewLine}Violations:{Environment.NewLine}{string.Join(Environment.NewLine, violations)}");
    }
}
