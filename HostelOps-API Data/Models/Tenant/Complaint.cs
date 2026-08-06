namespace HostelOps_API_Data.Models.Tenant;

public class Complaint
{
    // =========================
    // Primary Key (PK)
    // =========================
    public int ComplaintId { get; set; }

    // =========================
    // Foreign Key (FK)
    // Refers Residents.ResidentId
    // Nullable
    // =========================
    public int? ResidentId { get; set; }

    // =========================
    // Foreign Key (FK)
    // Refers Staff.StaffId
    // Nullable
    // =========================
    public int? StaffId { get; set; }

    public string Subject { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Priority { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime? ResolvedAt { get; set; }

    // Navigation Property
    public Resident? Resident { get; set; }

    // Navigation Property
    public Staff? Staff { get; set; }
}