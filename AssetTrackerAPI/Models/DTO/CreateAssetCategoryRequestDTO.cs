using System.ComponentModel.DataAnnotations;

namespace AssetTrackerAPI.Models.DTO;

public class CreateAssetCategoryRequestDTO
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }
}