using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        public int? ResidentId { get; set; }

        public int? InquiryId { get; set; }

        [Required]
        public DateOnly ReservedFrom { get; set; }

        [Required]
        public DateOnly ReservedTo { get; set; }

        [Required]
        public int RoomTypeId { get; set; }

        [Required]
        public int SharingTypeId { get; set; }

        [Required]
        public ReservationStatus Status { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal AdvanceAmount { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("ResidentId")]
        public Resident? Resident { get; set; }

        [ForeignKey("InquiryId")]
        public Inquiry? Inquiry { get; set; }

        [ForeignKey("RoomTypeId")]
        public RoomType RoomType { get; set; } = null!;

        [ForeignKey("SharingTypeId")]
        public SharingType SharingType { get; set; } = null!;

        public ICollection<StayAllocation> StayAllocations { get; set; } = new List<StayAllocation>();
    }
}