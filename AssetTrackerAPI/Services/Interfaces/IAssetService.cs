using AssetTrackerAPI.Models.Domain;

namespace AssetTrackerAPI.Services.Interfaces;

public interface IAssetService
{
    Task<List<Asset>> GetAllAsync();

    Task<Asset?> GetByIdAsync(int id);

    Task<Asset> CreateAsync(Asset asset);

    Task<Asset?> UpdateAsync(
        int id,
        Asset asset);

    Task<Asset?> DeleteAsync(int id);
}