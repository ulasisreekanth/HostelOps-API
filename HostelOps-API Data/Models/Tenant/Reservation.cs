namespace HostelOps_API_Data.Models.Tenant;

public class Reservation
{
    // =========================
    // Primary Key (PK)
    // =========================
    public int ReservationId { get; set; }

    // =========================
    // Foreign Key (FK)
    // Refers Residents.ResidentId
    // Nullable because reservation can be created from Inquiry
    // =========================
    public int? ResidentId { get; set; }

    // =========================
    // Foreign Key (FK)
    // Refers Inquiries.InquiryId
    // Nullable because existing resident can reserve directly
    // =========================
    public int? InquiryId { get; set; }

    // =========================
    // Foreign Key (FK)
    // Refers RoomTypes.RoomTypeId
    // =========================
    public int RoomTypeId { get; set; }

    // =========================
    // Foreign Key (FK)
    // Refers SharingTypes.SharingTypeId
    // =========================
    public int SharingTypeId { get; set; }

    public DateOnly ReservedFrom { get; set; }

    public DateOnly ReservedTo { get; set; }

    public string Status { get; set; } = string.Empty;

    public decimal AdvanceAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    // Navigation Properties

    public Resident? Resident { get; set; }

    public Inquiry? Inquiry { get; set; }

    public RoomType RoomType { get; set; } = null!;

    public SharingType SharingType { get; set; } = null!;

    public ICollection<StayAllocation> StayAllocations { get; set; } = new List<StayAllocation>();
}