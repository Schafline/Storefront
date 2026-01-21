using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Storefront.Models;
using Microsoft.EntityFrameworkCore;
using Storefront.Data;
using Storefront.Services;

public class ProductDetailModel : PageModel
{
    private readonly ShopContext _context;
    private readonly BasketService _basketService;

    public ProductDetailModel(ShopContext context, BasketService basketService)
    {
        _context = context;
        _basketService = basketService;
    }

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    public Product? Product { get; set; }


    public async Task<IActionResult> OnGetAsync()
    {
        if (Id == 0)
        {
            return NotFound();
        }

        Product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == Id);

        if (Product == null)
        {
            return NotFound();
        }

        return Page();
    }


    public async Task<IActionResult> OnPostAsync()
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == Id);

        if (product == null)
        {
            return NotFound();
        }

        _basketService.AddToBasket(product);

        return RedirectToPage("/Basket");
    }

}
