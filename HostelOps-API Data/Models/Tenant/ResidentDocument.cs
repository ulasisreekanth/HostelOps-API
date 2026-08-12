using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class ResidentDocument
    {
        [Key]
        //Primary key for the ResidentDocument entity, uniquely identifying each document.
        public Guid DocumentId { get; set; }

        [Required]
        //Foreign key referencing the associated Resident entity, indicating which resident submitted the document.
        public Guid ResidentId { get; set; }

        [Required]
        [StringLength(100)]
        //The type of document submitted by the resident (e.g., Passport, ID Card, Driver's License), with a maximum length of 100 characters.
        public string DocumentType { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        //The unique number or identifier associated with the document (e.g., passport number, ID card number), with a maximum length of 100 characters.
        public string DocumentNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        //The URL or file path where the document is stored, with a maximum length of 500 characters.

        //This property holds the location of the document file, allowing for retrieval and verification of the submitted document.
        public string FileUrl { get; set; } = string.Empty;

        //Timestamp indicating when the document was uploaded, represented as a DateTime value.
        public DateTime UploadedAt { get; set; }
        
        //  Indicates whether the document has been verified by the hostel administration. A value of true means the document is verified, while false indicates it is pending verification.
        public bool IsVerified { get; set; }

        //Foreign key referencing the user who verified the document, if applicable. Nullable to allow for documents that have not yet been verified.
        public int? VerifiedBy { get; set; }

        //Timestamp indicating when the document was verified, if applicable. Nullable to allow for documents that have not yet been verified.
        public DateTime? VerifiedAt { get; set; }

        [ForeignKey("ResidentId")]
        //Navigation property to the associated Resident entity, representing the relationship between ResidentDocument and Resident.
        public Resident Resident { get; set; } = null!;
    }
}