namespace HostelOps_API_Data.Models.Tenant;

public class Bed
{
    // Primary Key (PK)
    public int BedId { get; set; }

    // Foreign Key (FK)
    // Refers Room.RoomId
    public int RoomId { get; set; }

    // Foreign Key (FK)
    // Refers BedType.BedTypeId
    public int BedTypeId { get; set; }

    public string BedNumber { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    // Navigation Property
    public Room Room { get; set; } = null!;

    // Navigation Property
    public BedType BedType { get; set; } = null!;

    // One Bed can have many Stay Allocations
    public ICollection<StayAllocation> StayAllocations { get; set; } = new List<StayAllocation>();
}