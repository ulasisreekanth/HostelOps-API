using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
public class Complaint
{
    /// <summary>
    /// Primary key for the Complaint entity.
    /// </summary>
    [Key]
    public Guid ComplaintId { get; set; }

    /// <summary>
    /// Foreign key referencing the associated Hostel entity.
    /// </summary>
    [Required]
    public Guid? ResidentId { get; set; }

    /// <summary>
    /// Foreign key referencing the associated Staff entity (if applicable).
    /// </summary>
    [Required]
    public Guid? StaffId { get; set; }


    /// <summary>
    /// The subject or title of the complaint, with a maximum length of 200 characters.
    /// </summary>
    [Required]
    [StringLength(200)]
    public string Subject { get; set; } = string.Empty;


    /// <summary>
    /// Detailed description of the complaint, with a maximum length of 1000 characters.
    /// </summary>
    [Required]
    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;


    /// <summary>
    /// The priority level of the complaint (e.g., Low, Medium, High).
    /// </summary>
    [Required]
    public ComplaintPriority Priority { get; set; }


    /// <summary>
    /// The current status of the complaint (e.g., Open, In Progress, Resolved).
    /// </summary>
    [Required]
    public ComplaintStatus Status { get; set; }

    /// <summary>
    /// Timestamp indicating when the complaint was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Timestamp indicating when the complaint was last updated.
    /// </summary>
    public DateTime? ResolvedAt { get; set; }


    /// <summary>
    /// Navigation property to the associated Resident entity, representing the resident who filed the complaint.
    /// </summary>
    [ForeignKey("ResidentId")]
    public Resident? Resident { get; set; }


    /// <summary>
    /// Navigation property to the associated Staff entity, representing the staff member assigned to handle the complaint.
    /// </summary>
    [ForeignKey("StaffId")]
    public Staff? Staff { get; set; }
}
}