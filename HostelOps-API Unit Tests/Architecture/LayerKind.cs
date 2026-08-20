namespace HostelOps_API_Unit_Tests.Architecture;

/// <summary>
/// Clean Architecture / DDD layer classification from architecture.md.
/// Classification is based on project naming and SDK, not folder heuristics.
/// </summary>
public enum LayerKind
{
    Unknown = 0,
    Domain,
    Application,
    Infrastructure,
    Presentation,
    Contracts,
    SharedKernel
}
