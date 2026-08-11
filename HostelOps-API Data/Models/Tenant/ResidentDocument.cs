using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class ResidentDocument
    {
        [Key]
        public int DocumentId { get; set; }

        [Required]
        public int ResidentId { get; set; }

        [Required]
        [StringLength(100)]
        public string DocumentType { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string DocumentNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string FileUrl { get; set; } = string.Empty;

        public DateTime UploadedAt { get; set; }

        public bool IsVerified { get; set; }

        public int? VerifiedBy { get; set; }

        public DateTime? VerifiedAt { get; set; }

        [ForeignKey("ResidentId")]
        public Resident Resident { get; set; } = null!;
    }
}