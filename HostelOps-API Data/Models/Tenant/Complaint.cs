using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class Complaint
    {
        [Key]
        // Primary key for the Complaint entity.
        public int ComplaintId { get; set; }

        [Required]
        // Foreign key referencing the associated Hostel entity.
        public int? ResidentId { get; set; }

        [Required]
        // Foreign key referencing the associated Staff entity (if applicable).
        public int? StaffId { get; set; }


        [Required]
        [StringLength(200)]
        // The subject or title of the complaint, with a maximum length of 200 characters.
        public string Subject { get; set; } = string.Empty;


        [Required]
        [StringLength(1000)]
        // Detailed description of the complaint, with a maximum length of 1000 characters.
        public string Description { get; set; } = string.Empty;


        [Required]
        // The priority level of the complaint (e.g., Low, Medium, High).
        public ComplaintPriority Priority { get; set; }


        [Required]
        // The current status of the complaint (e.g., Open, In Progress, Resolved).
        public ComplaintStatus Status { get; set; }

        // Timestamp indicating when the complaint was created.
        public DateTime CreatedAt { get; set; }

        // Timestamp indicating when the complaint was last updated.
        public DateTime? ResolvedAt { get; set; }


        // Navigation property to the associated Resident entity, representing the resident who filed the complaint.
        [ForeignKey("ResidentId")]
        public Resident? Resident { get; set; }


        // Navigation property to the associated Staff entity, representing the staff member assigned to handle the complaint.
        [ForeignKey("StaffId")]
        public Staff? Staff { get; set; }
    }
}