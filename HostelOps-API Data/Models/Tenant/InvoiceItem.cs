using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class InvoiceItem
    {
        [Key]
        //Primary key for the InvoiceItem entity, uniquely identifying each invoice item.
        public Guid InvoiceItemId { get; set; }

        [Required]
        //Foreign key referencing the associated invoice to which this item belongs.
        public Guid InvoiceId { get; set; }

        [Required]
        //Foreign key referencing the associated charge type for this invoice item.
        public Guid ChargeTypeId { get; set; }

        [Required]
        [StringLength(200)]
        //Description of the invoice item, with a maximum length of 200 characters.
        public string Description { get; set; } = string.Empty;

        //The quantity of the item being invoiced, represented as a decimal value with a precision of 10 and scale of 2.
        [Column(TypeName = "decimal(10,2)")]

        //The unit price of the item being invoiced, represented as a decimal value with a precision of 10 and scale of 2.
        public decimal Quantity { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        //The unit price of the item being invoiced, represented as a decimal value with a precision of 10 and scale of 2.
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        //The total amount for this invoice item, calculated as Quantity multiplied by UnitPrice, represented as a decimal value with a precision of 10 and scale of 2.

        [Required]
        public decimal Amount { get; set; }

        [ForeignKey("InvoiceId")]
        //Navigation property to the associated Invoice entity, representing the relationship between InvoiceItem and Invoice.
        public Invoice Invoice { get; set; } = null!;

        [ForeignKey("ChargeTypeId")]
        //Navigation property to the associated ChargeType entity, representing the relationship between InvoiceItem and ChargeType.
        public ChargeType ChargeType { get; set; } = null!;
    }
}