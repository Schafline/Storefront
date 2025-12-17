using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Storefront.Models;
using Microsoft.EntityFrameworkCore;
using Storefront.Data;

public class ProductDetailModel : PageModel
{
    private readonly ShopContext _context;

    public ProductDetailModel(ShopContext context)
    {
        _context = context;
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

        var basketJson = TempData["Basket"] as string ?? "[]";
        var basketItems = System.Text.Json.JsonSerializer.Deserialize<List<Product>>(basketJson) ?? new List<Product>();

        if (basketItems != null)
        {
            basketItems.Add(product);
        }

        TempData["Basket"] = System.Text.Json.JsonSerializer.Serialize(basketItems);

        return RedirectToPage("/Basket");
    }




}
