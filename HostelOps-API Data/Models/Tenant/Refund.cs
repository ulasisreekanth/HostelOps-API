using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
public class Refund
{
    /// <summary>
    /// Primary key for the Refund entity, representing the unique identifier for each refund.
    /// </summary>
    [Key]
    public Guid RefundId { get; set; }

    /// <summary>
    /// Foreign key referencing the associated payment for which this refund is being processed.
    /// </summary>
    [Required]
    public Guid PaymentId { get; set; }

    /// <summary>
    /// The date on which the refund was issued, represented as a DateOnly value.
    /// </summary>
    [Required]
    public DateOnly RefundDate { get; set; }

    /// <summary>
    /// The amount of the refund, represented as a decimal value with a precision of 10 and scale of 2.
    /// </summary>
    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Optional reason or explanation for the refund, with a maximum length of 500 characters.
    /// </summary>
    [StringLength(500)]
    public string? Reason { get; set; }

    /// <summary>
    /// The current status of the refund, represented by the RefundStatus enum (e.g., Pending, Approved, Rejected).
    /// </summary>
    [Required]
    public RefundStatus Status { get; set; }

    /// <summary>
    /// Navigation property to the associated Payment entity, representing the relationship between Refund and Payment.
    /// </summary>
    [ForeignKey("PaymentId")]
    public Payment Payment { get; set; } = null!;
}
}