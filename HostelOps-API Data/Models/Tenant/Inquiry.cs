namespace HostelOps_API_Data.Models.Tenant;

public class Inquiry
{
    // Primary Key (PK)
    public int InquiryId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public DateOnly PreferredCheckIn { get; set; }

    public DateOnly PreferredCheckOut { get; set; }

    // Foreign Key (FK)
    // Refers SharingTypes.SharingTypeId
    public int SharingTypeId { get; set; }

    // Foreign Key (FK)
    // Refers RoomTypes.RoomTypeId
    public int RoomTypeId { get; set; }

    public string Message { get; set; } = string.Empty;

    public string Source { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    // Navigation Properties

    public SharingType SharingType { get; set; } = null!;

    public RoomType RoomType { get; set; } = null!;

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}