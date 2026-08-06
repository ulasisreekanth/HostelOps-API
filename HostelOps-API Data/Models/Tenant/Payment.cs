namespace HostelOps_API_Data.Models.Tenant;

public class Payment
{
    // =========================
    // Primary Key (PK)
    // =========================
    public long PaymentId { get; set; }

    // =========================
    // Foreign Key (FK)
    // Refers Invoices.InvoiceId
    // =========================
    public long InvoiceId { get; set; }

    // =========================
    // Foreign Key (FK)
    // Refers PaymentMethods.PaymentMethodId
    // =========================
    public int PaymentMethodId { get; set; }

    public DateOnly PaymentDate { get; set; }

    public decimal Amount { get; set; }

    public string TransactionId { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    // Navigation Properties

    public Invoice Invoice { get; set; } = null!;

    public PaymentMethod PaymentMethod { get; set; } = null!;

    public ICollection<Refund> Refunds { get; set; } = new List<Refund>();
}