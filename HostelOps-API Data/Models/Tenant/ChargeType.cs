namespace HostelOps_API_Data.Models.Tenant;

public class ChargeType
{
    // =========================
    // Primary Key (PK)
    // =========================
    public int ChargeTypeId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public bool IsRecurring { get; set; }

    public bool IsMandatory { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    // Navigation Property

    public ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
}