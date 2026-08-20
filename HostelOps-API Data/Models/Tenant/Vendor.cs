using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
public class Vendor
{
    /// <summary>
    /// Primary key for the Vendor entity, representing the unique identifier for each vendor in the system. This property is of type integer and is required for identifying individual vendors.
    /// </summary>
    [Key]
    public Guid VendorId { get; set; }

    /// <summary>
    /// The name of the vendor, with a maximum length of 150 characters. This property is required and cannot be null or empty, ensuring that each vendor has a valid name for identification purposes.
    /// </summary>
    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Optional contact person for the vendor, with a maximum length of 100 characters. This property can be null if no contact person is specified, allowing flexibility in vendor information.
    /// </summary>
    [StringLength(100)]
    public string? ContactPerson { get; set; }

    /// <summary>
    /// Optional phone number for the vendor, with a maximum length of 20 characters. This property can be null if no phone number is provided, allowing vendors to have contact information if available.
    /// </summary>
    [Phone]
    [StringLength(20)]
    public string? Phone { get; set; }

    /// <summary>
    /// Optional email address for the vendor, with a maximum length of 150 characters. This property can be null if no email address is provided, allowing vendors to have contact information if available.
    /// </summary>
    [EmailAddress]
    [StringLength(150)]
    public string? Email { get; set; }

    /// <summary>
    /// Optional address for the vendor, with a maximum length of 500 characters. This property can be null if no address is provided, allowing vendors to have location information if available.
    /// </summary>
    [StringLength(500)]
    public string? Address { get; set; }

    /// <summary>
    /// Optional city for the vendor, with a maximum length of 100 characters. This property can be null if no city is provided, allowing vendors to have location information if available.
    /// </summary>
    [StringLength(100)]
    public string? City { get; set; }

    /// <summary>
    /// Optional state for the vendor, with a maximum length of 100 characters. This property can be null if no state is provided, allowing vendors to have location information if available.
    /// </summary>
    [StringLength(100)]
    public string? State { get; set; }

    /// <summary>
    /// Optional pincode for the vendor, with a maximum length of 10 characters. This property can be null if no pincode is provided, allowing vendors to have location information if available.
    /// </summary>
    [StringLength(10)]
    public string? Pincode { get; set; }

    /// <summary>
    /// Optional country for the vendor, with a maximum length of 30 characters. This property can be null if no country is provided, allowing vendors to have location information if available.
    /// </summary>
    [StringLength(30)]
    public string? GstNumber { get; set; }

    /// <summary>
    /// Indicates whether the vendor is currently active and available for business, represented as a boolean value. This property is required and cannot be null, ensuring that each vendor has a defined status.
    /// </summary>
    [Required]
    public bool IsActive { get; set; }

    /// <summary>
    /// Timestamp indicating when the vendor record was created, represented as a DateTime value. This property is required and cannot be null, providing information about the creation date of the vendor record.
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Navigation property to the collection of associated Expense entities, representing the expenses related to this vendor. This property allows access to the details of expenses made for this vendor.
    /// </summary>
    public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
}