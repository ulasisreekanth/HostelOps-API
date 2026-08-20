using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
public class PaymentMethod
{
    /// <summary>
    /// Primary key for the PaymentMethod entity, representing the unique identifier for each payment method.
    /// </summary>
    [Key]
    public Guid PaymentMethodId { get; set; }

    /// <summary>
    /// The name of the payment method, with a maximum length of 100 characters.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The type of the payment method, represented by the PaymentMethodType enum (e.g., CreditCard, PayPal, BankTransfer).
    /// </summary>
    [Required]
    public PaymentMethodType Type { get; set; }

    /// <summary>
    /// Optional details or description of the payment method, with a maximum length of 500 characters.
    /// </summary>
    [StringLength(500)]
    public string? Details { get; set; }
    
    /// <summary>
    /// Indicates whether the payment method is currently active and available for use.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Timestamp indicating when the payment method record was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Navigation property to the collection of associated Payment entities, representing the payments made using this payment method.
    /// </summary>
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
}