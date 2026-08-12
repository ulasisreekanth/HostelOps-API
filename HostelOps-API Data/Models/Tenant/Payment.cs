using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class Payment
    {
        [Key]
        //Primary key for the Payment entity, uniquely identifying each payment.
        public long PaymentId { get; set; }

        [Required]
        //Foreign key referencing the associated invoice for which this payment is made.
        public long InvoiceId { get; set; }

        [Required]
        //The date on which the payment was made, represented as a DateOnly value.
        public DateOnly PaymentDate { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        //The amount of the payment, represented as a decimal value with a precision of 10 and scale of 2.
        public decimal Amount { get; set; }

        [Required]
        //Foreign key referencing the payment method used for this payment.
        public int PaymentMethodId { get; set; }

        [StringLength(100)]
        //The transaction identifier associated with the payment, with a maximum length of 100 characters.
        public string? TransactionId { get; set; }

        [Required]
        //  The current status of the payment, represented by the PaymentStatus enum (e.g., Pending, Completed, Failed).
        public PaymentStatus Status { get; set; }

        [StringLength(500)]
        //Additional notes or comments related to the payment, with a maximum length of 500 characters.
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; }

        [ForeignKey("InvoiceId")]
        //Navigation property to the associated Invoice entity, representing the relationship between Payment and Invoice.
        public Invoice Invoice { get; set; } = null!;

        [ForeignKey("PaymentMethodId")]
        //Navigation property to the associated PaymentMethod entity, representing the relationship between Payment and PaymentMethod.
        public PaymentMethod PaymentMethod { get; set; } = null!;
        
        //Navigation property to the collection of associated Refund entities, representing any refunds related to this payment.
        public ICollection<Refund> Refunds { get; set; } = new List<Refund>();
    }
}