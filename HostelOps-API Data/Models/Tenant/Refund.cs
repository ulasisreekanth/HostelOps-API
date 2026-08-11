using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    [Table("Refunds")]
    public class Refund
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RefundId { get; set; }

        [Required]
        public long PaymentId { get; set; }

        [Required]
        public DateOnly RefundDate { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        [StringLength(500)]
        public string? Reason { get; set; }

        [Required]
        public RefundStatus Status { get; set; }

        [ForeignKey("PaymentId")]
        public Payment Payment { get; set; } = null!;
    }
}