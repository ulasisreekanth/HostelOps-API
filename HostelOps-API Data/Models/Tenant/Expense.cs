using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    [Table("Expenses")]
    public class Expense
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ExpenseId { get; set; }

        public int? ExpenseCategoryId { get; set; }

        public int? VendorId { get; set; }

        [Required]
        public DateOnly ExpenseDate { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        [Required]
        public int PaymentMethodId { get; set; }

        [StringLength(100)]
        public string? InvoiceNumber { get; set; }

        [StringLength(500)]
        public string? ReceiptUrl { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [ForeignKey("ExpenseCategoryId")]
        public ExpenseCategory? ExpenseCategory { get; set; }

        [ForeignKey("VendorId")]
        public Vendor? Vendor { get; set; }

        [ForeignKey("PaymentMethodId")]
        public PaymentMethod PaymentMethod { get; set; } = null!;
    }
}