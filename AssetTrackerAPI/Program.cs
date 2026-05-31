using Microsoft.EntityFrameworkCore;
using AssetTrackerAPI.Data;

using AssetTrackerAPI.Repositories;
using AssetTrackerAPI.Repositories.Interfaces;
using AssetTrackerAPI.Services;
using AssetTrackerAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AssetTrackerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AssetTrackerDbConnectionString")));


// Repositories
builder.Services.AddScoped<
    IAssetCategoryRepository,
    AssetCategoryRepository>();

builder.Services.AddScoped<
    IAssetRepository,
    AssetRepository>();

builder.Services.AddScoped<
    IAssetVersionRepository,
    AssetVersionRepository>();


// Services
builder.Services.AddScoped<
    IAssetCategoryService,
    AssetCategoryService>();

builder.Services.AddScoped<
    IAssetService,
    AssetService>();

builder.Services.AddScoped<
    IAssetVersionService,
    AssetVersionService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors("AllowReact");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
