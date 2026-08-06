namespace HostelOps_API_Data.Models.Tenant;

public class InvoiceItem
{
    // =========================
    // Primary Key (PK)
    // =========================
    public long InvoiceItemId { get; set; }

    // =========================
    // Foreign Key (FK)
    // Refers Invoices.InvoiceId
    // =========================
    public long InvoiceId { get; set; }

    // =========================
    // Foreign Key (FK)
    // Refers ChargeTypes.ChargeTypeId
    // =========================
    public int ChargeTypeId { get; set; }

    public string Description { get; set; } = string.Empty;

    public decimal Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Amount { get; set; }

    // Navigation Properties

    public Invoice Invoice { get; set; } = null!;

    public ChargeType ChargeType { get; set; } = null!;
}