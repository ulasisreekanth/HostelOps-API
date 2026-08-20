using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
public class Building
{
    /// <summary>
    /// Primary key for the Building entity.
    /// </summary>
    [Key]
    public Guid BuildingId { get; set; }

    /// <summary>
    /// Foreign key referencing the associated Hostel entity.
    /// </summary>
    [Required]
    public Guid HostelId { get; set; }

    /// <summary>
    /// The name of the building, with a maximum length of 100 characters.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Optional description of the building, with a maximum length of 500 characters.
    /// </summary>
    [StringLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// The current status of the building (e.g., Active, Inactive).
    /// </summary>
    [Required]
    public BuildingStatus Status { get; set; }

    /// <summary>
    /// Timestamp indicating when the building record was created.
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Timestamp indicating when the building record was last updated.
    /// </summary>
    [Required]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Navigation property to the associated Hostel entity, representing the relationship between Building and Hostel.
    /// </summary>
    [ForeignKey(nameof(HostelId))]
    public virtual Hostel Hostel { get; set; } = null!;
    
    /// <summary>
    /// Navigation property to the collection of Floor entities associated with this Building, representing the floors within the building.
    /// </summary>
    public virtual ICollection<Floor> Floors { get; set; } = new List<Floor>();
}
}