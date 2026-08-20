namespace HostelOps_API_Unit_Tests.Architecture;

/// <summary>
/// Objectively forbidden technical dependencies for inner layers.
/// Keep this list data-driven so new platform packages can be added later.
/// </summary>
public static class ForbiddenDependencies
{
    public static readonly string[] DomainPackagePrefixes =
    [
        "Microsoft.AspNetCore",
        "Microsoft.EntityFrameworkCore",
        "Microsoft.Data.SqlClient",
        "System.Data.SqlClient",
        "Npgsql",
        "Dapper",
        "MongoDB",
        "StackExchange.Redis",
        "RabbitMQ",
        "MassTransit",
        "Hangfire",
        "Azure.",
        "Microsoft.Azure",
        "AWSSDK",
        "Amazon.",
        "Google.Cloud",
        "Microsoft.Extensions.Http"
    ];

    public static readonly string[] DomainTypeNamespaces =
    [
        "Microsoft.AspNetCore",
        "Microsoft.EntityFrameworkCore",
        "Microsoft.Data.SqlClient",
        "System.Data.SqlClient",
        "Npgsql",
        "Dapper",
        "MongoDB",
        "StackExchange.Redis",
        "RabbitMQ",
        "MassTransit",
        "Hangfire",
        "Azure",
        "Amazon",
        "Google.Cloud",
        "System.Net.Http",
        "Microsoft.Extensions.Logging",
        "Microsoft.Extensions.DependencyInjection",
        "Microsoft.Extensions.Configuration",
        "Microsoft.Extensions.Hosting",
        "Microsoft.FeatureManagement",
        "System.ComponentModel.DataAnnotations.Schema"
    ];

    public static readonly string[] ApplicationPackagePrefixes =
    [
        "Microsoft.EntityFrameworkCore",
        "Microsoft.Data.SqlClient",
        "System.Data.SqlClient",
        "Npgsql",
        "Dapper",
        "MongoDB",
        "StackExchange.Redis",
        "Azure.",
        "Microsoft.Azure",
        "AWSSDK",
        "Amazon.",
        "Hangfire"
    ];

    public static readonly string[] ApplicationTypeNamespaces =
    [
        "Microsoft.EntityFrameworkCore",
        "Microsoft.AspNetCore.Mvc",
        "Microsoft.AspNetCore.Builder",
        "Microsoft.AspNetCore.Hosting"
    ];

    public static bool MatchesPrefix(string packageId, IEnumerable<string> prefixes) =>
        prefixes.Any(prefix =>
            packageId.Equals(prefix.TrimEnd('.'), StringComparison.OrdinalIgnoreCase) ||
            packageId.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
}
