namespace HostelOps_API_Unit_Tests.Architecture;

/// <summary>
/// Modular architecture and Shared Kernel rules from architecture.md sections 17–19.
/// </summary>
public class ModuleAndSharedKernelTests
{
    [Fact(DisplayName = "Modules must not have circular project dependencies")]
    public void Modules_MustNotHave_CircularDependencies()
    {
        var moduleProjects = ArchitectureAssert.ModuleProjects();
        if (moduleProjects.Count == 0)
            return;
        var graph = moduleProjects
            .Where(p => p.ModuleName is not null)
            .GroupBy(p => p.ModuleName!, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(
                g => g.Key,
                g => g.SelectMany(project => project.ProjectReferencePaths
                        .Select(ArchitectureSolution.FindByPath)
                        .Where(referenced =>
                            referenced?.ModuleName is not null &&
                            !referenced.ModuleName.Equals(g.Key, StringComparison.OrdinalIgnoreCase))
                        .Select(referenced => referenced!.ModuleName!))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList(),
                StringComparer.OrdinalIgnoreCase);

        var cycle = FindCycle(graph);
        Assert.True(
            cycle is null,
            $"Avoid circular module dependencies (architecture.md §18). Cycle: {cycle}");
    }

    [Fact(DisplayName = "A module may only reference another module through Contracts")]
    public void Modules_MayOnlyReference_OtherModuleContracts()
    {
        var moduleProjects = ArchitectureAssert.ModuleProjects();
        if (moduleProjects.Count == 0)
            return;
        var violations = new List<string>();

        foreach (var project in moduleProjects)
        {
            foreach (var referenced in project.ProjectReferencePaths.Select(ArchitectureSolution.FindByPath))
            {
                if (referenced?.ModuleName is null)
                    continue;

                if (referenced.ModuleName.Equals(project.ModuleName, StringComparison.OrdinalIgnoreCase))
                    continue;

                if (referenced.Layer != LayerKind.Contracts)
                {
                    violations.Add(
                        $"{project.Name} ({project.ModuleName}) → {referenced.Name} ({referenced.ModuleName}/{referenced.Layer}). " +
                        "Cross-module references must use Contracts.");
                }
            }
        }

        Assert.True(
            violations.Count == 0,
            "Modules must communicate through contracts/events, not internal projects:" +
            Environment.NewLine +
            string.Join(Environment.NewLine, violations));
    }

    [Fact(DisplayName = "SharedKernel must not reference module projects")]
    public void SharedKernel_MustNotReference_Modules()
    {
        if (!ArchitectureAssert.HasLayer(LayerKind.SharedKernel))
            return;

        var violations = ArchitectureSolution.ProjectsIn(LayerKind.SharedKernel)
            .SelectMany(project => project.ProjectReferencePaths
                .Select(ArchitectureSolution.FindByPath)
                .Where(referenced => referenced?.ModuleName is not null)
                .Select(referenced => $"{project.Name} → {referenced!.Name}"))
            .ToList();

        Assert.True(
            violations.Count == 0,
            "Do not put or pull module-specific projects into SharedKernel:" +
            Environment.NewLine +
            string.Join(Environment.NewLine, violations));
    }

    private static string? FindCycle(IReadOnlyDictionary<string, List<string>> graph)
    {
        var visiting = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var stack = new Stack<string>();

        foreach (var node in graph.Keys)
        {
            if (Visit(node))
                return string.Join(" → ", stack.Reverse());
        }

        return null;

        bool Visit(string node)
        {
            if (visited.Contains(node))
                return false;
            if (!visiting.Add(node))
                return true;

            stack.Push(node);
            if (graph.TryGetValue(node, out var neighbors))
            {
                foreach (var neighbor in neighbors)
                {
                    if (Visit(neighbor))
                        return true;
                }
            }

            stack.Pop();
            visiting.Remove(node);
            visited.Add(node);
            return false;
        }
    }
}
