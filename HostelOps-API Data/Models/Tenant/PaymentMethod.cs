using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class PaymentMethod
    {
        [Key]
        //Primary key for the PaymentMethod entity, representing the unique identifier for each payment method.
        public Guid PaymentMethodId { get; set; }

        [Required]
        [StringLength(100)]
        //The name of the payment method, with a maximum length of 100 characters.
        public string Name { get; set; } = string.Empty;

        [Required]
        //The type of the payment method, represented by the PaymentMethodType enum (e.g., CreditCard, PayPal, BankTransfer).
        public PaymentMethodType Type { get; set; }

        [StringLength(500)]
        //Optional details or description of the payment method, with a maximum length of 500 characters.
        public string? Details { get; set; }
        
        //Indicates whether the payment method is currently active and available for use.
        public bool IsActive { get; set; }

        //Timestamp indicating when the payment method record was created.
        public DateTime CreatedAt { get; set; }

        //Navigation property to the collection of associated Payment entities, representing the payments made using this payment method.
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}