using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
public class Invoice
{
    /// <summary>
    /// Unique identifier for the invoice, serving as the primary key in the database.
    /// </summary>
    [Key]
    public Guid InvoiceId { get; set; }

    /// <summary>
    /// Foreign key referencing the associated resident for whom the invoice is generated.
    /// </summary>
    [Required]
    public Guid ResidentId { get; set; }

    /// <summary>
    /// Unique invoice number assigned to the invoice, with a maximum length of 50 characters.
    /// </summary>
    [Required]
    [StringLength(50)]
    public string InvoiceNumber { get; set; } = string.Empty;

    /// <summary>
    /// The date when the invoice was issued, represented as a DateOnly value.
    /// </summary>
    [Required]
    public DateOnly InvoiceDate { get; set; }

    /// <summary>
    /// The due date for payment of the invoice, represented as a DateOnly value.
    /// </summary>
    [Required]
    public DateOnly DueDate { get; set; }

    /// <summary>
    /// The total amount of the invoice, represented as a decimal value with a precision of 10 and scale of 2.
    /// </summary>
    [Column(TypeName = "decimal(10,2)")]
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// The amount that has been paid towards the invoice, represented as a decimal value with a precision of 10 and scale of 2.
    /// </summary>
    [Column(TypeName = "decimal(10,2)")]
    public decimal PaidAmount { get; set; }

    /// <summary>
    /// The current status of the invoice, represented by the InvoiceStatus enum (e.g., Pending, Paid, Overdue).
    /// </summary>
    [Required]
    public InvoiceStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Navigation property to the associated Resident entity, representing the relationship between Invoice and Resident.
    /// </summary>
    [ForeignKey("ResidentId")]
    public Resident Resident { get; set; } = null!;
    
    /// <summary>
    /// Navigation property to the collection of associated InvoiceItem entities, representing the items included in the invoice.
    /// </summary>
    public ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

    /// <summary>
    /// Navigation property to the collection of associated Payment entities, representing the payments made towards the invoice.
    /// </summary>
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
}