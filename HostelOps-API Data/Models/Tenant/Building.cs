using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class Building
    {
        [Key]
        // Primary key for the Building entity.
        public int BuildingId { get; set; }

        [Required]
        // Foreign key referencing the associated Hostel entity.
        public int HostelId { get; set; }

        [Required]
        [StringLength(100)]
        // The name of the building, with a maximum length of 100 characters.
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        // Optional description of the building, with a maximum length of 500 characters.
        public string? Description { get; set; }

        [Required]
        // The current status of the building (e.g., Active, Inactive).
        public BuildingStatus Status { get; set; }

        [Required]
        // Timestamp indicating when the building record was created.
        public DateTime CreatedAt { get; set; }

        [Required]
        // Timestamp indicating when the building record was last updated.
        public DateTime UpdatedAt { get; set; }

        [ForeignKey(nameof(HostelId))]
        // Navigation property to the associated Hostel entity, representing the relationship between Building and Hostel.
        public virtual Hostel Hostel { get; set; } = null!;
        
        // Navigation property to the collection of Floor entities associated with this Building, representing the floors within the building.
        public virtual ICollection<Floor> Floors { get; set; } = new List<Floor>();
    }
}