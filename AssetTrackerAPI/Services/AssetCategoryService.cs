using AssetTrackerAPI.Models.Domain;
using AssetTrackerAPI.Repositories.Interfaces;
using AssetTrackerAPI.Services.Interfaces;

namespace AssetTrackerAPI.Services;

public class AssetCategoryService : IAssetCategoryService
{
    private readonly IAssetCategoryRepository _repository;

    public AssetCategoryService(
        IAssetCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<AssetCategory> CreateAsync(
        AssetCategory assetCategory)
    {
        return await _repository.CreateAsync(assetCategory);
    }

    public async Task<AssetCategory?> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<List<AssetCategory>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<AssetCategory?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<AssetCategory?> UpdateAsync(
        int id,
        AssetCategory assetCategory)
    {
        return await _repository.UpdateAsync(id, assetCategory);
    }
}