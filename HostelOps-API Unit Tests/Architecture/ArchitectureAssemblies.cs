using System.Reflection;

namespace HostelOps_API_Unit_Tests.Architecture;

/// <summary>
/// Loads production assemblies copied into the test output directory.
/// </summary>
public static class ArchitectureAssemblies
{
    public static IReadOnlyList<Assembly> All { get; } = Load();

    public static Assembly[] For(LayerKind layer)
    {
        var names = ArchitectureSolution.ProjectsIn(layer)
            .Select(p => p.AssemblyName)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        return All.Where(a => a.GetName().Name is string name && names.Contains(name)).ToArray();
    }

    private static IReadOnlyList<Assembly> Load()
    {
        var outputDirectory = Path.GetDirectoryName(typeof(ArchitectureAssemblies).Assembly.Location)
            ?? throw new InvalidOperationException("Unable to determine test output directory.");

        var loaded = new List<Assembly>();
        foreach (var project in ArchitectureSolution.ProductionProjects)
        {
            var dllPath = Path.Combine(outputDirectory, project.AssemblyName + ".dll");
            if (!File.Exists(dllPath))
            {
                throw new FileNotFoundException(
                    $"Production assembly '{project.AssemblyName}.dll' was not found in the test output directory. " +
                    $"Ensure '{project.Name}' is referenced by the unit test project and has been built.",
                    dllPath);
            }

            loaded.Add(LoadAssembly(project.AssemblyName, dllPath));
        }

        return loaded;
    }

    private static Assembly LoadAssembly(string assemblyName, string dllPath)
    {
        var existing = AppDomain.CurrentDomain.GetAssemblies()
            .FirstOrDefault(a =>
                string.Equals(a.GetName().Name, assemblyName, StringComparison.OrdinalIgnoreCase));

        return existing ?? Assembly.LoadFrom(dllPath);
    }
}
