using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
public class Resident
{
    /// <summary>
    /// Primary key for the Resident entity, representing the unique identifier for each resident.
    /// </summary>
    [Key]
    public Guid ResidentId { get; set; }

    /// <summary>
    /// The full name of the resident, with a maximum length of 150 characters.
    /// </summary>
    [Required]
    [StringLength(150)]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// The email address of the resident, with a maximum length of 150 characters and validated for proper email format.
    /// </summary>
    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The phone number of the resident, with a maximum length of 20 characters and validated for proper phone number format.
    /// </summary>
    [Required]
    [Phone]
    [StringLength(20)]
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// The date of birth of the resident, represented as a DateOnly value.
    /// </summary>
    [Required]
    public DateOnly DateOfBirth { get; set; }

    /// <summary>
    /// The gender of the resident, represented by the Gender enum (e.g., Male, Female, Other).
    /// </summary>
    [Required]
    public Gender Gender { get; set; }

    /// <summary>
    /// Optional address of the resident, with a maximum length of 500 characters.
    /// </summary>
    [StringLength(500)]
    public string? Address { get; set; }

    [StringLength(100)]
    public string? City { get; set; }

    [StringLength(100)]
    public string? State { get; set; }

    [StringLength(10)]
    public string? Pincode { get; set; }

    [StringLength(100)]
    public string? Country { get; set; }

    [StringLength(50)]
    public string? IdProofType { get; set; }

    [StringLength(100)]
    public string? IdProofNumber { get; set; }

    [StringLength(500)]
    public string? ProfileImageUrl { get; set; }

    /// <summary>
    /// The current status of the resident, represented by the ResidentStatus enum (e.g., Active, Inactive, Suspended).
    /// </summary>
    [Required]
    public ResidentStatus Status { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }
    
    /// <summary>
    /// Navigation property to the collection of associated Reservation entities, representing the reservations made by this resident.
    /// </summary>
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    /// <summary>
    /// Navigation property to the collection of associated StayAllocation entities, representing the stay allocations linked to this resident.
    /// </summary>
    public ICollection<StayAllocation> StayAllocations { get; set; } = new List<StayAllocation>();

    /// <summary>
    /// Navigation property to the collection of associated ResidentDocument entities, representing the documents submitted by this resident.
    /// </summary>
    public ICollection<ResidentDocument> ResidentDocuments { get; set; } = new List<ResidentDocument>();

    // Navigation property to the collection of associated Payment entities, representing the payments made by this resident.

    // Navigation property to the collection of associated Payment entities, representing the payments made by this resident.
    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    /// <summary>
    /// Navigation property to the collection of associated Complaint entities, representing the complaints lodged by this resident.
    /// </summary>
    public ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();
}
}