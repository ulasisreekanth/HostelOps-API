using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    [Table("SharingTypes")]
    public class SharingType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SharingTypeId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public int MaxOccupants { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Inquiry> Inquiries { get; set; } = new List<Inquiry>();

        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}