using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class Complaint
    {
        [Key]
        public int ComplaintId { get; set; }


        public int? ResidentId { get; set; }


        public int? StaffId { get; set; }


        [Required]
        [StringLength(200)]
        public string Subject { get; set; } = string.Empty;


        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;


        [Required]
        public ComplaintPriority Priority { get; set; }


        [Required]
        public ComplaintStatus Status { get; set; }


        public DateTime CreatedAt { get; set; }


        public DateTime? ResolvedAt { get; set; }



        [ForeignKey("ResidentId")]
        public Resident? Resident { get; set; }



        [ForeignKey("StaffId")]
        public Staff? Staff { get; set; }
    }
}