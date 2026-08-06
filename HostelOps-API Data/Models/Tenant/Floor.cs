namespace HostelOps_API_Data.Models.Tenant;

public class Floor
{
    // Primary Key (PK)
    public int FloorId { get; set; }

    // Foreign Key (FK)
    // Refers Building.BuildingId
    public int BuildingId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int FloorNumber { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    // Navigation Property
    public Building Building { get; set; } = null!;

    // One Floor has many Rooms
    public ICollection<Room> Rooms { get; set; } = new List<Room>();
}