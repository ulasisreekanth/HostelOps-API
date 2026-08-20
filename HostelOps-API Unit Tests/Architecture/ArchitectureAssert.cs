using System.Reflection;
using System.Runtime.CompilerServices;
using NetArchTest.Rules;

namespace HostelOps_API_Unit_Tests.Architecture;

internal static class ArchitectureAssert
{
    public static void Passes(TestResult result, string rule)
    {
        if (result.IsSuccessful)
            return;

        var violations = result.FailingTypes?
            .Select(type => type.FullName ?? type.Name)
            .ToArray() ?? [];

        Assert.Fail(
            $"{rule}{Environment.NewLine}Violations:{Environment.NewLine}{string.Join(Environment.NewLine, violations)}");
    }

    public static bool HasLayer(LayerKind layer) =>
        ArchitectureSolution.ProjectsIn(layer).Count > 0;

    public static IReadOnlyList<ArchitectureProject> ModuleProjects() =>
        ArchitectureSolution.ProductionProjects
            .Where(p => p.ModuleName is not null)
            .ToList();

    public static IEnumerable<Type> ProductionTypes()
    {
        return ArchitectureAssemblies.All
            .SelectMany(assembly =>
            {
                try
                {
                    return assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException ex)
                {
                    return ex.Types.Where(t => t is not null)!;
                }
            })
            .Where(t => t is { IsPublic: true } or { IsNotPublic: true })
            .Where(t => t is not null && !IsCompilerGenerated(t))!;
    }

    public static IEnumerable<Type> TypesFrom(LayerKind layer)
    {
        var assemblyNames = ArchitectureSolution.ProjectsIn(layer)
            .Select(p => p.AssemblyName)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        return ProductionTypes()
            .Where(t => t.Assembly.GetName().Name is string name && assemblyNames.Contains(name));
    }

    public static ArchitectureProject? ProjectFor(Type type)
    {
        var assemblyName = type.Assembly.GetName().Name;
        return assemblyName is null
            ? null
            : ArchitectureSolution.FindByAssemblyName(assemblyName);
    }

    public static bool ImplementsGenericInterface(Type type, string genericTypeFullName)
    {
        return GetInterfaces(type).Any(i =>
            i.IsGenericType &&
            i.GetGenericTypeDefinition().FullName == genericTypeFullName);
    }

    public static bool Inherits(Type type, string baseTypeFullName)
    {
        for (var current = type; current is not null; current = current.BaseType)
        {
            if (string.Equals(current.FullName, baseTypeFullName, StringComparison.Ordinal))
                return true;
        }

        return false;
    }

    private static IEnumerable<Type> GetInterfaces(Type type)
    {
        try
        {
            return type.GetInterfaces();
        }
        catch
        {
            return [];
        }
    }

    private static bool IsCompilerGenerated(Type type) =>
        type.Name.Contains('<', StringComparison.Ordinal) ||
        type.IsDefined(typeof(CompilerGeneratedAttribute), inherit: false);
}
