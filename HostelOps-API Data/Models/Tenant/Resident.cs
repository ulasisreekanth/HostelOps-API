using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class Resident
    {
        [Key]
        public int ResidentId { get; set; }

        [Required]
        [StringLength(150)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        public Gender Gender { get; set; }

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

        [Required]
        public ResidentStatus Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        public ICollection<StayAllocation> StayAllocations { get; set; } = new List<StayAllocation>();

        public ICollection<ResidentDocument> ResidentDocuments { get; set; } = new List<ResidentDocument>();

        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

        public ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();
    }
}