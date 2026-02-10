using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Storefront.Constants;
using Storefront.Data;
using Storefront.Models;

public class ShippingDetailsModel : PageModel
{
    private readonly ShopContext _context;

    public ShippingDetailsModel(ShopContext context)
    {
        _context = context;
    }

    [BindProperty]
    public ShippingInfo ShippingInfo { get; set; }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.ShippingInfos.Add(ShippingInfo);
        await _context.SaveChangesAsync();
        HttpContext.Session.SetInt32(SessionKeys.ShippingInfoIdKey, ShippingInfo.Id);
        return RedirectToPage("Basket");
    }
}