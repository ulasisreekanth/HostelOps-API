using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class Hostel
    {
        [Key]
        //Primary key for the Hostel entity, uniquely identifying each hostel.
        public int HostelId { get; set; }

        [Required]
        [StringLength(150)]
        //The name of the hostel, with a maximum length of 150 characters.
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        //A unique code representing the hostel, with a maximum length of 20 characters.
        public string Code { get; set; } = string.Empty;
        
        [Required]
        [StringLength(500)]
        // description of the hostel, with a maximum length of 500 characters.
        public string? Address { get; set; }

        [StringLength(100)]
        //Optional city where the hostel is located, with a maximum length of 100 characters.
        public string? City { get; set; }

        [StringLength(100)]
        //Optional state where the hostel is located, with a maximum length of 100 characters.
        public string? State { get; set; }

        [StringLength(10)]
        //Optional postal code (pincode) for the hostel's location, with a maximum length of 10 characters.
        public string? Pincode { get; set; }

        [StringLength(100)]
        //Optional country where the hostel is located, with a maximum length of 100 characters.
        public string? Country { get; set; }

        [Phone]
        [StringLength(12)]
        // contact phone number for the hostel, with a maximum length of 12 characters.
        public string? Phone { get; set; }

        [EmailAddress]
        [StringLength(20)]
        //Optional contact email address for the hostel, with a maximum length of 20 characters.
        public string? Email { get; set; }

        [StringLength(500)]
        //Optional URL to the hostel's logo image, with a maximum length of 500 characters.
        public string? LogoUrl { get; set; }

        [Required]
        [StringLength(20)]
        //The timezone in which the hostel operates, with a maximum length of 20 characters. Defaults to "UTC".
        public string Timezone { get; set; } = "UTC";

        [Required]
        //The current status of the hostel, represented by the HostelStatus enum (e.g., Active, Inactive).
        public HostelStatus Status { get; set; }

        [Required]
        //Timestamp indicating when the hostel record was created.
        public DateTime CreatedAt { get; set; }

        [Required]
        //Timestamp indicating when the hostel record was last updated.
        public DateTime UpdatedAt { get; set; }
        
        //Navigation property to the collection of Building entities associated with this Hostel, representing the buildings within the hostel.
        public virtual ICollection<Building> Buildings { get; set; } = new List<Building>();
    }
}