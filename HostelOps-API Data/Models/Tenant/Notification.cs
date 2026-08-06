namespace HostelOps_API_Data.Models.Tenant;

public class Notification
{
    // =========================
    // Primary Key (PK)
    // =========================
    public long NotificationId { get; set; }

    // =========================
    // Foreign Key (FK)
    // Refers Staff.StaffId
    // Nullable
    // =========================
    public int? StaffId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public bool IsRead { get; set; }

    public DateTime CreatedAt { get; set; }

    // Navigation Property
    public Staff? Staff { get; set; }
}