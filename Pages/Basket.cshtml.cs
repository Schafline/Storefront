using Microsoft.AspNetCore.Mvc.RazorPages;
using storefront.Models;

public class BasketModel : PageModel
{
    public List<BasketItem> Items { get; set; } = new();

    public void OnGet()
    {
        Items.Add(new BasketItem { Name = "T-shirt", Quantity = 2 });
        Items.Add(new BasketItem { Name = "Sneakers", Quantity = 1 });
    }
}