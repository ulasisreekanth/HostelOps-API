using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
    public class Setting
    {
        [Key]
        //primary key for the Setting entity, uniquely identifying each setting record in the database.
        public Guid SettingId { get; set; }


        [Required]
        [StringLength(100)]
        //The key or name of the setting, with a maximum length of 100 characters. This property is required and cannot be null or empty.
        public string SettingKey { get; set; } = string.Empty;


        [Required]
        //  The value associated with the setting, represented as a string. This property is required and cannot be null or empty.
        public string SettingValue { get; set; } = string.Empty;


        [StringLength(500)]
        //Optional description of the setting, providing additional details or information about the setting, with a maximum length of 500 characters.
        public string? Description { get; set; }

        //Timestamp indicating when the setting record was created, represented as a DateTime value.
        public DateTime CreatedAt { get; set; }

        //Timestamp indicating when the setting record was last updated, represented as a DateTime value.
        public DateTime UpdatedAt { get; set; }
    }
}