using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace HostelOps_API_Unit_Tests.Architecture;

/// <summary>
/// Discovers production projects in the repository and classifies them into
/// architecture layers so additional rules can be added without hard-coding
/// today's project names.
/// </summary>
public static class ArchitectureSolution
{
    public static string Root { get; } = FindRoot();

    public static IReadOnlyList<ArchitectureProject> ProductionProjects { get; } =
        DiscoverProductionProjects();

    public static IReadOnlyList<ArchitectureProject> ProjectsIn(LayerKind layer) =>
        ProductionProjects.Where(p => p.Layer == layer).ToList();

    public static ArchitectureProject? FindByPath(string path)
    {
        var fullPath = Path.GetFullPath(path);
        return ProductionProjects.FirstOrDefault(p =>
            string.Equals(p.Path, fullPath, StringComparison.OrdinalIgnoreCase));
    }

    public static ArchitectureProject? FindByAssemblyName(string assemblyName) =>
        ProductionProjects.FirstOrDefault(p =>
            string.Equals(p.AssemblyName, assemblyName, StringComparison.OrdinalIgnoreCase));

    private static string FindRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null)
        {
            var hasSolution =
                directory.GetFiles("*.slnx").Length > 0 ||
                directory.GetFiles("*.sln").Length > 0;
            var hasArchitectureDoc = File.Exists(
                Path.Combine(directory.FullName, ".github", "instructions", "architecture.md"));

            if (hasSolution && hasArchitectureDoc)
                return directory.FullName;

            directory = directory.Parent;
        }

        throw new InvalidOperationException(
            "Could not locate the repository root (expected a solution file and .github/instructions/architecture.md).");
    }

    private static IReadOnlyList<ArchitectureProject> DiscoverProductionProjects()
    {
        return Directory.EnumerateFiles(Root, "*.csproj", SearchOption.AllDirectories)
            .Where(IsProductionProjectPath)
            .Select(ParseProject)
            .OrderBy(p => p.Name, StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    private static bool IsProductionProjectPath(string path)
    {
        var segments = path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        if (segments.Any(s => s is "bin" or "obj"))
            return false;

        var fileName = Path.GetFileNameWithoutExtension(path);
        var directoryName = Path.GetFileName(Path.GetDirectoryName(path)) ?? string.Empty;
        return !IsTestName(fileName) && !IsTestName(directoryName);
    }

    private static bool IsTestName(string name) =>
        name.Contains("Test", StringComparison.OrdinalIgnoreCase);

    private static ArchitectureProject ParseProject(string path)
    {
        var fullPath = Path.GetFullPath(path);
        var document = XDocument.Load(fullPath);
        var projectName = Path.GetFileNameWithoutExtension(fullPath);
        var sdk = document.Root?.Attribute("Sdk")?.Value ?? string.Empty;
        var assemblyName =
            GetProperty(document, "AssemblyName") ?? projectName;

        var directory = Path.GetDirectoryName(fullPath)!;
        var projectReferences = document
            .Descendants()
            .Where(e => e.Name.LocalName == "ProjectReference")
            .Select(e => e.Attribute("Include")?.Value)
            .Where(include => !string.IsNullOrWhiteSpace(include))
            .Select(include => Path.GetFullPath(Path.Combine(directory, include!)))
            .ToList();

        var packageReferences = document
            .Descendants()
            .Where(e => e.Name.LocalName == "PackageReference")
            .Select(e => e.Attribute("Include")?.Value)
            .Where(include => !string.IsNullOrWhiteSpace(include))
            .Select(include => include!)
            .ToList();

        var moduleName = TryGetModuleName(projectName);
        var layer = Classify(projectName, sdk);

        return new ArchitectureProject
        {
            Path = fullPath,
            Name = projectName,
            AssemblyName = assemblyName,
            Sdk = sdk,
            Layer = layer,
            ModuleName = moduleName,
            ProjectReferencePaths = projectReferences,
            PackageReferences = packageReferences
        };
    }

    private static string? GetProperty(XDocument document, string name) =>
        document.Descendants()
            .FirstOrDefault(e => e.Name.LocalName == name)
            ?.Value
            ?.Trim();

    private static LayerKind Classify(string projectName, string sdk)
    {
        if (sdk.Contains(".Web", StringComparison.OrdinalIgnoreCase))
            return LayerKind.Presentation;

        if (HasLayerSegment(projectName, "Domain"))
            return LayerKind.Domain;

        if (HasLayerSegment(projectName, "Application"))
            return LayerKind.Application;

        if (HasLayerSegment(projectName, "Infrastructure"))
            return LayerKind.Infrastructure;

        if (HasLayerSegment(projectName, "Contracts"))
            return LayerKind.Contracts;

        if (HasLayerSegment(projectName, "SharedKernel") ||
            HasLayerSegment(projectName, "Shared.Kernel"))
            return LayerKind.SharedKernel;

        return LayerKind.Unknown;
    }

    private static bool HasLayerSegment(string projectName, string layer)
    {
        var normalized = NormalizeName(projectName);
        var layerNormalized = NormalizeName(layer);
        return Regex.IsMatch(
            normalized,
            $@"(^|\.){Regex.Escape(layerNormalized)}(\.|$)",
            RegexOptions.IgnoreCase);
    }

    private static string? TryGetModuleName(string projectName)
    {
        var normalized = NormalizeName(projectName);
        var match = Regex.Match(
            normalized,
            @"\.Modules\.([^.]+)",
            RegexOptions.IgnoreCase);
        return match.Success ? match.Groups[1].Value : null;
    }

    private static string NormalizeName(string name) =>
        name.Replace(' ', '.').Replace('-', '.');
}
