using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class Resident
    {
        [Key]
        //Primary key for the Resident entity, representing the unique identifier for each resident.
        public int ResidentId { get; set; }

        [Required]
        [StringLength(150)]
        //The full name of the resident, with a maximum length of 150 characters.
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        //The email address of the resident, with a maximum length of 150 characters and validated for proper email format.
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        [StringLength(20)]
        //The phone number of the resident, with a maximum length of 20 characters and validated for proper phone number format.
        public string Phone { get; set; } = string.Empty;

        [Required]
        //  The date of birth of the resident, represented as a DateOnly value.
        public DateOnly DateOfBirth { get; set; }

        [Required]
        //The gender of the resident, represented by the Gender enum (e.g., Male, Female, Other).
        public Gender Gender { get; set; }

        [StringLength(500)]
        //Optional address of the resident, with a maximum length of 500 characters.
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
        //The current status of the resident, represented by the ResidentStatus enum (e.g., Active, Inactive, Suspended).
        public ResidentStatus Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
        
        // Navigation property to the collection of associated Reservation entities, representing the reservations made by this resident.
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        // Navigation property to the collection of associated StayAllocation entities, representing the stay allocations linked to this resident.
        public ICollection<StayAllocation> StayAllocations { get; set; } = new List<StayAllocation>();

        // Navigation property to the collection of associated ResidentDocument entities, representing the documents submitted by this resident.
        public ICollection<ResidentDocument> ResidentDocuments { get; set; } = new List<ResidentDocument>();

        // Navigation property to the collection of associated Payment entities, representing the payments made by this resident.

        // Navigation property to the collection of associated Payment entities, representing the payments made by this resident.
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

        // Navigation property to the collection of associated Complaint entities, representing the complaints lodged by this resident.
        public ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();
    }
}