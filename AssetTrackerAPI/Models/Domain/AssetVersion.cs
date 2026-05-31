namespace AssetTrackerAPI.Models.Domain;

using System.ComponentModel.DataAnnotations;

public class AssetVersion
{
    [Key]
    public int VersionId { get; set; } // Primary Key

    public int AssetId { get; set; } // Foreign Key

    public string VersionNumber { get; set; } = string.Empty; // Not Null

    public DateTime ReleaseDate { get; set; } // Not Null

    public string? Notes { get; set; }

    public Asset Asset { get; set; } = null!;
}