using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
public class StaffRole
{
    /// <summary>
    /// The unique identifier for the staff role, represented as an integer. This property serves as the primary key for the StaffRole entity.
    /// </summary>
    [Key]
    public Guid StaffRoleId { get; set; }

    /// <summary>
    /// The name of the staff role, with a maximum length of 100 characters. This property is required and cannot be null or empty.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Optional description of the staff role, providing additional details or information about the role, with a maximum length of 500 characters. This property can be null if no description is provided.
    /// </summary>
    [StringLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// Indicates whether the staff role is currently active and available for assignment, represented as a boolean value. This property is required and cannot be null.
    /// </summary>
    [Required]
    public bool IsActive { get; set; }

    /// <summary>
    /// Timestamp indicating when the staff role record was created, represented as a DateTime value. This property is required and cannot be null.
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Navigation property to the collection of associated Staff entities, representing the staff members assigned to this role. This property allows access to the details of staff members associated with this role.
    /// </summary>
    public ICollection<Staff> Staffs { get; set; } = new List<Staff>();
}
}