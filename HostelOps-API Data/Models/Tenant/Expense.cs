using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
public class Expense
{
    /// <summary>
    /// Primary key for the Expense entity.
    /// </summary>
    [Key]
    public Guid ExpenseId { get; set; }

    /// <summary>
    /// Foreign key referencing the ExpenseCategory entity, indicating the category of the expense.
    /// </summary>
    [Required]
    public Guid? ExpenseCategoryId { get; set; }
    
    /// <summary>
    /// Foreign key referencing the Vendor entity, indicating the vendor associated with the expense.
    /// </summary>
    public Guid? VendorId { get; set; }

    /// <summary>
    /// The date when the expense was incurred.
    /// </summary>
    [Required]
    public DateOnly ExpenseDate { get; set; }

    /// <summary>
    /// A brief description of the expense, with a maximum length of 500 characters.
    /// </summary>
    [Required]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The monetary amount of the expense, stored as a decimal with precision of 10 and scale of 2.
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Foreign key referencing the PaymentMethod entity, indicating the method of payment used for the expense.
    /// </summary>
    [Required]
    public Guid PaymentMethodId { get; set; }

    /// <summary>
    /// An optional reference number associated with the expense, with a maximum length of 100 characters.
    /// </summary>
    [StringLength(100)]
    public string? InvoiceNumber { get; set; }

    /// <summary>
    /// An optional URL pointing to a receipt or supporting document for the expense, with a maximum length of 500 characters.
    /// </summary>
    [StringLength(500)]
    public string? ReceiptUrl { get; set; }

    /// <summary>
    /// The ID of the user who created the expense record, indicating who is responsible for the entry.
    /// </summary>
    [Required]
    public int CreatedBy { get; set; }

    /// <summary>
    /// Timestamp indicating when the expense record was created.
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Navigation property to the associated ExpenseCategory entity, representing the category of the expense.
    /// </summary>
    [ForeignKey("ExpenseCategoryId")]
    public ExpenseCategory? ExpenseCategory { get; set; }

    /// <summary>
    /// Navigation property to the associated Vendor entity, representing the vendor associated with the expense.
    /// </summary>
    [ForeignKey("VendorId")]
    public Vendor? Vendor { get; set; }

    /// <summary>
    /// Navigation property to the associated PaymentMethod entity, representing the method of payment used for the expense.
    /// </summary>
    [ForeignKey("PaymentMethodId")]
    public PaymentMethod PaymentMethod { get; set; } = null!;
}
}