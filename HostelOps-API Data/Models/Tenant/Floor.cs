using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    [Table("Floors")]
    public class Floor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FloorId { get; set; }

        [Required]
        public int BuildingId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int FloorNumber { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public FloorStatus Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [ForeignKey(nameof(BuildingId))]
        public virtual Building Building { get; set; } = null!;

        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}