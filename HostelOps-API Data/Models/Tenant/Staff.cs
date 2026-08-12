using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class Staff
    {
        [Key]
        //Primary key for the Staff entity, uniquely identifying each staff member record in the database.
        public int StaffId { get; set; }


        [Required]
        //Foreign key referencing the StaffRole entity, indicating the role assigned to the staff member. This property is required and cannot be null.
        public int StaffRoleId { get; set; }


        [Required]
        [StringLength(150)]
        //The full name of the staff member, with a maximum length of 150 characters. This property is required and cannot be null or empty.
        public string FullName { get; set; } = string.Empty;


        [Phone]
        [StringLength(20)]
        //The phone number of the staff member, with a maximum length of 20 characters and validated for proper phone number format. This property is optional and can be null.
        public string? Phone { get; set; }


        [EmailAddress]
        [StringLength(150)]
        //The email address of the staff member, with a maximum length of 150 characters and validated for proper email format. This property is optional and can be null.
        public string? Email { get; set; }

        //The date of birth of the staff member, represented as a DateOnly value. This property is optional and can be null.
        public DateOnly? DateOfBirth { get; set; }

        //The gender of the staff member, represented by the Gender enum (e.g., Male, Female, Other). This property is optional and can be null.
        public Gender? Gender { get; set; }


        [StringLength(500)]
        //Optional address of the staff member, with a maximum length of 500 characters. This property can be null if the address is not provided.
        public string? Address { get; set; }


        [Column(TypeName = "decimal(10,2)")]
        //The salary of the staff member, represented as a decimal value with a precision of 10 and scale of 2. This property is optional and can be null if the salary is not specified.
        public decimal Salary { get; set; }


        [Required]
        //The date when the staff member joined, represented as a DateOnly value. This property is required and cannot be null.
        public DateOnly JoinDate { get; set; }


        [Required]
        //The current status of the staff member, represented by the StaffStatus enum (e.g., Active, Inactive, Suspended). This property is required and cannot be null.
        public StaffStatus Status { get; set; }

        //Timestamp indicating when the staff member record was created, represented as a DateTime value. This property is required and cannot be null.
        public DateTime CreatedAt { get; set; }


        public DateTime UpdatedAt { get; set; }



        [ForeignKey("StaffRoleId")]
        //Navigation property to the associated StaffRole entity, representing the role assigned to the staff member. This property allows access to the details of the staff role associated with this staff member.
        public StaffRole StaffRole { get; set; } = null!;

        //Navigation property to the collection of associated Complaint entities, representing the complaints made by this staff member. This property allows access to the details of complaints filed by this staff member.
        public ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();

        //Navigation property to the collection of associated Notification entities, representing the notifications received by this staff member. This property allows access to the details of notifications sent to this staff member.
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}