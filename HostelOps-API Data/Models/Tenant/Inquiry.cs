using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
public class Inquiry
{
    /// <summary>
    /// Primary key for the Inquiry entity, uniquely identifying each inquiry record.
    /// </summary>
    [Key]
    public Guid InquiryId { get; set; }

    /// <summary>
    /// The full name of the person making the inquiry, with a maximum length of 150 characters.
    /// </summary>
    [Required]
    [StringLength(150)]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// The phone number of the person making the inquiry, with a maximum length of 20 characters.
    /// </summary>
    [Required]
    [Phone]
    [StringLength(20)]
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// The email address of the person making the inquiry, with a maximum length of 150 characters.
    /// </summary>
    [EmailAddress]
    [StringLength(150)]
    public string? Email { get; set; }

    /// <summary>
    /// The ID of the hostel to which the inquiry is related, serving as a foreign key to the Hostel entity.
    /// </summary>
    [Required]
    public DateOnly PreferredCheckIn { get; set; }

    /// <summary>
    /// The preferred check-out date for the inquiry, represented as a DateOnly value.
    /// </summary>
    [Required]
    public DateOnly PreferredCheckOut { get; set; }

    /// <summary>
    /// The ID of the sharing type associated with the inquiry, serving as a foreign key to the SharingType entity.
    /// </summary>
    [Required]
    public Guid SharingTypeId { get; set; }

    /// <summary>
    /// The ID of the room type associated with the inquiry, serving as a foreign key to the RoomType entity.
    /// </summary>
    [Required]
    public Guid RoomTypeId { get; set; }

    /// <summary>
    /// Optional message or additional information provided by the person making the inquiry, with a maximum length of 500 characters.
    /// </summary>
    [StringLength(500)]
    public string? Message { get; set; }

    /// <summary>
    /// The source from which the inquiry originated, represented by the InquirySource enum (e.g., Website, Phone, Walk-in).
    /// </summary>
    [Required]
    public InquirySource Source { get; set; }

    /// <summary>
    /// The current status of the inquiry, represented by the InquiryStatus enum (e.g., New, In Progress, Closed).
    /// </summary>
    [Required]
    public InquiryStatus Status { get; set; }

    /// <summary>
    /// Timestamp indicating when the inquiry record was created.
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Timestamp indicating when the inquiry record was last updated.
    /// </summary>
    [Required]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Navigation property to the associated SharingType entity, representing the relationship between Inquiry and SharingType.
    /// </summary>
    [ForeignKey("SharingTypeId")]
    public SharingType SharingType { get; set; } = null!;

    /// <summary>
    /// Navigation property to the associated RoomType entity, representing the relationship between Inquiry and RoomType.
    /// </summary>
    [ForeignKey("RoomTypeId")]
    public RoomType RoomType { get; set; } = null!;

    /// <summary>
    /// Navigation property to the associated Hostel entity, representing the relationship between Inquiry and Hostel.
    /// </summary>
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
}