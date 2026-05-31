namespace AssetTrackerAPI.Models.Domain;

using System.ComponentModel.DataAnnotations;

public class AssetCategory
{
    [Key]
    public int CategoryId { get; set; } // Primary Key

    public string Name { get; set; } = string.Empty; // Not Null

    public string? Description { get; set; }

    public ICollection<Asset> Assets { get; set; }
        = new List<Asset>();
}