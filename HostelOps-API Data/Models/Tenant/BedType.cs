using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class BedType
    {
        [Key]
        // Primary key for the BedType entity.
        public int BedTypeId { get; set; }

        [Required]
        [StringLength(100)]
        // The name of the bed type, with a maximum length of 100 characters.
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        // Optional description of the bed type, with a maximum length of 500 characters.
        public string? Description { get; set; }

        [Required]
        // The maximum number of occupants allowed for this bed type.
        public bool IsActive { get; set; }

        [Required]
        // Timestamp indicating when the bed type record was created.
        public DateTime CreatedAt { get; set; }
    }
}