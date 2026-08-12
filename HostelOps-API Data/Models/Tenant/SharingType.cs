using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
public class SharingType
{
    /// <summary>
    /// Primary key for the SharingType entity, uniquely identifying each sharing type record in the database.
    /// </summary>
    [Key]
    public Guid SharingTypeId { get; set; }

    /// <summary>
    /// The name of the sharing type, with a maximum length of 100 characters. This property is required and cannot be null or empty.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Optional description of the sharing type, providing additional details or information about the sharing type, with a maximum length of 500 characters.
    /// </summary>
    [StringLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// The maximum number of occupants allowed for this sharing type, represented as an integer value. This property is required and cannot be null.
    /// </summary>
    [Required]
    public int MaxOccupants { get; set; }

    /// <summary>
    /// Indicates whether the sharing type is currently active and available for use, represented as a boolean value. This property is required and cannot be null.
    /// </summary>
    [Required]
    public bool IsActive { get; set; }

    /// <summary>
    /// Timestamp indicating when the sharing type record was created, represented as a DateTime value. This property is required and cannot be null.
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Navigation property to the collection of associated Inquiry entities, representing the inquiries related to this sharing type. This property allows access to the details of inquiries made for this sharing type.
    /// </summary>
    public virtual ICollection<Inquiry> Inquiries { get; set; } = new List<Inquiry>();

    /// <summary>
    /// Navigation property to the collection of associated Reservation entities, representing the reservations made for this sharing type. This property allows access to the details of reservations associated with this sharing type.
    /// </summary>
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
}