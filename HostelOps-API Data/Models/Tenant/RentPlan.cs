namespace HostelOps_API_Data.Models.Tenant;

public class RentPlan
{
    // =========================
    // Primary Key (PK)
    // =========================
    public int RentPlanId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal RentAmount { get; set; }

    public string BillingCycle { get; set; } = string.Empty;

    public decimal SecurityDeposit { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    // Navigation Property

    public ICollection<StayAllocation> StayAllocations { get; set; } = new List<StayAllocation>();
}