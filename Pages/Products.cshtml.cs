using Microsoft.AspNetCore.Mvc.RazorPages;
using Storefront.Models;
using Microsoft.EntityFrameworkCore;
using Storefront.Data;
using Storefront.ViewModels;
using Storefront.Extensions;

namespace Storefront.Pages;

public class ProductsModel : PageModel
{
  public List<ProductCardViewModel> Products { get; set; } = [];

  private readonly ShopContext _context;

  public ProductsModel(ShopContext context)
  {
    _context = context;
  }

public async Task OnGetAsync()
{
    Products = await _context.Products
        .AsNoTracking()
        .Select(p => new ProductCardViewModel
        {
            Id = p.Id,
            Slug = p.Slug,
            Name = p.Name,
            ImageUrl = p.ImageUrl,
            Price = p.Price,
            AudienceLabel = p.Audience.ToString(),
            CategoryName = p.Category.Name,
            ConditionLabel = p.Condition.ToDisplayLabel(),
            ConditionBadgeCss = p.Condition.ToBadgeCssClass(),
            ColourName = p.Colour,
            ColourHex = p.BaseColour.ToHexCode(),
            Size = p.Size,
            StockQuantity = p.StockQuantity
        })
        .ToListAsync();
}
}
