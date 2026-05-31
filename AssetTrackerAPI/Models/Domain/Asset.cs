namespace AssetTrackerAPI.Models.Domain;

using System.ComponentModel.DataAnnotations;

public class Asset
{
    [Key]
    public int AssetId { get; set; } // Primary Key

    public int CategoryId { get; set; } // Foreign Key

    public AssetCategory AssetCategory { get; set; } = null!;

    public string Name { get; set; } = string.Empty; // Not Null

    public string Contributor { get; set; } = string.Empty; // Not Null

    public string? ProjectName { get; set; }

    public DateTime CreatedDate { get; set; } // Not Null

    public ICollection<AssetVersion> AssetVersions { get; set; }
        = new List<AssetVersion>();
}