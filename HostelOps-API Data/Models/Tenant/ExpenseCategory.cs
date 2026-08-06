namespace HostelOps_API_Data.Models.Tenant;

public class ExpenseCategory
{
    // =========================
    // Primary Key (PK)
    // =========================
    public int ExpenseCategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    // Navigation Property
    // One Expense Category can have many Expenses
    public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}