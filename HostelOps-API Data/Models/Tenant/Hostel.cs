using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
public class Hostel
{
    /// <summary>
    /// Primary key for the Hostel entity, uniquely identifying each hostel.
    /// </summary>
    [Key]
    public Guid HostelId { get; set; }

    /// <summary>
    /// The name of the hostel, with a maximum length of 150 characters.
    /// </summary>
    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// A unique code representing the hostel, with a maximum length of 20 characters.
    /// </summary>
    [Required]
    [StringLength(20)]
    public string Code { get; set; } = string.Empty;
    
    /// <summary>
    /// Description of the hostel, with a maximum length of 500 characters.
    /// </summary>
    [Required]
    [StringLength(500)]
    public string? Address { get; set; }

    /// <summary>
    /// Optional city where the hostel is located, with a maximum length of 100 characters.
    /// </summary>
    [StringLength(100)]
    public string? City { get; set; }

    /// <summary>
    /// Optional state where the hostel is located, with a maximum length of 100 characters.
    /// </summary>
    [StringLength(100)]
    public string? State { get; set; }

    /// <summary>
    /// Optional postal code (pincode) for the hostel's location, with a maximum length of 10 characters.
    /// </summary>
    [StringLength(10)]
    public string? Pincode { get; set; }

    /// <summary>
    /// Optional country where the hostel is located, with a maximum length of 100 characters.
    /// </summary>
    [StringLength(100)]
    public string? Country { get; set; }

    /// <summary>
    /// Contact phone number for the hostel, with a maximum length of 12 characters.
    /// </summary>
    [Phone]
    [StringLength(12)]
    public string? Phone { get; set; }

    /// <summary>
    /// Optional contact email address for the hostel, with a maximum length of 20 characters.
    /// </summary>
    [EmailAddress]
    [StringLength(20)]
    public string? Email { get; set; }

    /// <summary>
    /// Optional URL to the hostel's logo image, with a maximum length of 500 characters.
    /// </summary>
    [StringLength(500)]
    public string? LogoUrl { get; set; }

    /// <summary>
    /// The timezone in which the hostel operates, with a maximum length of 20 characters. Defaults to "UTC".
    /// </summary>
    [Required]
    [StringLength(20)]
    public string Timezone { get; set; } = "UTC";

    /// <summary>
    /// The current status of the hostel, represented by the HostelStatus enum (e.g., Active, Inactive).
    /// </summary>
    [Required]
    public HostelStatus Status { get; set; }

    /// <summary>
    /// Timestamp indicating when the hostel record was created.
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Timestamp indicating when the hostel record was last updated.
    /// </summary>
    [Required]
    public DateTime UpdatedAt { get; set; }
    
    /// <summary>
    /// Navigation property to the collection of Building entities associated with this Hostel, representing the buildings within the hostel.
    /// </summary>
    public virtual ICollection<Building> Buildings { get; set; } = new List<Building>();
}
}