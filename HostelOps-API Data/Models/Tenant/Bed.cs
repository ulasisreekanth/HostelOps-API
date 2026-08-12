using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{

public class Bed
{
    /// <summary>
    /// Primary key for the Bed entity.
    /// </summary>
    [Key]
    public Guid BedId { get; set; }

    /// <summary>
    /// Foreign key that identifies the room to which the bed belongs.
    /// </summary>
    [Required]
    public Guid RoomId { get; set; }

    /// <summary>
    /// The unique number or identifier for the bed within the room, with a maximum length of 20 characters.
    /// </summary>
    [Required]
    [StringLength(10)]
    public string BedNumber { get; set; } = string.Empty;

    /// <summary>
    /// The name or label for the bed, with a maximum length of 50 characters.
    /// </summary>
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Current status of bed, represented using the BedStatus enum (e.g., Available, Occupied, Maintenance).
    /// </summary>
    [Required]
    public BedStatus Status { get; set; }

    /// <summary>
    /// Timestamp indicating when the bed record was created.
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Timestamp indicating when the bed record was last updated.
    /// </summary>
    [Required]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Navigation property to the associated Room entity, representing the relationship between Bed and Room.
    /// </summary>
    [ForeignKey(nameof(RoomId))]
    public virtual Room Room { get; set; } = null!;
  
    /// <summary>
    /// Navigation property to the collection of StayAllocation entities associated with this Bed, representing the stay allocations for this bed.
    /// </summary>
    public virtual ICollection<StayAllocation> StayAllocations { get; set; } = new List<StayAllocation>();
}
}