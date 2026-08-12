using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class RoomType
    {
        [Key]
        //Unique identifier for the RoomType entity, serving as the primary key in the database.
        public Guid RoomTypeId { get; set; }

        [Required]
        [StringLength(100)]
        //The name or designation of the room type, with a maximum length of 100 characters.
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        //Optional description of the room type, providing additional details or information about the room type, with a maximum length of 500 characters.
        public string? Description { get; set; }

        [Required]
        //The maximum number of occupants allowed for this room type, represented as an integer value.
        public int Occupants { get; set; }

        [Required]
        //Indicates whether the room type is currently active and available for use, represented as a boolean value.
        public bool IsActive { get; set; }

        [Required]
        //Timestamp indicating when the room type record was created, represented as a DateTime value.
        public DateTime CreatedAt { get; set; }

        //Navigation property to the collection of associated Inquiry entities, representing the inquiries related to this room type. This property allows access to the details of inquiries made for this room type.
        public virtual ICollection<Inquiry> Inquiries { get; set; } = new List<Inquiry>();

        //Navigation property to the collection of associated Reservation entities, representing the reservations made for this room type. This property allows access to the details of reservations associated with this room type.
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}