namespace HostelOps_API_Unit_Tests.Architecture;

/// <summary>
/// Enforces the inward dependency rule from architecture.md sections 3, 5, and 6.
/// Presentation is the composition root and may reference inner layers including Infrastructure.
/// </summary>
public class LayerDependencyTests
{
    [Fact(DisplayName = "Production projects must not reference test projects")]
    public void ProductionProjects_MustNotReference_TestProjects()
    {
        ProjectReferenceRules.ProductionMustNotReferenceTests();
    }

    [Fact(DisplayName = "Non-presentation projects must not depend on the API/web project")]
    public void NonPresentationProjects_MustNotReference_Presentation()
    {
        ProjectReferenceRules.NonPresentationMustNotReferencePresentation();
    }

    [Fact(DisplayName = "Domain may only reference Domain, SharedKernel, and Contracts")]
    public void Domain_MustOnlyReference_InnerAbstractions()
    {
        ProjectReferenceRules.MustOnlyReference(
            LayerKind.Domain,
            LayerKind.Domain,
            LayerKind.SharedKernel,
            LayerKind.Contracts);
    }

    [Fact(DisplayName = "Application may only reference Domain, Application, SharedKernel, and Contracts")]
    public void Application_MustOnlyReference_InnerLayers()
    {
        ProjectReferenceRules.MustOnlyReference(
            LayerKind.Application,
            LayerKind.Application,
            LayerKind.Domain,
            LayerKind.SharedKernel,
            LayerKind.Contracts);
    }

    [Fact(DisplayName = "Infrastructure may not reference Presentation")]
    public void Infrastructure_MustNotReference_Presentation()
    {
        if (!ArchitectureAssert.HasLayer(LayerKind.Infrastructure))
            return;
        ProjectReferenceRules.MustOnlyReference(
            LayerKind.Infrastructure,
            LayerKind.Infrastructure,
            LayerKind.Application,
            LayerKind.Domain,
            LayerKind.SharedKernel,
            LayerKind.Contracts,
            LayerKind.Unknown);
    }

    [Fact(DisplayName = "Contracts may only reference Contracts and SharedKernel")]
    public void Contracts_MustOnlyReference_SharedAbstractions()
    {
        ProjectReferenceRules.MustOnlyReference(
            LayerKind.Contracts,
            LayerKind.Contracts,
            LayerKind.SharedKernel);
    }

    [Fact(DisplayName = "SharedKernel must not reference outer business layers")]
    public void SharedKernel_MustNotReference_OuterLayers()
    {
        ProjectReferenceRules.MustOnlyReference(
            LayerKind.SharedKernel,
            LayerKind.SharedKernel);
    }
}
