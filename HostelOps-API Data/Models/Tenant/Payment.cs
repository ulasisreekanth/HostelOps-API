using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
public class Payment
{
    /// <summary>
    /// Primary key for the Payment entity, uniquely identifying each payment.
    /// </summary>
    [Key]
    public Guid PaymentId { get; set; }

    /// <summary>
    /// Foreign key referencing the associated invoice for which this payment is made.
    /// </summary>
    [Required]
    public Guid InvoiceId { get; set; }

    /// <summary>
    /// The date on which the payment was made, represented as a DateOnly value.
    /// </summary>
    [Required]
    public DateOnly PaymentDate { get; set; }

    /// <summary>
    /// The amount of the payment, represented as a decimal value with a precision of 10 and scale of 2.
    /// </summary>
    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Foreign key referencing the payment method used for this payment.
    /// </summary>
    [Required]
    public Guid PaymentMethodId { get; set; }

    /// <summary>
    /// The transaction identifier associated with the payment, with a maximum length of 100 characters.
    /// </summary>
    [StringLength(100)]
    public string? TransactionId { get; set; }

    /// <summary>
    /// The current status of the payment, represented by the PaymentStatus enum (e.g., Pending, Completed, Failed).
    /// </summary>
    [Required]
    public PaymentStatus Status { get; set; }

    /// <summary>
    /// Additional notes or comments related to the payment, with a maximum length of 500 characters.
    /// </summary>
    [StringLength(500)]
    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Navigation property to the associated Invoice entity, representing the relationship between Payment and Invoice.
    /// </summary>
    [ForeignKey("InvoiceId")]
    public Invoice Invoice { get; set; } = null!;

    /// <summary>
    /// Navigation property to the associated PaymentMethod entity, representing the relationship between Payment and PaymentMethod.
    /// </summary>
    [ForeignKey("PaymentMethodId")]
    public PaymentMethod PaymentMethod { get; set; } = null!;
    
    /// <summary>
    /// Navigation property to the collection of associated Refund entities, representing any refunds related to this payment.
    /// </summary>
    public ICollection<Refund> Refunds { get; set; } = new List<Refund>();
}
}