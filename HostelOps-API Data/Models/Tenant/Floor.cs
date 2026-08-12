using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
public class Floor
{
    /// <summary>
    /// Primary key of the Floor entity.
    /// </summary>
    [Key]
    public Guid FloorId { get; set; }

    /// <summary>
    /// Foreign key that identifies which Building this Floor belongs to.
    /// </summary>
    [Required]
    public Guid BuildingId { get; set; }

    /// <summary>
    /// Name of the floor & max length allowesniss 100 characters.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The floor number within the building.
    /// </summary>
    [Required]
    public int FloorNumber { get; set; }

    /// <summary>
    /// Optional description of the floor, with a maximum length of 500 characters.
    /// </summary>
    [StringLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// The current status of the floor (e.g., Active, Inactive).
    /// </summary>
    [Required]
    public FloorStatus Status { get; set; }

    /// <summary>
    /// Timestamp indicating when the floor record was created.
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Timestamp indicating when the floor record was last updated.
    /// </summary>
    [Required]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Navigation property to the associated Building entity, it represents the relationship between Floor and Building.
    /// </summary>
    [ForeignKey(nameof(BuildingId))]
    public virtual Building Building { get; set; } = null!;

    /// <summary>
    /// Navigation property to the collection of Room entities associated with this Floor.
    /// </summary>
    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
}