using Microsoft.EntityFrameworkCore;
using AssetTrackerAPI.Data;
using AssetTrackerAPI.Models.Domain;
using AssetTrackerAPI.Repositories.Interfaces;

namespace AssetTrackerAPI.Repositories;

public class AssetCategoryRepository : IAssetCategoryRepository
{
    private readonly AssetTrackerDbContext _context;

    public AssetCategoryRepository(
        AssetTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<AssetCategory> CreateAsync(
        AssetCategory assetCategory)
    {
        await _context.AssetCategory.AddAsync(assetCategory);

        await _context.SaveChangesAsync();

        return assetCategory;
    }

    public async Task<AssetCategory?> DeleteAsync(int id)
    {
        var existingCategory = await _context.AssetCategory
            .FirstOrDefaultAsync(x => x.CategoryId == id);

        if (existingCategory == null)
        {
            return null;
        }

        _context.AssetCategory.Remove(existingCategory);

        await _context.SaveChangesAsync();

        return existingCategory;
    }

    public async Task<List<AssetCategory>> GetAllAsync()
    {
        return await _context.AssetCategory.ToListAsync();
    }

    public async Task<AssetCategory?> GetByIdAsync(int id)
    {
        return await _context.AssetCategory
            .FirstOrDefaultAsync(x => x.CategoryId == id);
    }

    public async Task<AssetCategory?> UpdateAsync(
        int id,
        AssetCategory assetCategory)
    {
        var existingCategory = await _context.AssetCategory
            .FirstOrDefaultAsync(x => x.CategoryId == id);

        if (existingCategory == null)
        {
            return null;
        }

        existingCategory.Name = assetCategory.Name;
        existingCategory.Description = assetCategory.Description;

        await _context.SaveChangesAsync();

        return existingCategory;
    }
}