namespace HostelOps_API_Data.Models.Tenant;

public class RoomType
{
    // Primary Key (PK)
    public int RoomTypeId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Occupants { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    // Navigation Property
    // One RoomType can have many Reservations
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    // Navigation Property
    // One RoomType can have many Inquiries
    public ICollection<Inquiry> Inquiries { get; set; } = new List<Inquiry>();
}