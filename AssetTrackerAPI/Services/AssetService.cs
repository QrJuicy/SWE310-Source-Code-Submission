using AssetTrackerAPI.Models.Domain;
using AssetTrackerAPI.Repositories.Interfaces;
using AssetTrackerAPI.Services.Interfaces;

namespace AssetTrackerAPI.Services;

public class AssetService : IAssetService
{
    private readonly IAssetRepository _repository;

    public AssetService(
        IAssetRepository repository)
    {
        _repository = repository;
    }

    public async Task<Asset> CreateAsync(
        Asset asset)
    {
        return await _repository.CreateAsync(asset);
    }

    public async Task<Asset?> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<List<Asset>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Asset?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Asset?> UpdateAsync(
        int id,
        Asset asset)
    {
        return await _repository.UpdateAsync(id, asset);
    }
}