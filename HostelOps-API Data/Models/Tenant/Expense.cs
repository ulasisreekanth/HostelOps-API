using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class Expense
    {
        [Key]
        //primary key for the Expense entity.
        public long ExpenseId { get; set; }
        
        [Required]
        //Foreign key referencing the ExpenseCategory entity, indicating the category of the expense.
        public int? ExpenseCategoryId { get; set; }
        
        //Foreign key referencing the Vendor entity, indicating the vendor associated with the expense.
        public int? VendorId { get; set; }

        [Required]
        //The date when the expense was incurred.
        public DateOnly ExpenseDate { get; set; }

        [Required]
        [StringLength(500)]
        //A brief description of the expense, with a maximum length of 500 characters.
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        //The monetary amount of the expense, stored as a decimal with precision of 10 and scale of 2.
        public decimal Amount { get; set; }

        [Required]
        //Foreign key referencing the PaymentMethod entity, indicating the method of payment used for the expense.
        public int PaymentMethodId { get; set; }

        [StringLength(100)]
        //An optional reference number associated with the expense, with a maximum length of 100 characters.
        public string? InvoiceNumber { get; set; }

        [StringLength(500)]
        //An optional URL pointing to a receipt or supporting document for the expense, with a maximum length of 500 characters.
        public string? ReceiptUrl { get; set; }

        [Required]
        //The ID of the user who created the expense record, indicating who is responsible for the entry.
        public int CreatedBy { get; set; }

        [Required]
        //Timestamp indicating when the expense record was created.
        public DateTime CreatedAt { get; set; }

        [ForeignKey("ExpenseCategoryId")]
        //Navigation property to the associated ExpenseCategory entity, representing the category of the expense.
        public ExpenseCategory? ExpenseCategory { get; set; }

        [ForeignKey("VendorId")]
        //Navigation property to the associated Vendor entity, representing the vendor associated with the expense.
        public Vendor? Vendor { get; set; }

        [ForeignKey("PaymentMethodId")]
        //Navigation property to the associated PaymentMethod entity, representing the method of payment used for the expense.
        public PaymentMethod PaymentMethod { get; set; } = null!;
    }
}