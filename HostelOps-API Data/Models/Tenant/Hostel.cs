namespace HostelOps_API_Data.Models.Tenant;

public class Hostel
{
    // =========================
    // Primary Key (PK)
    // =========================
    public int HostelId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public string Pincode { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string LogoUrl { get; set; } = string.Empty;

    public string Timezone { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    // Navigation Property
    // One Hostel has many Buildings so it should be collection of Building
    public ICollection<Building> Buildings { get; set; } = new List<Building>();
}