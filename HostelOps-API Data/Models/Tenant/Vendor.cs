using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class Vendor
    {
        [Key]
        //Primary key for the Vendor entity, representing the unique identifier for each vendor in the system. This property is of type integer and is required for identifying individual vendors.
        public Guid VendorId { get; set; }

        [Required]
        [StringLength(150)]
        //The name of the vendor, with a maximum length of 150 characters. This property is required and cannot be null or empty, ensuring that each vendor has a valid name for identification purposes.   
        public string Name { get; set; } = string.Empty;

        [StringLength(100)]
        //Optional contact person for the vendor, with a maximum length of 100 characters. This property can be null if no contact person is specified, allowing flexibility in vendor information.
        public string? ContactPerson { get; set; }

        [Phone]
        [StringLength(20)]
        //Optional phone number for the vendor, with a maximum length of 20 characters. This property can be null if no phone number is provided, allowing vendors to have contact information if available.
        public string? Phone { get; set; }

        [EmailAddress]
        [StringLength(150)]
        //Optional email address for the vendor, with a maximum length of 150 characters. This property can be null if no email address is provided, allowing vendors to have contact information if available.
        public string? Email { get; set; }

        [StringLength(500)]
        //Optional address for the vendor, with a maximum length of 500 characters. This property can be null if no address is provided, allowing vendors to have location information if available.
        public string? Address { get; set; }

        [StringLength(100)]
        //Optional city for the vendor, with a maximum length of 100 characters. This property can be null if no city is provided, allowing vendors to have location information if available.
        public string? City { get; set; }

        [StringLength(100)]
        //Optional state for the vendor, with a maximum length of 100 characters. This property can be null if no state is provided, allowing vendors to have location information if available.
        public string? State { get; set; }

        [StringLength(10)]
        //Optional pincode for the vendor, with a maximum length of 10 characters. This property can be null if no pincode is provided, allowing vendors to have location information if available.
        public string? Pincode { get; set; }

        [StringLength(30)]
        //Optional country for the vendor, with a maximum length of 30 characters. This property can be null if no country is provided, allowing vendors to have location information if available.
        public string? GstNumber { get; set; }

        [Required]
        //Indicates whether the vendor is currently active and available for business, represented as a boolean value. This property is required and cannot be null, ensuring that each vendor has a defined status.
        public bool IsActive { get; set; }

        [Required]
        //Timestamp indicating when the vendor record was created, represented as a DateTime value. This property is required and cannot be null, providing information about the creation date of the vendor record.
        public DateTime CreatedAt { get; set; }
 
        //Navigation property to the collection of associated Expense entities, representing the expenses related to this vendor. This property allows access to the details of expenses made for this vendor.
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}