using Microsoft.EntityFrameworkCore;
using AssetTrackerAPI.Data;
using AssetTrackerAPI.Models.Domain;
using AssetTrackerAPI.Repositories.Interfaces;

namespace AssetTrackerAPI.Repositories;

public class AssetRepository : IAssetRepository
{
    private readonly AssetTrackerDbContext _context;

    public AssetRepository(
        AssetTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<Asset> CreateAsync(
        Asset asset)
    {
        await _context.Asset.AddAsync(asset);

        await _context.SaveChangesAsync();

        return asset;
    }

    public async Task<Asset?> DeleteAsync(int id)
    {
        var existingAsset = await _context.Asset
            .FirstOrDefaultAsync(x => x.AssetId == id);

        if (existingAsset == null)
        {
            return null;
        }

        _context.Asset.Remove(existingAsset);

        await _context.SaveChangesAsync();

        return existingAsset;
    }

    public async Task<List<Asset>> GetAllAsync()
    {
        return await _context.Asset.ToListAsync();
    }

    public async Task<Asset?> GetByIdAsync(int id)
    {
        return await _context.Asset
            .FirstOrDefaultAsync(x => x.AssetId == id);
    }

    public async Task<Asset?> UpdateAsync(
        int id,
        Asset asset)
    {
        var existingAsset = await _context.Asset
            .FirstOrDefaultAsync(x => x.AssetId == id);

        if (existingAsset == null)
        {
            return null;
        }

        existingAsset.CategoryId = asset.CategoryId;
        existingAsset.Name = asset.Name;
        existingAsset.Contributor = asset.Contributor;
        existingAsset.ProjectName = asset.ProjectName;

        await _context.SaveChangesAsync();

        return existingAsset;
    }
}