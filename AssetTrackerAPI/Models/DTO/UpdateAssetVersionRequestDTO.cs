using System.ComponentModel.DataAnnotations;

namespace AssetTrackerAPI.Models.DTO;

public class UpdateAssetVersionRequestDTO
{
    [Required]
    public int AssetId { get; set; }

    [Required]
    [StringLength(20)]
    public string VersionNumber { get; set; } = string.Empty;

    [Required]
    public DateTime ReleaseDate { get; set; }

    [StringLength(500)]
    public string? Notes { get; set; }
}