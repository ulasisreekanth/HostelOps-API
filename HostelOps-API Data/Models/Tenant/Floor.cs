using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class Floor
    {
        [Key]
        // Primary key of the Floor entity.
        public Guid FloorId { get; set; }

        [Required]
        // Foreign key that identifies which Building this Floor belongs to.
        public Guid BuildingId { get; set; }

        [Required]
        [StringLength(100)]
         // Name of the floor & max length allowesniss 100 characters.
        public string Name { get; set; } = string.Empty;

        [Required]
        // The floor number within the building.
        public int FloorNumber { get; set; }

        [StringLength(500)]
        // Optional description of the floor, with a maximum length of 500 characters.
        public string? Description { get; set; }

        [Required]
        // The current status of the floor (e.g., Active, Inactive).
        public FloorStatus Status { get; set; }

        [Required]
        // Timestamp indicating when the floor record was created.
        public DateTime CreatedAt { get; set; }

        [Required]
        // Timestamp indicating when the floor record was last updated.
        public DateTime UpdatedAt { get; set; }

        // Navigation property to the associated Building entity, it represents the relationship between Floor and Building.
        [ForeignKey(nameof(BuildingId))]
        public virtual Building Building { get; set; } = null!;

        // Navigation property to the collection of Room entities associated with this Floor.
        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}