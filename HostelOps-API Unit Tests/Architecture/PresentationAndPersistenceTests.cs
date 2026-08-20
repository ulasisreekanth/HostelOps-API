using NetArchTest.Rules;

namespace HostelOps_API_Unit_Tests.Architecture;

/// <summary>
/// Presentation and persistence boundary rules from architecture.md
/// sections 5.1, 23, 24, 25, 31, and 57.
/// </summary>
public class PresentationAndPersistenceTests
{
    private const string ControllerBase = "Microsoft.AspNetCore.Mvc.ControllerBase";
    private const string DbContext = "Microsoft.EntityFrameworkCore.DbContext";
    private const string EntityTypeConfiguration = "Microsoft.EntityFrameworkCore.IEntityTypeConfiguration`1";

    [Fact(DisplayName = "Controller types must live in the presentation layer")]
    public void Controllers_MustLiveIn_Presentation()
    {
        var violations = ArchitectureAssert.ProductionTypes()
            .Where(type => ArchitectureAssert.Inherits(type, ControllerBase))
            .Select(type => new
            {
                Type = type,
                Project = ArchitectureAssert.ProjectFor(type)
            })
            .Where(x => x.Project is null || x.Project.Layer != LayerKind.Presentation)
            .Select(x => $"{x.Type.FullName} in {x.Project?.Name ?? x.Type.Assembly.GetName().Name}")
            .ToList();

        Assert.True(
            violations.Count == 0,
            "Controllers / endpoints belong in the presentation layer:" +
            Environment.NewLine +
            string.Join(Environment.NewLine, violations));
    }

    [Fact(DisplayName = "Controllers must not depend on EF Core")]
    public void Controllers_MustNotDependOn_EntityFramework()
    {
        var hasControllers = ArchitectureAssert.ProductionTypes()
            .Any(type => ArchitectureAssert.Inherits(type, ControllerBase));

        if (!hasControllers)
            return;

        var result = Types.InAssemblies(ArchitectureAssemblies.All)
            .That()
            .Inherit(typeof(Microsoft.AspNetCore.Mvc.ControllerBase))
            .Should()
            .NotHaveDependencyOnAny("Microsoft.EntityFrameworkCore")
            .GetResult();

        ArchitectureAssert.Passes(
            result,
            "Controllers must not access the database directly (architecture.md §31, §57).");
    }

    [Fact(DisplayName = "DbContext types must live in Infrastructure")]
    public void DbContext_MustLiveIn_Infrastructure()
    {
        var violations = ArchitectureAssert.ProductionTypes()
            .Where(type => ArchitectureAssert.Inherits(type, DbContext))
            .Where(type => ArchitectureAssert.ProjectFor(type)?.Layer != LayerKind.Infrastructure)
            .Select(type => $"{type.FullName} in {ArchitectureAssert.ProjectFor(type)?.Name ?? type.Assembly.GetName().Name}")
            .ToList();

        Assert.True(
            violations.Count == 0,
            "EF Core DbContext types belong in Infrastructure:" +
            Environment.NewLine +
            string.Join(Environment.NewLine, violations));
    }

    [Fact(DisplayName = "IEntityTypeConfiguration types must live in Infrastructure")]
    public void EntityTypeConfigurations_MustLiveIn_Infrastructure()
    {
        var violations = ArchitectureAssert.ProductionTypes()
            .Where(type => ArchitectureAssert.ImplementsGenericInterface(type, EntityTypeConfiguration))
            .Where(type => ArchitectureAssert.ProjectFor(type)?.Layer != LayerKind.Infrastructure)
            .Select(type => $"{type.FullName} in {ArchitectureAssert.ProjectFor(type)?.Name ?? type.Assembly.GetName().Name}")
            .ToList();

        Assert.True(
            violations.Count == 0,
            "EF Core IEntityTypeConfiguration types belong in Infrastructure:" +
            Environment.NewLine +
            string.Join(Environment.NewLine, violations));
    }

