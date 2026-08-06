namespace HostelOps_API_Data.Models.Tenant;

public class Expense
{
    // =========================
    // Primary Key (PK)
    // =========================
    public long ExpenseId { get; set; }

    // =========================
    // Foreign Key (FK)
    // Refers ExpenseCategories.ExpenseCategoryId
    // Nullable
    // =========================
    public int? ExpenseCategoryId { get; set; }

    // =========================
    // Foreign Key (FK)
    // Refers Vendors.VendorId
    // Nullable
    // =========================
    public int? VendorId { get; set; }

    public DateOnly ExpenseDate { get; set; }

    public string Description { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    // ERD contains payment_method_id but no FK relation is defined
    public int PaymentMethodId { get; set; }

    public string InvoiceNumber { get; set; } = string.Empty;

    public string ReceiptUrl { get; set; } = string.Empty;

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    // Navigation Properties

    public ExpenseCategory? ExpenseCategory { get; set; }

    public Vendor? Vendor { get; set; }
}