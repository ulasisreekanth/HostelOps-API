namespace HostelOps_API_Data.Models.Tenant;

public class Vendor
{
    // =========================
    // Primary Key (PK)
    // =========================
    public int VendorId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string ContactPerson { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public string Pincode { get; set; } = string.Empty;

    public string GstNumber { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    // Navigation Property
    // One Vendor can have many Expenses
    public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}