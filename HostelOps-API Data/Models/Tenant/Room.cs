namespace HostelOps_API_Data.Models.Tenant;

public class Room
{
    // Primary Key (PK)
    public int RoomId { get; set; }

    // Foreign Key (FK)
    // Refers Floor.FloorId
    public int FloorId { get; set; }

    public string RoomNumber { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal AreaSqft { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    // Navigation Property
    public Floor Floor { get; set; } = null!;

    // One Room has many Beds
    public ICollection<Bed> Beds { get; set; } = new List<Bed>();
}