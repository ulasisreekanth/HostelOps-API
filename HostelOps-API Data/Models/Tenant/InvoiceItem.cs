using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    [Table("InvoiceItems")]
    public class InvoiceItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long InvoiceItemId { get; set; }

        [Required]
        public long InvoiceId { get; set; }

        [Required]
        public int ChargeTypeId { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        [ForeignKey("InvoiceId")]
        public Invoice Invoice { get; set; } = null!;

        [ForeignKey("ChargeTypeId")]
        public ChargeType ChargeType { get; set; } = null!;
    }
}