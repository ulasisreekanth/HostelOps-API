using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class Invoice
    {
        [Key]
        //Unique identifier for the invoice, serving as the primary key in the database.
        public long InvoiceId { get; set; }

        [Required]
        //Foreign key referencing the associated resident for whom the invoice is generated.
        public int ResidentId { get; set; }

        [Required]
        [StringLength(50)]
        //Unique invoice number assigned to the invoice, with a maximum length of 50 characters.
        public string InvoiceNumber { get; set; } = string.Empty;

        [Required]
        //The date when the invoice was issued, represented as a DateOnly value.
        public DateOnly InvoiceDate { get; set; }

        [Required]
        //The due date for payment of the invoice, represented as a DateOnly value.
        public DateOnly DueDate { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        //The total amount of the invoice, represented as a decimal value with a precision of 10 and scale of 2.
        public decimal TotalAmount { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        //The amount that has been paid towards the invoice, represented as a decimal value with a precision of 10 and scale of 2.
        public decimal PaidAmount { get; set; }

        [Required]
        //  The current status of the invoice, represented by the InvoiceStatus enum (e.g., Pending, Paid, Overdue).
        public InvoiceStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [ForeignKey("ResidentId")]
        //  Navigation property to the associated Resident entity, representing the relationship between Invoice and Resident.
        public Resident Resident { get; set; } = null!;
        
        //  Navigation property to the collection of associated InvoiceItem entities, representing the items included in the invoice.
        public ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
        
        //  Navigation property to the collection of associated Payment entities, representing the payments made towards the invoice.
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}