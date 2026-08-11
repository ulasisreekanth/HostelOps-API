using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        [Required]
        public int FloorId { get; set; }

        [Required]
        [StringLength(20)]
        public string RoomNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? AreaSqft { get; set; }

        [Required]
        public RoomStatus Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [ForeignKey(nameof(FloorId))]
        public virtual Floor Floor { get; set; } = null!;

        public virtual ICollection<Bed> Beds { get; set; } = new List<Bed>();
    }
}