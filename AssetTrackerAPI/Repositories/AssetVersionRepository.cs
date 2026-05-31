using Microsoft.EntityFrameworkCore;
using AssetTrackerAPI.Data;
using AssetTrackerAPI.Models.Domain;
using AssetTrackerAPI.Repositories.Interfaces;

namespace AssetTrackerAPI.Repositories;

public class AssetVersionRepository : IAssetVersionRepository
{
    private readonly AssetTrackerDbContext _context;

    public AssetVersionRepository(
        AssetTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<AssetVersion> CreateAsync(
        AssetVersion assetVersion)
    {
        await _context.AssetVersion.AddAsync(assetVersion);

        await _context.SaveChangesAsync();

        return assetVersion;
    }

    public async Task<AssetVersion?> DeleteAsync(int id)
    {
        var existingVersion = await _context.AssetVersion
            .FirstOrDefaultAsync(x => x.VersionId == id);

        if (existingVersion == null)
        {
            return null;
        }

        _context.AssetVersion.Remove(existingVersion);

        await _context.SaveChangesAsync();

        return existingVersion;
    }

    public async Task<List<AssetVersion>> GetAllAsync()
    {
        return await _context.AssetVersion.ToListAsync();
    }

    public async Task<AssetVersion?> GetByIdAsync(int id)
    {
        return await _context.AssetVersion
            .FirstOrDefaultAsync(x => x.VersionId == id);
    }

    public async Task<AssetVersion?> UpdateAsync(
        int id,
        AssetVersion assetVersion)
    {
        var existingVersion = await _context.AssetVersion
            .FirstOrDefaultAsync(x => x.VersionId == id);

        if (existingVersion == null)
        {
            return null;
        }

        existingVersion.AssetId = assetVersion.AssetId;
        existingVersion.VersionNumber = assetVersion.VersionNumber;
        existingVersion.ReleaseDate = assetVersion.ReleaseDate;
        existingVersion.Notes = assetVersion.Notes;

        await _context.SaveChangesAsync();

        return existingVersion;
    }
}