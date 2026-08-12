using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class ExpenseCategory
    {
        [Key]
        //Primary key for the ExpenseCategory entity, uniquely identifying each expense category.
        public int ExpenseCategoryId { get; set; }

        [Required]
        [StringLength(100)]
        //The name of the expense category, with a maximum length of 100 characters.
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        //Optional description of the expense category, with a maximum length of 500 characters.
        public string? Description { get; set; }

        [Required]
        // Indicates whether the expense category is currently active or inactive.
        public bool IsActive { get; set; }

        [Required]
        // Timestamp indicating when the expense category record was created.
        public DateTime CreatedAt { get; set; }
        
        // Navigation property to the collection of Expense entities associated with this ExpenseCategory.
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}