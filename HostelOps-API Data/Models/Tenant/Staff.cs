namespace HostelOps_API_Data.Models.Tenant;

public class Staff
{
    // =========================
    // Primary Key (PK)
    // =========================
    public int StaffId { get; set; }

    // =========================
    // Foreign Key (FK)
    // Refers StaffRoles.StaffRoleId
    // =========================
    public int StaffRoleId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public DateOnly DateOfBirth { get; set; }

    public string Gender { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public decimal Salary { get; set; }

    public DateOnly JoinDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    // Navigation Properties

    public StaffRole StaffRole { get; set; } = null!;

    public ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();

    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}