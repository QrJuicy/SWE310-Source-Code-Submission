namespace AssetTrackerAPI.Models.DTO;

public class AssetVersionDTO
{
    public int VersionId { get; set; }

    public int AssetId { get; set; }

    public string VersionNumber { get; set; } = string.Empty;

    public DateTime ReleaseDate { get; set; }

    public string? Notes { get; set; }
}