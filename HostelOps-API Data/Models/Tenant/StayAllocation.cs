using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class StayAllocation
    {
        [Key]
        //primary key for the StayAllocation entity, representing the unique identifier for each stay allocation record. This property is of type integer and is required for identifying individual stay allocations in the system.
        public Guid AllocationId { get; set; }

        //foreign key referencing the Reservation entity, representing the reservation associated with this stay allocation. This property is of type integer and is optional, allowing for cases where a stay allocation may not be linked to a specific reservation.
        public Guid? ReservationId { get; set; }

        [Required]
        //foreign key referencing the Resident entity, representing the resident associated with this stay allocation. This property is of type integer and is required for linking the stay allocation to a specific resident in the system.
        public Guid ResidentId { get; set; }

        [Required]
        //foreign key referencing the Bed entity, representing the bed assigned to this stay allocation. This property is of type integer and is required for linking the stay allocation to a specific bed in the system.
        public Guid BedId { get; set; }

        [Required]
        //foreign key referencing the RentPlan entity, representing the rent plan associated with this stay allocation. This property is of type integer and is required for linking the stay allocation to a specific rent plan in the system.
        public DateOnly CheckInDate { get; set; }

        //foreign key referencing the RentPlan entity, representing the rent plan associated with this stay allocation. This property is of type integer and is required for linking the stay allocation to a specific rent plan in the system.
        public DateOnly? CheckOutDate { get; set; }

        [Required]
        //foreign key referencing the RentPlan entity, representing the rent plan associated with this stay allocation. This property is of type integer and is required for linking the stay allocation to a specific rent plan in the system.
        public Guid RentPlanId { get; set; }

        [Required]
        //The current status of the stay allocation, represented by the StayAllocationStatus enum. This property is required and indicates whether the stay allocation is active, completed, or cancelled.
        public StayAllocationStatus Status { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        //The security deposit amount associated with the stay allocation, represented as a decimal value with a precision of 10 and scale of 2. This property is required and indicates the amount of money held as a security deposit for the stay allocation.
        public decimal SecurityDeposit { get; set; }
 
        //The total rent amount associated with the stay allocation, represented as a decimal value with a precision of 10 and scale of 2. This property is required and indicates the total rent amount for the stay allocation.
        public DateTime CreatedAt { get; set; }
 
        //Timestamp indicating when the stay allocation record was last updated, represented as a DateTime value. This property is required and indicates the last modification date of the stay allocation record.
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("ReservationId")]
        //Navigation property to the associated Reservation entity, representing the reservation linked to this stay allocation. This property allows access to the details of the reservation associated with the stay allocation.
        public Reservation? Reservation { get; set; }

        [ForeignKey("ResidentId")]
        //Navigation property to the associated Resident entity, representing the resident linked to this stay allocation. This property allows access to the details of the resident associated with the stay allocation.
        public Resident Resident { get; set; } = null!;

        [ForeignKey("BedId")]
        //Navigation property to the associated Bed entity, representing the bed assigned to this stay allocation. This property allows access to the details of the bed associated with the stay allocation.
        public Bed Bed { get; set; } = null!;

        [ForeignKey("RentPlanId")]
        //Navigation property to the associated RentPlan entity, representing the rent plan linked to this stay allocation. This property allows access to the details of the rent plan associated with the stay allocation.
        public RentPlan RentPlan { get; set; } = null!;
    }
}