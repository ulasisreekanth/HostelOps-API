using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
public class Notification
{
    /// <summary>
    /// Primary key for the Notification entity, uniquely identifying each notification.
    /// </summary>
    [Key]
    public Guid NotificationId { get; set; }

    /// <summary>
    /// Foreign key referencing the associated staff member to whom this notification is related.
    /// </summary>
    [Required]
    public Guid? StaffId { get; set; }


    /// <summary>
    /// The title of the notification, with a maximum length of 200 characters.
    /// </summary>
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;


    /// <summary>
    /// The message content of the notification, with a maximum length of 500 characters.
    /// </summary>
    [Required]
    [StringLength(500)]
    public string Message { get; set; } = string.Empty;


    /// <summary>
    /// The type of the notification, represented by the NotificationType enum.
    /// </summary>
    [Required]
    public NotificationType Type { get; set; }


    /// <summary>
    /// Indicates whether the notification has been read by the recipient staff member.
    /// </summary>
    [Required]
    public bool IsRead { get; set; }


    public DateTime CreatedAt { get; set; }



    /// <summary>
    /// Navigation property to the associated Staff entity, representing the relationship between Notification and Staff.
    /// </summary>
    [ForeignKey("StaffId")]
    public Staff? Staff { get; set; }
}
}