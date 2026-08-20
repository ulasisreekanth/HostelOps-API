using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
public class RentPlan
{
    /// <summary>
    /// Primary key for the RentPlan entity, representing the unique identifier for each rent plan.
    /// </summary>
    [Key]
    public Guid RentPlanId { get; set; }

    /// <summary>
    /// The name of the rent plan, with a maximum length of 100 characters.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Optional description of the rent plan, with a maximum length of 500 characters.
    /// </summary>
    [StringLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// The amount of rent associated with this plan, represented as a decimal value with a precision of 10 and scale of 2.
    /// </summary>
    [Column(TypeName = "decimal(10,2)")]
    public decimal RentAmount { get; set; }

    /// <summary>
    /// The billing cycle for the rent plan, represented by the BillingCycle enum (e.g., Monthly, Quarterly, Yearly).
    /// </summary>
    [Required]
    public BillingCycle BillingCycle { get; set; }

    /// <summary>
    /// The security deposit amount required for this rent plan, represented as a decimal value with a precision of 10 and scale of 2.
    /// </summary>
    [Column(TypeName = "decimal(10,2)")]
    public decimal SecurityDeposit { get; set; }

    /// <summary>
    /// Indicates whether the rent plan is currently active and available for selection.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Timestamp indicating when the rent plan record was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Navigation property to the collection of associated StayAllocation entities, representing the stay allocations linked to this rent plan.
    /// </summary>
    public ICollection<StayAllocation> StayAllocations { get; set; } = new List<StayAllocation>();
}
}