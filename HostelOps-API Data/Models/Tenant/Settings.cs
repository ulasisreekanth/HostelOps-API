namespace HostelOps_API_Data.Models.Tenant;

public class Setting
{
    // =========================
    // Primary Key (PK)
    // =========================
    public int SettingId { get; set; }

    public string SettingKey { get; set; } = string.Empty;

    public string SettingValue { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}