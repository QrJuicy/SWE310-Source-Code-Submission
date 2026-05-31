using Microsoft.AspNetCore.Mvc;
using AssetTrackerAPI.Models.Domain;
using AssetTrackerAPI.Models.DTO;
using AssetTrackerAPI.Services.Interfaces;

namespace AssetTrackerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AssetCategoryController : ControllerBase
{
    private readonly IAssetCategoryService _service;

    public AssetCategoryController(
        IAssetCategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _service.GetAllAsync();

        var categoryDTOs = categories.Select(category =>
            new AssetCategoryDTO
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description
            });

        return Ok(categoryDTOs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _service.GetByIdAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        var categoryDTO = new AssetCategoryDTO
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
            Description = category.Description
        };

        return Ok(categoryDTO);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateAssetCategoryRequestDTO request)
    {
        var category = new AssetCategory
        {
            Name = request.Name,
            Description = request.Description
        };

        category = await _service.CreateAsync(category);

        var categoryDTO = new AssetCategoryDTO
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
            Description = category.Description
        };

        return CreatedAtAction(
            nameof(GetById),
            new { id = categoryDTO.CategoryId },
            categoryDTO);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateAssetCategoryRequestDTO request)
    {
        var category = new AssetCategory
        {
            Name = request.Name,
            Description = request.Description
        };

        var updatedCategory =
            await _service.UpdateAsync(id, category);

        if (updatedCategory == null)
        {
            return NotFound();
        }

        var categoryDTO = new AssetCategoryDTO
        {
            CategoryId = updatedCategory.CategoryId,
            Name = updatedCategory.Name,
            Description = updatedCategory.Description
        };

        return Ok(categoryDTO);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedCategory =
            await _service.DeleteAsync(id);

        if (deletedCategory == null)
        {
            return NotFound();
        }

        var categoryDTO = new AssetCategoryDTO
        {
            CategoryId = deletedCategory.CategoryId,
            Name = deletedCategory.Name,
            Description = deletedCategory.Description
        };

        return Ok(categoryDTO);
    }
}