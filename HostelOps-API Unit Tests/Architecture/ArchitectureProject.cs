namespace HostelOps_API_Unit_Tests.Architecture;

/// <summary>
/// Metadata for a production project discovered from the solution.
/// </summary>
public sealed class ArchitectureProject
{
    public required string Path { get; init; }
    public required string Name { get; init; }
    public required string AssemblyName { get; init; }
    public required string Sdk { get; init; }
    public required LayerKind Layer { get; init; }
    public required string? ModuleName { get; init; }
    public required IReadOnlyList<string> ProjectReferencePaths { get; init; }
    public required IReadOnlyList<string> PackageReferences { get; init; }

    public bool IsPresentation => Layer == LayerKind.Presentation;
    public bool IsDomain => Layer == LayerKind.Domain;
}
