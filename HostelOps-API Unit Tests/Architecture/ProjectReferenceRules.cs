namespace HostelOps_API_Unit_Tests.Architecture;

internal static class ProjectReferenceRules
{
    public static void MustOnlyReference(
        LayerKind sourceLayer,
        params LayerKind[] allowedTargets)
    {
        if (!ArchitectureAssert.HasLayer(sourceLayer))
            return;

        var allowed = allowedTargets.ToHashSet();
        var violations = new List<string>();

        foreach (var project in ArchitectureSolution.ProjectsIn(sourceLayer))
        {
            foreach (var referencePath in project.ProjectReferencePaths)
            {
                if (IsTestProject(referencePath))
                {
                    violations.Add($"{project.Name} → {Path.GetFileNameWithoutExtension(referencePath)} (test project)");
                    continue;
                }

                var referenced = ArchitectureSolution.FindByPath(referencePath);
                if (referenced is null)
                {
                    violations.Add($"{project.Name} → {referencePath} (unknown production project)");
                    continue;
                }

                if (!allowed.Contains(referenced.Layer))
                {
                    violations.Add($"{project.Name} ({sourceLayer}) → {referenced.Name} ({referenced.Layer})");
                }
            }
        }

        AssertNoViolations(
            violations,
            $"{sourceLayer} projects may only reference: {string.Join(", ", allowedTargets)}.");
    }

    public static void MustNotReferencePackages(
        LayerKind sourceLayer,
        IReadOnlyList<string> forbiddenPrefixes)
    {
        if (!ArchitectureAssert.HasLayer(sourceLayer))
            return;

        var violations = ArchitectureSolution.ProjectsIn(sourceLayer)
            .SelectMany(project => project.PackageReferences
                .Where(package => ForbiddenDependencies.MatchesPrefix(package, forbiddenPrefixes))
                .Select(package => $"{project.Name} references package '{package}'"))
            .ToList();

        AssertNoViolations(
            violations,
            $"{sourceLayer} must not reference infrastructure/presentation packages.");
    }

    public static void ProductionMustNotReferenceTests()
    {
        var violations = ArchitectureSolution.ProductionProjects
            .SelectMany(project => project.ProjectReferencePaths
                .Where(IsTestProject)
                .Select(path => $"{project.Name} → {Path.GetFileNameWithoutExtension(path)}"))
            .ToList();

        AssertNoViolations(violations, "Production projects must not reference test projects.");
    }

    public static void NonPresentationMustNotReferencePresentation()
    {
        var violations = ArchitectureSolution.ProductionProjects
            .Where(project => project.Layer != LayerKind.Presentation)
            .SelectMany(project => project.ProjectReferencePaths
                .Select(ArchitectureSolution.FindByPath)
                .Where(referenced => referenced?.Layer == LayerKind.Presentation)
                .Select(referenced => $"{project.Name} → {referenced!.Name}"))
            .ToList();

        AssertNoViolations(
            violations,
            "Only the presentation/composition-root project may depend on the API/web project.");
    }

    private static bool IsTestProject(string path)
    {
        var name = Path.GetFileNameWithoutExtension(path);
        var directory = Path.GetFileName(Path.GetDirectoryName(path)) ?? string.Empty;
        return name.Contains("Test", StringComparison.OrdinalIgnoreCase) ||
               directory.Contains("Test", StringComparison.OrdinalIgnoreCase);
    }

    private static void AssertNoViolations(IReadOnlyCollection<string> violations, string rule)
    {
        if (violations.Count == 0)
            return;

        Assert.Fail(
            $"{rule}{Environment.NewLine}Violations:{Environment.NewLine}{string.Join(Environment.NewLine, violations)}");
    }
}
