using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
public class Staff
{
    /// <summary>
    /// Primary key for the Staff entity, uniquely identifying each staff member record in the database.
    /// </summary>
    [Key]
    public Guid StaffId { get; set; }

    /// <summary>
    /// Foreign key referencing the StaffRole entity, indicating the role assigned to the staff member. This property is required and cannot be null.
    /// </summary>
    [Required]
    public Guid StaffRoleId { get; set; }


    /// <summary>
    /// The full name of the staff member, with a maximum length of 150 characters. This property is required and cannot be null or empty.
    /// </summary>
    [Required]
    [StringLength(150)]
    public string FullName { get; set; } = string.Empty;


    /// <summary>
    /// The phone number of the staff member, with a maximum length of 20 characters and validated for proper phone number format. This property is optional and can be null.
    /// </summary>
    [Phone]
    [StringLength(20)]
    public string? Phone { get; set; }


    /// <summary>
    /// The email address of the staff member, with a maximum length of 150 characters and validated for proper email format. This property is optional and can be null.
    /// </summary>
    [EmailAddress]
    [StringLength(150)]
    public string? Email { get; set; }

    /// <summary>
    /// The date of birth of the staff member, represented as a DateOnly value. This property is optional and can be null.
    /// </summary>
    public DateOnly? DateOfBirth { get; set; }

    /// <summary>
    /// The gender of the staff member, represented by the Gender enum (e.g., Male, Female, Other). This property is optional and can be null.
    /// </summary>
    public Gender? Gender { get; set; }


    /// <summary>
    /// Optional address of the staff member, with a maximum length of 500 characters. This property can be null if the address is not provided.
    /// </summary>
    [StringLength(500)]
    public string? Address { get; set; }


    /// <summary>
    /// The salary of the staff member, represented as a decimal value with a precision of 10 and scale of 2. This property is optional and can be null if the salary is not specified.
    /// </summary>
    [Column(TypeName = "decimal(10,2)")]
    public decimal Salary { get; set; }


    /// <summary>
    /// The date when the staff member joined, represented as a DateOnly value. This property is required and cannot be null.
    /// </summary>
    [Required]
    public DateOnly JoinDate { get; set; }


    /// <summary>
    /// The current status of the staff member, represented by the StaffStatus enum (e.g., Active, Inactive, Suspended). This property is required and cannot be null.
    /// </summary>
    [Required]
    public StaffStatus Status { get; set; }

    /// <summary>
    /// Timestamp indicating when the staff member record was created, represented as a DateTime value. This property is required and cannot be null.
    /// </summary>
    public DateTime CreatedAt { get; set; }


    public DateTime UpdatedAt { get; set; }



    /// <summary>
    /// Navigation property to the associated StaffRole entity, representing the role assigned to the staff member. This property allows access to the details of the staff role associated with this staff member.
    /// </summary>
    [ForeignKey("StaffRoleId")]
    public StaffRole StaffRole { get; set; } = null!;

    /// <summary>
    /// Navigation property to the collection of associated Complaint entities, representing the complaints made by this staff member. This property allows access to the details of complaints filed by this staff member.
    /// </summary>
    public ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();

    /// <summary>
    /// Navigation property to the collection of associated Notification entities, representing the notifications received by this staff member. This property allows access to the details of notifications sent to this staff member.
    /// </summary>
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
}