using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class RentPlan
    {
        [Key]
        //Primary key for the RentPlan entity, representing the unique identifier for each rent plan.
        public Guid RentPlanId { get; set; }

        [Required]
        [StringLength(100)]
        //The name of the rent plan, with a maximum length of 100 characters.
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        //Optional description of the rent plan, with a maximum length of 500 characters.
        public string? Description { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        //The amount of rent associated with this plan, represented as a decimal value with a precision of 10 and scale of 2.
        public decimal RentAmount { get; set; }

        [Required]
        //The billing cycle for the rent plan, represented by the BillingCycle enum (e.g., Monthly, Quarterly, Yearly).
        public BillingCycle BillingCycle { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        //The security deposit amount required for this rent plan, represented as a decimal value with a precision of 10 and scale of 2.
        public decimal SecurityDeposit { get; set; }

        //Indicates whether the rent plan is currently active and available for selection.
        public bool IsActive { get; set; }

        //Timestamp indicating when the rent plan record was created.
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        //Navigation property to the collection of associated StayAllocation entities, representing the stay allocations linked to this rent plan.
        public ICollection<StayAllocation> StayAllocations { get; set; } = new List<StayAllocation>();
    }
}