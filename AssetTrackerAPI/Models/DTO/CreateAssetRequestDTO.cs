using System.ComponentModel.DataAnnotations;

namespace AssetTrackerAPI.Models.DTO;

public class CreateAssetRequestDTO
{
    [Required]
    public int CategoryId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Contributor { get; set; } = string.Empty;

    [StringLength(100)]
    public string ProjectName { get; set; } = string.Empty;
}