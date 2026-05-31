using AssetTrackerAPI.Models.Domain;

namespace AssetTrackerAPI.Repositories.Interfaces;

public interface IAssetRepository
{
    Task<List<Asset>> GetAllAsync();

    Task<Asset?> GetByIdAsync(int id);

    Task<Asset> CreateAsync(Asset asset);

    Task<Asset?> UpdateAsync(
        int id,
        Asset asset);

    Task<Asset?> DeleteAsync(int id);
}