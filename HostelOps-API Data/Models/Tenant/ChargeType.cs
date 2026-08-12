using HostelOps_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class ChargeType
    {
        [Key]
        // Primary key for the ChargeType entity.
        public Guid ChargeTypeId { get; set; }

        [Required]
        [StringLength(100)]
        // The name of the charge type, with a maximum length of 100 characters.
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        // Optional description of the charge type, with a maximum length of 500 characters.
        public string? Description { get; set; }

        [Required]
        // The category of the charge type, indicating whether it's a one-time or recurring charge.
        public ChargeCategory Category { get; set; }
        
        // Indicates whether the charge type is recurring (true) or one-time (false).
        public bool IsRecurring { get; set; }
        
        // Indicates whether the charge type is mandatory (true) or optional (false).
        public bool IsMandatory { get; set; }
        
        // Indicates whether the charge type is active (true) or inactive (false).
        public bool IsActive { get; set; }
        
        // Timestamp indicating when the charge type record was created.
        public DateTime CreatedAt { get; set; }
        
        //Navigation property to the collection of InvoiceItem entities associated with this ChargeType, representing the invoice items that use this charge type.
        public ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
    }
}