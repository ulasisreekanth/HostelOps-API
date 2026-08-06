namespace HostelOps_API_Data.Models.Tenant;

public class PaymentMethod
{
    // =========================
    // Primary Key (PK)
    // =========================
    public int PaymentMethodId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public string Details { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    // Navigation Property

    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}