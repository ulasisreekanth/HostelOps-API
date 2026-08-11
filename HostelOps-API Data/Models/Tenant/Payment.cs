using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class Payment
    {
        [Key]
        public long PaymentId { get; set; }

        [Required]
        public long InvoiceId { get; set; }

        [Required]
        public DateOnly PaymentDate { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        [Required]
        public int PaymentMethodId { get; set; }

        [StringLength(100)]
        public string? TransactionId { get; set; }

        [Required]
        public PaymentStatus Status { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; }

        [ForeignKey("InvoiceId")]
        public Invoice Invoice { get; set; } = null!;

        [ForeignKey("PaymentMethodId")]
        public PaymentMethod PaymentMethod { get; set; } = null!;

        public ICollection<Refund> Refunds { get; set; } = new List<Refund>();
    }
}