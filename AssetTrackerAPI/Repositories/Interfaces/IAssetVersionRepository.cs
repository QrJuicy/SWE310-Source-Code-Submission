using AssetTrackerAPI.Models.Domain;

namespace AssetTrackerAPI.Repositories.Interfaces;

public interface IAssetVersionRepository
{
    Task<List<AssetVersion>> GetAllAsync();

    Task<AssetVersion?> GetByIdAsync(int id);

    Task<AssetVersion> CreateAsync(AssetVersion assetVersion);

    Task<AssetVersion?> UpdateAsync(
        int id,
        AssetVersion assetVersion);

    Task<AssetVersion?> DeleteAsync(int id);
}