using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Storefront.Data;
using Storefront.Models;

public class ConfirmationModel : PageModel
{
    private readonly ShopContext _context;

    public ConfirmationModel(ShopContext context)
    {
        _context = context;
    }

    public Order Order { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Order = await _context.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (Order == null)
            return NotFound();

        return Page();
    }
}