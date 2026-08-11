using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    [Table("Notifications")]
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NotificationId { get; set; }


        public int? StaffId { get; set; }


        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;


        [Required]
        [StringLength(1000)]
        public string Message { get; set; } = string.Empty;


        [Required]
        public NotificationType Type { get; set; }


        [Required]
        public bool IsRead { get; set; }


        public DateTime CreatedAt { get; set; }



        [ForeignKey("StaffId")]
        public Staff? Staff { get; set; }
    }
}