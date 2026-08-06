namespace HostelOps_API_Data.Models.Tenant;

public class AuditLog
{
    // =========================
    // Primary Key (PK)
    // =========================
    public long AuditLogId { get; set; }

    public int UserId { get; set; }

    public string Action { get; set; } = string.Empty;

    public string Entity { get; set; } = string.Empty;

    public int? EntityId { get; set; }

    public string Details { get; set; } = string.Empty;

    public string IpAddress { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}