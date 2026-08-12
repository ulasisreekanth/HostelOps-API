using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class Notification
    {
        [Key]
        //Primary key for the Notification entity, uniquely identifying each notification.
        public long NotificationId { get; set; }

        [Required]
        //Foreign key referencing the associated staff member to whom this notification is related.
        public int? StaffId { get; set; }


        [Required]
        [StringLength(200)]
        //The title of the notification, with a maximum length of 200 characters.
        public string Title { get; set; } = string.Empty;


        [Required]
        [StringLength(500)]
        //The message content of the notification, with a maximum length of 500 characters.
        public string Message { get; set; } = string.Empty;


        [Required]
        //The type of the notification, represented by the NotificationType enum.
        public NotificationType Type { get; set; }


        [Required]
        //Indicates whether the notification has been read by the recipient staff member.
        public bool IsRead { get; set; }


        public DateTime CreatedAt { get; set; }



        [ForeignKey("StaffId")]
        //Navigation property to the associated Staff entity, representing the relationship between Notification and Staff.
        public Staff? Staff { get; set; }
    }
}