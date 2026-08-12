using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
public class ChargeType
{
    /// <summary>
    /// Primary key for the ChargeType entity.
    /// </summary>
    [Key]
    public Guid ChargeTypeId { get; set; }

    /// <summary>
    /// The name of the charge type, with a maximum length of 100 characters.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Optional description of the charge type, with a maximum length of 500 characters.
    /// </summary>
    [StringLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// The category of the charge type, indicating whether it's a one-time or recurring charge.
    /// </summary>
    [Required]
    public ChargeCategory Category { get; set; }
    
    /// <summary>
    /// Indicates whether the charge type is recurring (true) or one-time (false).
    /// </summary>
    public bool IsRecurring { get; set; }
    
    /// <summary>
    /// Indicates whether the charge type is mandatory (true) or optional (false).
    /// </summary>
    public bool IsMandatory { get; set; }
    
    /// <summary>
    /// Indicates whether the charge type is active (true) or inactive (false).
    /// </summary>
    public bool IsActive { get; set; }
    
    /// <summary>
    /// Timestamp indicating when the charge type record was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Navigation property to the collection of InvoiceItem entities associated with this ChargeType, representing the invoice items that use this charge type.
    /// </summary>
    public ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
}
}