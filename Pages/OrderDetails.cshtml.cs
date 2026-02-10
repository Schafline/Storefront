using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Storefront.Data;
using Storefront.Models;

public class OrderDetailsModel : PageModel
{
    private readonly ShopContext _context;

    public OrderDetailsModel(ShopContext context)
    {
        _context = context;
    }

    public Order Order { get; set; }

    public IActionResult OnGet(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return RedirectToPage("OrderLookup");
        }

        var normalisedCode = code.Trim().ToUpperInvariant();

        Order = _context.Orders
            .Include(o => o.Items)
            .Include(o => o.ShippingInfo)
            .FirstOrDefault(o => o.VerificationCode == normalisedCode);

        if (Order == null)
        {
            return RedirectToPage("OrderLookup", new { notfound = true });
        }

        return Page();
    }
}