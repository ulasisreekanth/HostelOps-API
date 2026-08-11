using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    [Table("Staff")]
    public class Staff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StaffId { get; set; }


        [Required]
        public int StaffRoleId { get; set; }


        [Required]
        [StringLength(150)]
        public string FullName { get; set; } = string.Empty;


        [Phone]
        [StringLength(20)]
        public string? Phone { get; set; }


        [EmailAddress]
        [StringLength(150)]
        public string? Email { get; set; }


        public DateOnly? DateOfBirth { get; set; }


        public Gender? Gender { get; set; }


        [StringLength(500)]
        public string? Address { get; set; }


        [Column(TypeName = "decimal(10,2)")]
        public decimal Salary { get; set; }


        [Required]
        public DateOnly JoinDate { get; set; }


        [Required]
        public StaffStatus Status { get; set; }


        public DateTime CreatedAt { get; set; }


        public DateTime UpdatedAt { get; set; }



        [ForeignKey("StaffRoleId")]
        public StaffRole StaffRole { get; set; } = null!;


        public ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();


        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}