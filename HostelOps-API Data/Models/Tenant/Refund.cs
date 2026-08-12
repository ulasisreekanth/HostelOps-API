using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class Refund
    {
        [Key]
        //Primary key for the Refund entity, representing the unique identifier for each refund.
        public long RefundId { get; set; }

        [Required]
        //Foreign key referencing the associated payment for which this refund is being processed.
        public long PaymentId { get; set; }

        [Required]
        //The date on which the refund was issued, represented as a DateOnly value.
        public DateOnly RefundDate { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        //The amount of the refund, represented as a decimal value with a precision of 10 and scale of 2.
        public decimal Amount { get; set; }

        [StringLength(500)]
        //Optional reason or explanation for the refund, with a maximum length of 500 characters.
        public string? Reason { get; set; }

        [Required]
        //The current status of the refund, represented by the RefundStatus enum (e.g., Pending, Approved, Rejected).
        public RefundStatus Status { get; set; }

        [ForeignKey("PaymentId")]
        //Navigation property to the associated Payment entity, representing the relationship between Refund and Payment.
        public Payment Payment { get; set; } = null!;
    }
}