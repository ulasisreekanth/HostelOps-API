using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
public class Reservation
{
    /// <summary>
    /// Primary key for the Reservation entity, uniquely identifying each reservation record.
    /// </summary>
    [Key]
    public Guid ReservationId { get; set; }

    /// <summary>
    /// Foreign key referencing the Resident entity, indicating the resident associated with this reservation. Nullable to allow for reservations without a specific resident.
    /// </summary>
    [Required]
    public Guid? ResidentId { get; set; }

    /// <summary>
    /// Foreign key referencing the Inquiry entity, indicating the inquiry associated with this reservation. Nullable to allow for reservations without a specific inquiry.
    /// </summary>
    [Required]
    public Guid? InquiryId { get; set; }

    /// <summary>
    /// The start date of the reservation, indicating when the reserved stay begins.
    /// </summary>
    [Required]
    public DateOnly ReservedFrom { get; set; }

    /// <summary>
    /// The end date of the reservation, indicating when the reserved stay ends.
    /// </summary>
    [Required]
    public DateOnly ReservedTo { get; set; }

    /// <summary>
    /// Foreign key referencing the RoomType entity, indicating the type of room associated with this reservation.
    /// </summary>
    [Required]
    public Guid RoomTypeId { get; set; }

    /// <summary>
    /// Foreign key referencing the SharingType entity, indicating the type of sharing arrangement associated with this reservation.
    /// </summary>
    [Required]
    public Guid SharingTypeId { get; set; }

    /// <summary>
    /// The current status of the reservation, represented by the ReservationStatus enum (e.g., Pending, Confirmed, Cancelled).
    /// </summary>
    [Required]
    public ReservationStatus Status { get; set; }

    /// <summary>
    /// The advance amount paid for the reservation, represented as a decimal value with a precision of 10 and scale of 2.
    /// </summary>
    [Column(TypeName = "decimal(10,2)")]
    public decimal AdvanceAmount { get; set; }

    /// <summary>
    /// The date on which the reservation was created, represented as a DateOnly value.
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The date on which the reservation was last updated, represented as a DateOnly value.
    /// </summary>
    [Required]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Navigation property to the associated Resident entity, representing the relationship between Reservation and Resident.
    /// </summary>
    [ForeignKey("ResidentId")]
    public Resident? Resident { get; set; }

    /// <summary>
    /// Navigation property to the associated Inquiry entity, representing the relationship between Reservation and Inquiry.
    /// </summary>
    [ForeignKey("InquiryId")]
    public Inquiry? Inquiry { get; set; }

    /// <summary>
    /// Navigation property to the associated RoomType entity, representing the relationship between Reservation and RoomType.
    /// </summary>
    [ForeignKey("RoomTypeId")]
    public RoomType RoomType { get; set; } = null!;

    /// <summary>
    /// Navigation property to the associated SharingType entity, representing the relationship between Reservation and SharingType.
    /// </summary>
    [ForeignKey("SharingTypeId")]
    public SharingType SharingType { get; set; } = null!;

    /// <summary>
    /// Navigation property to the collection of associated StayAllocation entities, representing the stay allocations linked to this reservation.
    /// </summary>
    public ICollection<StayAllocation> StayAllocations { get; set; } = new List<StayAllocation>();
}
}