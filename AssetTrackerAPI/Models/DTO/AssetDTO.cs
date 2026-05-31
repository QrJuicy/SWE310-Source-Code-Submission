namespace AssetTrackerAPI.Models.DTO;

public class AssetDTO
{
    public int AssetId { get; set; }

    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Contributor { get; set; } = string.Empty;

    public string ProjectName { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }
}