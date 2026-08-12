using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class StaffRole
    {
        [Key]
        //The unique identifier for the staff role, represented as an integer. This property serves as the primary key for the StaffRole entity.
        public int StaffRoleId { get; set; }

        [Required]
        [StringLength(100)]
        //The name of the staff role, with a maximum length of 100 characters. This property is required and cannot be null or empty.
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        //Optional description of the staff role, providing additional details or information about the role, with a maximum length of 500 characters. This property can be null if no description is provided.
        public string? Description { get; set; }

        [Required]
        //  Indicates whether the staff role is currently active and available for assignment, represented as a boolean value. This property is required and cannot be null.
        public bool IsActive { get; set; }

        [Required]
        //Timestamp indicating when the staff role record was created, represented as a DateTime value. This property is required and cannot be null.
        public DateTime CreatedAt { get; set; }

        //Navigation property to the collection of associated Staff entities, representing the staff members assigned to this role. This property allows access to the details of staff members associated with this role.
        public ICollection<Staff> Staffs { get; set; } = new List<Staff>();
    }
}