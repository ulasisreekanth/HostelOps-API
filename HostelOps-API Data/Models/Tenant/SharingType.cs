namespace HostelOps_API_Data.Models.Tenant;

public class SharingType
{
    // Primary Key (PK)
    public int SharingTypeId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int MaxOccupants { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    // Navigation Property
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    // Navigation Property
    public ICollection<Inquiry> Inquiries { get; set; } = new List<Inquiry>();
}