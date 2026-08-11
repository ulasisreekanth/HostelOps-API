using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class AuditLog
    {
        [Key]
        public long AuditLogId { get; set; }


        [Required]
        public int UserId { get; set; }


        [Required]
        [StringLength(100)]
        public string Action { get; set; } = string.Empty;


        [Required]
        [StringLength(100)]
        public string Entity { get; set; } = string.Empty;


        public int? EntityId { get; set; }


        public string? Details { get; set; }


        [StringLength(50)]
        public string? IpAddress { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}