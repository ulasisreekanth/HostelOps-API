using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class RentPlan
    {
        [Key]
        public int RentPlanId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal RentAmount { get; set; }

        [Required]
        public BillingCycle BillingCycle { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal SecurityDeposit { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<StayAllocation> StayAllocations { get; set; } = new List<StayAllocation>();
    }
}