using AssetTrackerAPI.Models.Domain;
using AssetTrackerAPI.Repositories.Interfaces;
using AssetTrackerAPI.Services.Interfaces;

namespace AssetTrackerAPI.Services;

public class AssetVersionService : IAssetVersionService
{
    private readonly IAssetVersionRepository _repository;

    public AssetVersionService(
        IAssetVersionRepository repository)
    {
        _repository = repository;
    }

    public async Task<AssetVersion> CreateAsync(
        AssetVersion assetVersion)
    {
        return await _repository.CreateAsync(assetVersion);
    }

    public async Task<AssetVersion?> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<List<AssetVersion>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<AssetVersion?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<AssetVersion?> UpdateAsync(
        int id,
        AssetVersion assetVersion)
    {
        return await _repository.UpdateAsync(id, assetVersion);
    }
}