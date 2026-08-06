namespace HostelOps_API_Data.Models.Tenant;

public class StaffRole
{
    // =========================
    // Primary Key (PK)
    // =========================
    public int StaffRoleId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    // Navigation Property
    // One Staff Role can have many Staff
    public ICollection<Staff> StaffMembers { get; set; } = new List<Staff>();
}