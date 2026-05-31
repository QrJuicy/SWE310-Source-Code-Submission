using Microsoft.AspNetCore.Mvc;
using AssetTrackerAPI.Models.Domain;
using AssetTrackerAPI.Models.DTO;
using AssetTrackerAPI.Services.Interfaces;

namespace AssetTrackerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AssetController : ControllerBase
{
    private readonly IAssetService _service;

    public AssetController(
        IAssetService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var assets = await _service.GetAllAsync();

        var assetDTOs = assets.Select(asset =>
            new AssetDTO
            {
                AssetId = asset.AssetId,
                CategoryId = asset.CategoryId,
                Name = asset.Name,
                Contributor = asset.Contributor,
                ProjectName = asset.ProjectName ?? string.Empty,
                CreatedDate = asset.CreatedDate
            });

        return Ok(assetDTOs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var asset = await _service.GetByIdAsync(id);

        if (asset == null)
        {
            return NotFound();
        }

        var assetDTO = new AssetDTO
        {
            AssetId = asset.AssetId,
            CategoryId = asset.CategoryId,
            Name = asset.Name,
            Contributor = asset.Contributor,
            ProjectName = asset.ProjectName ?? string.Empty,
            CreatedDate = asset.CreatedDate
        };

        return Ok(assetDTO);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateAssetRequestDTO request)
    {
        var asset = new Asset
        {
            CategoryId = request.CategoryId,
            Name = request.Name,
            Contributor = request.Contributor,
            ProjectName = request.ProjectName,
            CreatedDate = DateTime.Now
        };

        asset = await _service.CreateAsync(asset);

        var assetDTO = new AssetDTO
        {
            AssetId = asset.AssetId,
            CategoryId = asset.CategoryId,
            Name = asset.Name,
            Contributor = asset.Contributor,
            ProjectName = asset.ProjectName ?? string.Empty,
            CreatedDate = asset.CreatedDate
        };

        return CreatedAtAction(
            nameof(GetById),
            new { id = assetDTO.AssetId },
            assetDTO);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateAssetRequestDTO request)
    {
        var asset = new Asset
        {
            CategoryId = request.CategoryId,
            Name = request.Name,
            Contributor = request.Contributor,
            ProjectName = request.ProjectName
        };

        var updatedAsset =
            await _service.UpdateAsync(id, asset);

        if (updatedAsset == null)
        {
            return NotFound();
        }

        var assetDTO = new AssetDTO
        {
            AssetId = updatedAsset.AssetId,
            CategoryId = updatedAsset.CategoryId,
            Name = updatedAsset.Name,
            Contributor = updatedAsset.Contributor,
            ProjectName = updatedAsset.ProjectName ?? string.Empty,
            CreatedDate = updatedAsset.CreatedDate
        };

        return Ok(assetDTO);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedAsset =
            await _service.DeleteAsync(id);

        if (deletedAsset == null)
        {
            return NotFound();
        }

        var assetDTO = new AssetDTO
        {
            AssetId = deletedAsset.AssetId,
            CategoryId = deletedAsset.CategoryId,
            Name = deletedAsset.Name,
            Contributor = deletedAsset.Contributor,
            ProjectName = deletedAsset.ProjectName ?? string.Empty,
            CreatedDate = deletedAsset.CreatedDate
        };

        return Ok(assetDTO);
    }
}