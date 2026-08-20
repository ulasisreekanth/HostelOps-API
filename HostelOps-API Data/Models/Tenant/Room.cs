using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
public class Room
{
    /// <summary>
    /// Primary key for the Room entity, uniquely identifying each room in the database.
    /// </summary>
    [Key]
    public Guid RoomId { get; set; }

    /// <summary>
    /// Foreign key referencing the associated Floor entity, indicating which floor the room is located on.
    /// </summary>
    [Required]
    public Guid FloorId { get; set; }

    /// <summary>
    /// The unique number or identifier assigned to the room, with a maximum length of 20 characters.
    /// </summary>
    [Required]
    [StringLength(20)]
    public string RoomNumber { get; set; } = string.Empty;

    /// <summary>
    /// The name or designation of the room, with a maximum length of 100 characters.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Optional description of the room, providing additional details or information about the room, with a maximum length of 500 characters.
    /// </summary>
    [StringLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// The area of the room in square feet, represented as a decimal value with a precision of 10 and scale of 2. This property is optional and can be null if the area is not specified.
    /// </summary>
    [Column(TypeName = "decimal(10,2)")]
    public decimal? AreaSqft { get; set; }

    /// <summary>
    /// The current status of the room, represented by the RoomStatus enum (e.g., Available, Occupied, UnderMaintenance). This property is required and cannot be null.
    /// </summary>
    [Required]
    public RoomStatus Status { get; set; }

    /// <summary>
    /// Timestamp indicating when the room record was created, represented as a DateTime value.
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Timestamp indicating when the room record was last updated, represented as a DateTime value.
    /// </summary>
    [Required]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Navigation property to the associated Floor entity, representing the relationship between Room and Floor. This property allows access to the details of the floor on which the room is located.
    /// </summary>
    [ForeignKey(nameof(FloorId))]
    public virtual Floor Floor { get; set; } = null!;

    /// <summary>
    /// Navigation property to the collection of associated Bed entities, representing the beds available in the room. This property allows access to the details of the beds within the room.
    /// </summary>
    public virtual ICollection<Bed> Beds { get; set; } = new List<Bed>();
}
}