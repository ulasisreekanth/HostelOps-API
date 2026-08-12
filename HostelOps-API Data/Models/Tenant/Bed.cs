using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    
    public class Bed
    {
        [Key]
        // Primary key for the Bed entity.
        public int BedId { get; set; }

        [Required]
        // Foreign key that identifies the room to which the bed belongs.
        public int RoomId { get; set; }

        [Required]
        [StringLength(10)]
        // The unique number or identifier for the bed within the room, with a maximum length of 20 characters.
        public string BedNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        // The name or label for the bed, with a maximum length of 50 characters.
        public string Name { get; set; } = string.Empty;

        [Required]
        //current status of bed, represented using the BedStatus enum (e.g., Available, Occupied, Maintenance).
        public BedStatus Status { get; set; }

        [Required]
        // Timestamp indicating when the bed record was created.
        public DateTime CreatedAt { get; set; }

        [Required]
        // Timestamp indicating when the bed record was last updated.
        public DateTime UpdatedAt { get; set; }

        [ForeignKey(nameof(RoomId))]
        // Navigation property to the associated Room entity, representing the relationship between Bed and Room.
        public virtual Room Room { get; set; } = null!;
      
        // Navigation property to the collection of StayAllocation entities associated with this Bed, representing the stay allocations for this bed.
        public virtual ICollection<StayAllocation> StayAllocations { get; set; } = new List<StayAllocation>();
    }
}