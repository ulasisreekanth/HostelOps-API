using NetArchTest.Rules;

namespace HostelOps_API_Unit_Tests.Architecture;

/// <summary>
/// Enforces Domain independence from infrastructure and presentation
/// (architecture.md sections 5.3, 6, 25, 37, 43, 52).
/// </summary>
public class DomainIndependenceTests
{
    [Fact(DisplayName = "Domain projects must not reference infrastructure or presentation packages")]
    public void Domain_MustNotReference_InfrastructurePackages()
    {
        ProjectReferenceRules.MustNotReferencePackages(
            LayerKind.Domain,
            ForbiddenDependencies.DomainPackagePrefixes);
    }

    [Fact(DisplayName = "Domain types must not depend on infrastructure or presentation namespaces")]
    public void DomainTypes_MustNotDependOn_InfrastructureNamespaces()
    {
        if (!ArchitectureAssert.HasLayer(LayerKind.Domain))
            return;

        var assemblies = ArchitectureAssemblies.For(LayerKind.Domain);
        var result = Types.InAssemblies(assemblies)
            .Should()
            .NotHaveDependencyOnAny(ForbiddenDependencies.DomainTypeNamespaces)
            .GetResult();

        ArchitectureAssert.Passes(
            result,
            "Domain types must remain independent of ASP.NET, EF Core, HTTP, cloud SDKs, logging, DI, and persistence attributes.");
    }

    [Fact(DisplayName = "Application projects must not reference persistence or cloud infrastructure packages")]
    public void Application_MustNotReference_PersistencePackages()
    {
        ProjectReferenceRules.MustNotReferencePackages(
            LayerKind.Application,
            ForbiddenDependencies.ApplicationPackagePrefixes);
    }

    [Fact(DisplayName = "Application types must not depend on EF Core or MVC/hosting types")]
    public void ApplicationTypes_MustNotDependOn_EfCoreOrMvc()
    {
        if (!ArchitectureAssert.HasLayer(LayerKind.Application))
            return;

        var assemblies = ArchitectureAssemblies.For(LayerKind.Application);
        var result = Types.InAssemblies(assemblies)
            .Should()
            .NotHaveDependencyOnAny(ForbiddenDependencies.ApplicationTypeNamespaces)
            .GetResult();

        ArchitectureAssert.Passes(
            result,
            "Application should coordinate use cases, not own persistence or HTTP pipeline types.");
    }

    [Fact(DisplayName = "Domain types must not use IServiceProvider (service locator)")]
    public void Domain_MustNotUse_ServiceLocator()
    {
        if (!ArchitectureAssert.HasLayer(LayerKind.Domain))
            return;

        var assemblies = ArchitectureAssemblies.For(LayerKind.Domain);
        var result = Types.InAssemblies(assemblies)
            .Should()
            .NotHaveDependencyOnAny(
                "System.IServiceProvider",
                "Microsoft.Extensions.DependencyInjection.IServiceScopeFactory")
            .GetResult();

        ArchitectureAssert.Passes(result, "Domain must not use a service locator.");
    }
}
