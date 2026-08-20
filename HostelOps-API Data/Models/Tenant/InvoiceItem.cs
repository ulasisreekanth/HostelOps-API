using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
public class InvoiceItem
{
    /// <summary>
    /// Primary key for the InvoiceItem entity, uniquely identifying each invoice item.
    /// </summary>
    [Key]
    public Guid InvoiceItemId { get; set; }

    /// <summary>
    /// Foreign key referencing the associated invoice to which this item belongs.
    /// </summary>
    [Required]
    public Guid InvoiceId { get; set; }

    /// <summary>
    /// Foreign key referencing the associated charge type for this invoice item.
    /// </summary>
    [Required]
    public Guid ChargeTypeId { get; set; }

    /// <summary>
    /// Description of the invoice item, with a maximum length of 200 characters.
    /// </summary>
    [Required]
    [StringLength(200)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The quantity of the item being invoiced, represented as a decimal value with a precision of 10 and scale of 2.
    /// </summary>
    [Column(TypeName = "decimal(10,2)")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// The unit price of the item being invoiced, represented as a decimal value with a precision of 10 and scale of 2.
    /// </summary>
    [Column(TypeName = "decimal(10,2)")]
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// The total amount for this invoice item, calculated as Quantity multiplied by UnitPrice, represented as a decimal value with a precision of 10 and scale of 2.
    /// </summary>
    [Column(TypeName = "decimal(10,2)")]
    [Required]
    public decimal Amount { get; set; }

    /// <summary>
    /// Navigation property to the associated Invoice entity, representing the relationship between InvoiceItem and Invoice.
    /// </summary>
    [ForeignKey("InvoiceId")]
    public Invoice Invoice { get; set; } = null!;

    /// <summary>
    /// Navigation property to the associated ChargeType entity, representing the relationship between InvoiceItem and ChargeType.
    /// </summary>
    [ForeignKey("ChargeTypeId")]
    public ChargeType ChargeType { get; set; } = null!;
}
}