    [Fact(DisplayName = "Controllers must not return or accept Domain types")]
    public void Controllers_MustNotExpose_DomainTypes()
    {
        if (!ArchitectureAssert.HasLayer(LayerKind.Domain))
            return;

        var domainAssemblies = ArchitectureAssemblies.For(LayerKind.Domain)
            .Select(a => a.GetName().Name)
            .Where(name => name is not null)
            .Cast<string>()
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        var violations = ArchitectureAssert.ProductionTypes()
            .Where(type => ArchitectureAssert.Inherits(type, ControllerBase))
            .SelectMany(controller => DescribeDomainLeaks(controller, domainAssemblies))
            .ToList();

        Assert.True(
            violations.Count == 0,
            "Do not expose Domain types through API signatures (architecture.md §23):" +
            Environment.NewLine +
            string.Join(Environment.NewLine, violations));
    }

    [Fact(DisplayName = "Repository implementations must not live in Domain")]
    public void RepositoryImplementations_MustNotLiveIn_Domain()
    {
        if (!ArchitectureAssert.HasLayer(LayerKind.Domain))
            return;

        var violations = ArchitectureAssert.TypesFrom(LayerKind.Domain)
            .Where(type => type is { IsClass: true, IsAbstract: false, IsInterface: false })
            .Where(type => type.Name.EndsWith("Repository", StringComparison.OrdinalIgnoreCase))
            .Select(type => type.FullName ?? type.Name)
            .ToList();

        Assert.True(
            violations.Count == 0,
            "Repository implementations belong in Infrastructure; Domain may only contain repository abstractions:" +
            Environment.NewLine +
            string.Join(Environment.NewLine, violations));
    }

    [Fact(DisplayName = "IGenericRepository is not allowed")]
    public void GenericRepository_MustNotExist()
    {
        var violations = ArchitectureAssert.ProductionTypes()
            .Where(type =>
                type.Name is "IGenericRepository" ||
                type.Name.StartsWith("IGenericRepository`", StringComparison.Ordinal) ||
                type.Name is "GenericRepository" ||
                type.Name.StartsWith("GenericRepository`", StringComparison.Ordinal))
            .Select(type => type.FullName ?? type.Name)
            .ToList();

        Assert.True(
            violations.Count == 0,
            "Avoid generic repositories (architecture.md §24):" +
            Environment.NewLine +
            string.Join(Environment.NewLine, violations));
    }

    [Fact(DisplayName = "Controllers should use constructor injection rather than IServiceProvider")]
    public void Controllers_MustNotTake_IServiceProvider()
    {
        var violations = ArchitectureAssert.ProductionTypes()
            .Where(type => ArchitectureAssert.Inherits(type, ControllerBase))
            .Where(UsesServiceProvider)
            .Select(type => type.FullName ?? type.Name)
            .ToList();

        Assert.True(
            violations.Count == 0,
            "Avoid service locator in controllers (architecture.md §39):" +
            Environment.NewLine +
            string.Join(Environment.NewLine, violations));
    }

    private static IEnumerable<string> DescribeDomainLeaks(Type controller, HashSet<string> domainAssemblies)
    {
        foreach (var method in controller.GetMethods(
                     System.Reflection.BindingFlags.Instance |
                     System.Reflection.BindingFlags.Public |
                     System.Reflection.BindingFlags.DeclaredOnly))
        {
            foreach (var type in EnumerateSignatureTypes(method.ReturnType)
                         .Concat(method.GetParameters().SelectMany(p => EnumerateSignatureTypes(p.ParameterType))))
            {
                var assemblyName = type.Assembly.GetName().Name;
                if (assemblyName is not null && domainAssemblies.Contains(assemblyName))
                    yield return $"{controller.Name}.{method.Name} uses Domain type {type.FullName}";
            }
        }
    }

    private static IEnumerable<Type> EnumerateSignatureTypes(Type type)
    {
        yield return type;

        if (type.IsGenericType)
        {
            foreach (var argument in type.GetGenericArguments())
            {
                foreach (var nested in EnumerateSignatureTypes(argument))
                    yield return nested;
            }
        }

        if (type.IsArray)
        {
            foreach (var nested in EnumerateSignatureTypes(type.GetElementType()!))
                yield return nested;
        }
    }

    private static bool UsesServiceProvider(Type controller)
    {
        return controller
            .GetConstructors()
            .SelectMany(ctor => ctor.GetParameters())
            .Any(parameter =>
                parameter.ParameterType == typeof(IServiceProvider) ||
                parameter.ParameterType.FullName == "Microsoft.Extensions.DependencyInjection.IServiceScopeFactory");
    }
}
