namespace HostelOps_API_Data.Models.Tenant;

public class Refund
{
    // =========================
    // Primary Key (PK)
    // =========================
    public long RefundId { get; set; }

    // =========================
    // Foreign Key (FK)
    // Refers Payments.PaymentId
    // =========================
    public long PaymentId { get; set; }

    public DateOnly RefundDate { get; set; }

    public decimal Amount { get; set; }

    public string Reason { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    // Navigation Property

    public Payment Payment { get; set; } = null!;
}