using System.Text.Json;
using RealEstate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using RealEstate.Infrastructure;

namespace RealEstate.Api.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(RealEstateContext context)
        {
            if (!context.Properties.Any())
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "SeedData", "properties.json");

                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"Seed file not found: {filePath}");

                var jsonData = await File.ReadAllTextAsync(filePath);
                var properties = JsonSerializer.Deserialize<List<Property>>(jsonData);

                if (properties != null && properties.Any())
                {
                    context.Properties.AddRange(properties);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
