using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    
    public class Bed
    {
        [Key]
        public int BedId { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        [StringLength(20)]
        public string BedNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public BedStatus Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [ForeignKey(nameof(RoomId))]
        public virtual Room Room { get; set; } = null!;

        public virtual ICollection<StayAllocation> StayAllocations { get; set; } = new List<StayAllocation>();
    }
}