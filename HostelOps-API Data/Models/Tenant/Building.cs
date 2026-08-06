namespace HostelOps_API_Data.Models.Tenant;

public class Building
{
    // Primary Key (PK)
    public int BuildingId { get; set; }

    // Foreign Key (FK)
    // Refers Hostel.HostelId
    public int HostelId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    // Navigation Property
    // Building belongs to one Hostel
    public Hostel Hostel { get; set; } = null!;

    // One Building has many Floors
    public ICollection<Floor> Floors { get; set; } = new List<Floor>();
}