using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class AuditLog
    {
        [Key]
        // Primary key for the AuditLog entity.
        public long AuditLogId { get; set; }


        [Required]
        //Foreign key that identifies the user who performed the action being logged.
        public Guid UserId { get; set; }


        [Required]
        [StringLength(100)]
        // The action performed by the user (e.g., Create, Update, Delete) with a maximum length of 100 characters.
        public string Action { get; set; } = string.Empty;


        [Required]
        [StringLength(100)]
        // The name of the entity on which the action was performed (e.g., Bed, Room, Floor) with a maximum length of 100 characters.
        public string Entity { get; set; } = string.Empty;
  
        //Id referinring to  the specific record being audited.
        public int? EntityId { get; set; }

        [StringLength(500)]
        // Optional details about the action performed, with a maximum length of 500 characters. This
        public string? Details { get; set; }


        [StringLength(50)]
        // Optional IP address from which the action was performed, with a maximum length of 50 characters.
        public string? IpAddress { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}