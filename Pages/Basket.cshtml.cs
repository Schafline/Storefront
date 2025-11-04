
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.RazorPages;
using storefront.Models;

public class BasketModel : PageModel
{
    public List<Product> BasketItems { get; set; } = new();
    [BindProperty]
    public int Id { get; set; }

    public decimal TotalPrice { get; set; }

    public IActionResult OnPostRemove()
    {
        var basketJson = TempData["Basket"] as string ?? "[]";
        var basketItems = System.Text.Json.JsonSerializer.Deserialize<List<Product>>(basketJson) ?? new List<Product>();

        var itemToRemove = basketItems.FirstOrDefault(p => p.Id == Id);
        if (itemToRemove != null)
        {
            basketItems.Remove(itemToRemove);
            TempData["Basket"] = System.Text.Json.JsonSerializer.Serialize(basketItems);
        }

        return RedirectToPage();
    }


    public void OnGet()
    {
        var basketJson = TempData["Basket"] as string ?? "[]";
        BasketItems = System.Text.Json.JsonSerializer.Deserialize<List<Product>>(basketJson)?? new List<Product>();
        TotalPrice = BasketItems.Sum(p => p.Price);
        // Keep basket alive for future requests
        TempData.Keep("Basket");

    }
}