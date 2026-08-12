using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class Inquiry
    {
        [Key]
        //primary key for the Inquiry entity, uniquely identifying each inquiry record.
        public int InquiryId { get; set; }

        [Required]
        [StringLength(150)]
        //The full name of the person making the inquiry, with a maximum length of 150 characters.
        public string FullName { get; set; } = string.Empty;

        [Required]
        [Phone]
        [StringLength(20)]
        //The phone number of the person making the inquiry, with a maximum length of 20 characters.
        public string Phone { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(150)]
        //The email address of the person making the inquiry, with a maximum length of 150 characters.  
        public string? Email { get; set; }

        [Required]
        //The ID of the hostel to which the inquiry is related, serving as a foreign key to the Hostel entity.
        public DateOnly PreferredCheckIn { get; set; }

        [Required]
        //The preferred check-out date for the inquiry, represented as a DateOnly value.
        public DateOnly PreferredCheckOut { get; set; }

        [Required]
        //The ID of the sharing type associated with the inquiry, serving as a foreign key to the SharingType entity.
        public int SharingTypeId { get; set; }

        [Required]
        //The ID of the room type associated with the inquiry, serving as a foreign key to the RoomType entity.
        public int RoomTypeId { get; set; }

        [StringLength(500)]
        //  Optional message or additional information provided by the person making the inquiry, with a maximum length of 500 characters.
        public string? Message { get; set; }

        [Required]
        //The source from which the inquiry originated, represented by the InquirySource enum (e.g., Website, Phone, Walk-in).
        public InquirySource Source { get; set; }

        [Required]
        //The current status of the inquiry, represented by the InquiryStatus enum (e.g., New, In Progress, Closed).
        public InquiryStatus Status { get; set; }

        [Required]
        //Timestamp indicating when the inquiry record was created.
        public DateTime CreatedAt { get; set; }

        [Required]
        //Timestamp indicating when the inquiry record was last updated.
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("SharingTypeId")]
        //Navigation property to the associated SharingType entity, representing the relationship between Inquiry and SharingType.
        public SharingType SharingType { get; set; } = null!;

        [ForeignKey("RoomTypeId")]
        //Navigation property to the associated RoomType entity, representing the relationship between Inquiry and RoomType.
        public RoomType RoomType { get; set; } = null!;

        //Navigation property to the associated Hostel entity, representing the relationship between Inquiry and Hostel.
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}