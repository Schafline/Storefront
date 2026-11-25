using System.Text.Json;
using System.Text.Json.Serialization;
using Storefront.Models;
using StoreFront.Dtos;

namespace Storefront.Data;

public static class SeedData
{
  public static void Initialize(ShopContext context)
  {
    if (!context.Categories.Any())
    {
      context.Categories.AddRange(
        new Category { Name = "Tops" },
        new Category { Name = "Bottoms" },
        new Category
        {
          Name = "Dresses & Jumpsuits"
        },
        new Category { Name = "Outerwear" },
        new Category
        {
          Name = "Layering & Knitwear"
        },
        new Category { Name = "Footwear" },
        new Category { Name = "Accessories" }
      );

      context.SaveChanges();
    }
    // Only seed if the Products table is empty
    if (context.Products.Any())
      return;

    var json =
      File.ReadAllText(
        "Data/products.json");

    var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() } // Automatically maps JSON strings to C# enums
    };

    var seedProducts =
      JsonSerializer.Deserialize<
        List<ProductSeedDto>>(json, options);


    if (seedProducts == null)
    {
        return;
    }

    var categories =
      context.Categories
        .ToDictionary(
          c => c.Name,
          c => c);

    Console.WriteLine($"Importing {seedProducts.Count} products..."); 

    foreach (var seedProduct in seedProducts)
    {     
      context.Products.Add(
        new Product
        {
          Name =
            seedProduct.Name,

          Description =
            seedProduct.Description,

          Price =
            seedProduct.Price,

          ImageUrl =
            seedProduct.ImageUrl,

          Audience =
            seedProduct.Audience,

          Colour =
            seedProduct.Colour,
          
          BaseColour = 
            seedProduct.BaseColour,

          Slug = 
            seedProduct.Slug,

          StockQuantity =
            seedProduct.StockQuantity,

          Size = 
            seedProduct.Size,

          Condition = 
            seedProduct.Condition,

          Category =
            categories[
              seedProduct.Category]
        });
    }

  context.SaveChanges();
  }
}