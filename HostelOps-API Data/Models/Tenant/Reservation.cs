using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class Reservation
    {
        [Key]
        //Primary key for the Reservation entity, uniquely identifying each reservation record.
        public Guid ReservationId { get; set; }

        [Required]
        //Foreign key referencing the Resident entity, indicating the resident associated with this reservation. Nullable to allow for reservations without a specific resident.
        public Guid? ResidentId { get; set; }

        [Required]
        //Foreign key referencing the Inquiry entity, indicating the inquiry associated with this reservation. Nullable to allow for reservations without a specific inquiry.
        public Guid? InquiryId { get; set; }

        [Required]
        //The start date of the reservation, indicating when the reserved stay begins.
        public DateOnly ReservedFrom { get; set; }

        [Required]
        //The end date of the reservation, indicating when the reserved stay ends.
        public DateOnly ReservedTo { get; set; }

        [Required]
        //Foreign key referencing the RoomType entity, indicating the type of room associated with this reservation.
        public Guid RoomTypeId { get; set; }

        [Required]
        //Foreign key referencing the SharingType entity, indicating the type of sharing arrangement associated with this reservation.
        public Guid SharingTypeId { get; set; }

        [Required]
        //The current status of the reservation, represented by the ReservationStatus enum (e.g., Pending, Confirmed, Cancelled).
        public ReservationStatus Status { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        //The advance amount paid for the reservation, represented as a decimal value with a precision of 10 and scale of 2.
        public decimal AdvanceAmount { get; set; }

        [Required]
        //The date on which the reservation was created, represented as a DateOnly value.
        public DateTime CreatedAt { get; set; }

        [Required]
        //The date on which the reservation was last updated, represented as a DateOnly value.
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("ResidentId")]
        //Navigation property to the associated Resident entity, representing the relationship between Reservation and Resident.
        public Resident? Resident { get; set; }

        [ForeignKey("InquiryId")]
        //Navigation property to the associated Inquiry entity, representing the relationship between Reservation and Inquiry.
        public Inquiry? Inquiry { get; set; }

        [ForeignKey("RoomTypeId")]
        //Navigation property to the associated RoomType entity, representing the relationship between Reservation and RoomType.
        public RoomType RoomType { get; set; } = null!;

        [ForeignKey("SharingTypeId")]
        //Navigation property to the associated SharingType entity, representing the relationship between Reservation and SharingType.
        public SharingType SharingType { get; set; } = null!;

        //Navigation property to the collection of associated StayAllocation entities, representing the stay allocations linked to this reservation.
        public ICollection<StayAllocation> StayAllocations { get; set; } = new List<StayAllocation>();
    }
}