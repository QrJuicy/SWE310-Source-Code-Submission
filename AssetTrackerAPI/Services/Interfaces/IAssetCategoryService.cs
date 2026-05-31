using AssetTrackerAPI.Models.Domain;

namespace AssetTrackerAPI.Services.Interfaces;

public interface IAssetCategoryService
{
    Task<List<AssetCategory>> GetAllAsync();

    Task<AssetCategory?> GetByIdAsync(int id);

    Task<AssetCategory> CreateAsync(AssetCategory assetCategory);

    Task<AssetCategory?> UpdateAsync(
        int id,
        AssetCategory assetCategory);

    Task<AssetCategory?> DeleteAsync(int id);
}