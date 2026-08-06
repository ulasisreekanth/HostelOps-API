namespace HostelOps_API_Data.Models.Tenant;

public class Invoice
{
    // =========================
    // Primary Key (PK)
    // =========================
    public long InvoiceId { get; set; }

    // =========================
    // Foreign Key (FK)
    // Refers Residents.ResidentId
    // =========================
    public int ResidentId { get; set; }

    public string InvoiceNumber { get; set; } = string.Empty;

    public DateOnly InvoiceDate { get; set; }

    public DateOnly DueDate { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal PaidAmount { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    // Navigation Properties

    public Resident Resident { get; set; } = null!;

    public ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}