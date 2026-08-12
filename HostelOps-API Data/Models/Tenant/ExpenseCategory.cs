using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
public class ExpenseCategory
{
    /// <summary>
    /// Primary key for the ExpenseCategory entity, uniquely identifying each expense category.
    /// </summary>
    [Key]
    public Guid ExpenseCategoryId { get; set; }

    /// <summary>
    /// The name of the expense category, with a maximum length of 100 characters.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Optional description of the expense category, with a maximum length of 500 characters.
    /// </summary>
    [StringLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// Indicates whether the expense category is currently active or inactive.
    /// </summary>
    [Required]
    public bool IsActive { get; set; }

    /// <summary>
    /// Timestamp indicating when the expense category record was created.
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Navigation property to the collection of Expense entities associated with this ExpenseCategory.
    /// </summary>
    public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
}