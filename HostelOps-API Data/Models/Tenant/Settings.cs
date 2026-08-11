using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class Setting
    {
        [Key]
        public int SettingId { get; set; }


        [Required]
        [StringLength(100)]
        public string SettingKey { get; set; } = string.Empty;


        [Required]
        public string SettingValue { get; set; } = string.Empty;


        [StringLength(500)]
        public string? Description { get; set; }


        public DateTime CreatedAt { get; set; }


        public DateTime UpdatedAt { get; set; }
    }
}