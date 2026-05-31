using AssetTrackerAPI.Models.Domain;

namespace AssetTrackerAPI.Repositories.Interfaces;

public interface IAssetCategoryRepository
{
    Task<List<AssetCategory>> GetAllAsync();

    Task<AssetCategory?> GetByIdAsync(int id);

    Task<AssetCategory> CreateAsync(AssetCategory assetCategory);

    Task<AssetCategory?> UpdateAsync(
        int id,
        AssetCategory assetCategory);

    Task<AssetCategory?> DeleteAsync(int id);
}