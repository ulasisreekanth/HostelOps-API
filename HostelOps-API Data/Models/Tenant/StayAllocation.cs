using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class StayAllocation
    {
        [Key]
        public int AllocationId { get; set; }

        public int? ReservationId { get; set; }

        [Required]
        public int ResidentId { get; set; }

        [Required]
        public int BedId { get; set; }

        [Required]
        public DateOnly CheckInDate { get; set; }

        public DateOnly? CheckOutDate { get; set; }

        [Required]
        public int RentPlanId { get; set; }

        [Required]
        public StayAllocationStatus Status { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal SecurityDeposit { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [ForeignKey("ReservationId")]
        public Reservation? Reservation { get; set; }

        [ForeignKey("ResidentId")]
        public Resident Resident { get; set; } = null!;

        [ForeignKey("BedId")]
        public Bed Bed { get; set; } = null!;

        [ForeignKey("RentPlanId")]
        public RentPlan RentPlan { get; set; } = null!;
    }
}