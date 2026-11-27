using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Storefront.Data;
using Storefront.Models;
using Storefront.Services;

namespace Storefront.Pages;

public class ProductDetailModel : PageModel
{
    private readonly ShopContext _context;
    private readonly BasketService _basketService;

    public ProductDetailModel(
        ShopContext context,
        BasketService basketService)
    {
        _context = context;
        _basketService = basketService;
    }

    [BindProperty(SupportsGet = true)]
    public string Slug { get; set; } = string.Empty;

    public Product? Product { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        if (string.IsNullOrWhiteSpace(Slug))
        {
            return NotFound();
        }

        Product = await _context.Products
            .FirstOrDefaultAsync(p => p.Slug == Slug);

        if (Product == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(Slug))
        {
            return NotFound();
        }

        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Slug == Slug);

        if (product == null)
        {
            return NotFound();
        }

        _basketService.AddToBasket(product);
        return RedirectToPage("/Basket");
    }
}