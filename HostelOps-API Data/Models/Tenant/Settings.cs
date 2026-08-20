using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelOps_API.Models
{
public class Setting
{
    /// <summary>
    /// Primary key for the Setting entity, uniquely identifying each setting record in the database.
    /// </summary>
    [Key]
    public Guid SettingId { get; set; }

    /// <summary>
    /// The key or name of the setting, with a maximum length of 100 characters. This property is required and cannot be null or empty.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string SettingKey { get; set; } = string.Empty;


    /// <summary>
    /// The value associated with the setting, represented as a string. This property is required and cannot be null or empty.
    /// </summary>
    [Required]
    public string SettingValue { get; set; } = string.Empty;


    /// <summary>
    /// Optional description of the setting, providing additional details or information about the setting, with a maximum length of 500 characters.
    /// </summary>
    [StringLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// Timestamp indicating when the setting record was created, represented as a DateTime value.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Timestamp indicating when the setting record was last updated, represented as a DateTime value.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}
}