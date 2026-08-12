using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
public class BedType
{
    /// <summary>
    /// Primary key for the BedType entity.
    /// </summary>
    [Key]
    public Guid BedTypeId { get; set; }

    /// <summary>
    /// The name of the bed type, with a maximum length of 100 characters.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Optional description of the bed type, with a maximum length of 500 characters.
    /// </summary>
    [StringLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// The maximum number of occupants allowed for this bed type.
    /// </summary>
    [Required]
    public bool IsActive { get; set; }

    /// <summary>
    /// Timestamp indicating when the bed type record was created.
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; }
}
}