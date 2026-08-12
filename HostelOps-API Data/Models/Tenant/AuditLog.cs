using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HostelOps_API.Models
{
    public class AuditLog
    {
        /// <summary>
        /// Primary key for the AuditLog entity.
        /// </summary>
        [Key]
        public Guid AuditLogId { get; set; }


        /// <summary>
        /// Foreign key that identifies the user who performed the action being logged.
        /// </summary>
        [Required]
        public Guid UserId { get; set; }



        /// <summary>
        /// The action performed by the user (e.g., Create, Update, Delete) with a maximum length of 100 characters.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Action { get; set; } = string.Empty;



        /// <summary>
        /// The name of the entity on which the action was performed (e.g., Bed, Room, Floor) with a maximum length of 100 characters.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Entity { get; set; } = string.Empty;
  
        /// <summary>
        /// Id referring to the specific record being audited.
        /// </summary>
        public int? EntityId { get; set; }


        /// <summary>
        /// Optional details about the action performed, with a maximum length of 500 characters.
        /// </summary>
        [StringLength(500)]
        public string? Details { get; set; }



        /// <summary>
        /// Optional IP address from which the action was performed, with a maximum length of 50 characters.
        /// </summary>
        [StringLength(50)]
        public string? IpAddress { get; set; }


        [Required]
        public DateTime CreatedAt { get; set; }
    }
}