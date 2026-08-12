using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class Room
    {
        [Key]
        //Primary key for the Room entity, uniquely identifying each room in the database.
        public Guid RoomId { get; set; }

        [Required]
        //Foreign key referencing the associated Floor entity, indicating which floor the room is located on.
        public Guid FloorId { get; set; }

        [Required]
        [StringLength(20)]
        //The unique number or identifier assigned to the room, with a maximum length of 20 characters.
        public string RoomNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        //The name or designation of the room, with a maximum length of 100 characters.
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        //Optional description of the room, providing additional details or information about the room, with a maximum length of 500 characters.
        public string? Description { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        //  The area of the room in square feet, represented as a decimal value with a precision of 10 and scale of 2. This property is optional and can be null if the area is not specified.
        public decimal? AreaSqft { get; set; }

        [Required]
        //  The current status of the room, represented by the RoomStatus enum (e.g., Available, Occupied, UnderMaintenance). This property is required and cannot be null.
        public RoomStatus Status { get; set; }

        [Required]
        //Timestamp indicating when the room record was created, represented as a DateTime value.
        public DateTime CreatedAt { get; set; }

        [Required]
        //Timestamp indicating when the room record was last updated, represented as a DateTime value.
        public DateTime UpdatedAt { get; set; }

        [ForeignKey(nameof(FloorId))]
        //Navigation property to the associated Floor entity, representing the relationship between Room and Floor. This property allows access to the details of the floor on which the room is located.
        public virtual Floor Floor { get; set; } = null!;

        //Navigation property to the collection of associated Bed entities, representing the beds available in the room. This property allows access to the details of the beds within the room.
        public virtual ICollection<Bed> Beds { get; set; } = new List<Bed>();
    }
}