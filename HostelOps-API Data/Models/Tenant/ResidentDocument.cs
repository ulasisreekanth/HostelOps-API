using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
public class ResidentDocument
{
    /// <summary>
    /// Primary key for the ResidentDocument entity, uniquely identifying each document.
    /// </summary>
    [Key]
    public Guid DocumentId { get; set; }

    /// <summary>
    /// Foreign key referencing the associated Resident entity, indicating which resident submitted the document.
    /// </summary>
    [Required]
    public Guid ResidentId { get; set; }

    /// <summary>
    /// The type of document submitted by the resident (e.g., Passport, ID Card, Driver's License), with a maximum length of 100 characters.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string DocumentType { get; set; } = string.Empty;

    /// <summary>
    /// The unique number or identifier associated with the document (e.g., passport number, ID card number), with a maximum length of 100 characters.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string DocumentNumber { get; set; } = string.Empty;

    /// <summary>
    /// The URL or file path where the document is stored, with a maximum length of 500 characters.
    /// This property holds the location of the document file, allowing for retrieval and verification of the submitted document.
    /// </summary>
    [Required]
    [StringLength(500)]
    public string FileUrl { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp indicating when the document was uploaded, represented as a DateTime value.
    /// </summary>
    public DateTime UploadedAt { get; set; }
    
    /// <summary>
    /// Indicates whether the document has been verified by the hostel administration. A value of true means the document is verified, while false indicates it is pending verification.
    /// </summary>
    public bool IsVerified { get; set; }

    /// <summary>
    /// Foreign key referencing the user who verified the document, if applicable. Nullable to allow for documents that have not yet been verified.
    /// </summary>
    public int? VerifiedBy { get; set; }

    /// <summary>
    /// Timestamp indicating when the document was verified, if applicable. Nullable to allow for documents that have not yet been verified.
    /// </summary>
    public DateTime? VerifiedAt { get; set; }

    /// <summary>
    /// Navigation property to the associated Resident entity, representing the relationship between ResidentDocument and Resident.
    /// </summary>
    [ForeignKey("ResidentId")]
    public Resident Resident { get; set; } = null!;
}
}