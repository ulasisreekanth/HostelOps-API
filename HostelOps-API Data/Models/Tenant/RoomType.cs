using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
public class RoomType
{
    /// <summary>
    /// Unique identifier for the RoomType entity, serving as the primary key in the database.
    /// </summary>
    [Key]
    public Guid RoomTypeId { get; set; }

    /// <summary>
    /// The name or designation of the room type, with a maximum length of 100 characters.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Optional description of the room type, providing additional details or information about the room type, with a maximum length of 500 characters.
    /// </summary>
    [StringLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// The maximum number of occupants allowed for this room type, represented as an integer value.
    /// </summary>
    [Required]
    public int Occupants { get; set; }

    /// <summary>
    /// Indicates whether the room type is currently active and available for use, represented as a boolean value.
    /// </summary>
    [Required]
    public bool IsActive { get; set; }

    /// <summary>
    /// Timestamp indicating when the room type record was created, represented as a DateTime value.
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Navigation property to the collection of associated Inquiry entities, representing the inquiries related to this room type. This property allows access to the details of inquiries made for this room type.
    /// </summary>
    public virtual ICollection<Inquiry> Inquiries { get; set; } = new List<Inquiry>();

    /// <summary>
    /// Navigation property to the collection of associated Reservation entities, representing the reservations made for this room type. This property allows access to the details of reservations associated with this room type.
    /// </summary>
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
}