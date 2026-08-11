using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class Inquiry
    {
        [Key]
        public int InquiryId { get; set; }

        [Required]
        [StringLength(150)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [Phone]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(150)]
        public string? Email { get; set; }

        [Required]
        public DateOnly PreferredCheckIn { get; set; }

        [Required]
        public DateOnly PreferredCheckOut { get; set; }

        [Required]
        public int SharingTypeId { get; set; }

        [Required]
        public int RoomTypeId { get; set; }

        [StringLength(500)]
        public string? Message { get; set; }

        [Required]
        public InquirySource Source { get; set; }

        [Required]
        public InquiryStatus Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("SharingTypeId")]
        public SharingType SharingType { get; set; } = null!;

        [ForeignKey("RoomTypeId")]
        public RoomType RoomType { get; set; } = null!;

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}