using Microsoft.AspNetCore.Mvc;
using AssetTrackerAPI.Models.Domain;
using AssetTrackerAPI.Models.DTO;
using AssetTrackerAPI.Services.Interfaces;

namespace AssetTrackerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AssetVersionController : ControllerBase
{
    private readonly IAssetVersionService _service;

    public AssetVersionController(
        IAssetVersionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var versions = await _service.GetAllAsync();

        var versionDTOs = versions.Select(version =>
            new AssetVersionDTO
            {
                VersionId = version.VersionId,
                AssetId = version.AssetId,
                VersionNumber = version.VersionNumber,
                ReleaseDate = version.ReleaseDate,
                Notes = version.Notes
            });

        return Ok(versionDTOs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var version = await _service.GetByIdAsync(id);

        if (version == null)
        {
            return NotFound();
        }

        var versionDTO = new AssetVersionDTO
        {
            VersionId = version.VersionId,
            AssetId = version.AssetId,
            VersionNumber = version.VersionNumber,
            ReleaseDate = version.ReleaseDate,
            Notes = version.Notes
        };

        return Ok(versionDTO);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateAssetVersionRequestDTO request)
    {
        var version = new AssetVersion
        {
            AssetId = request.AssetId,
            VersionNumber = request.VersionNumber,
            ReleaseDate = request.ReleaseDate,
            Notes = request.Notes
        };

        version = await _service.CreateAsync(version);

        var versionDTO = new AssetVersionDTO
        {
            VersionId = version.VersionId,
            AssetId = version.AssetId,
            VersionNumber = version.VersionNumber,
            ReleaseDate = version.ReleaseDate,
            Notes = version.Notes
        };

        return CreatedAtAction(
            nameof(GetById),
            new { id = versionDTO.VersionId },
            versionDTO);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateAssetVersionRequestDTO request)
    {
        var version = new AssetVersion
        {
            AssetId = request.AssetId,
            VersionNumber = request.VersionNumber,
            ReleaseDate = request.ReleaseDate,
            Notes = request.Notes
        };

        var updatedVersion =
            await _service.UpdateAsync(id, version);

        if (updatedVersion == null)
        {
            return NotFound();
        }

        var versionDTO = new AssetVersionDTO
        {
            VersionId = updatedVersion.VersionId,
            AssetId = updatedVersion.AssetId,
            VersionNumber = updatedVersion.VersionNumber,
            ReleaseDate = updatedVersion.ReleaseDate,
            Notes = updatedVersion.Notes
        };

        return Ok(versionDTO);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedVersion =
            await _service.DeleteAsync(id);

        if (deletedVersion == null)
        {
            return NotFound();
        }

        var versionDTO = new AssetVersionDTO
        {
            VersionId = deletedVersion.VersionId,
            AssetId = deletedVersion.AssetId,
            VersionNumber = deletedVersion.VersionNumber,
            ReleaseDate = deletedVersion.ReleaseDate,
            Notes = deletedVersion.Notes
        };

        return Ok(versionDTO);
    }
}