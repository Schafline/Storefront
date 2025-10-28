using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using storefront.Models;

public class ProductDetailModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    public Product? Product { get; set; }

    private List<Product> GetProducts()
    {
        return new List<Product>
        {
            new Product {   Id = 1, Name = "T-shirt", Description = "Soft cotton T-shirt",
                            Price = 19.99m, ImageUrl = "/images/tshirt.jpg" },
            new Product {   Id = 2, Name = "Sneakers", Description = "Comfortable running shoes",
                            Price = 49.99m, ImageUrl = "/images/sneakers.jpg" }
        };
    }

    public void OnGet()
    {
        var product = GetProducts().FirstOrDefault(p => p.Id == Id);
        if (product != null)
        {
            Product = product;
        }

    }
public IActionResult OnPost()
{
    var product = GetProducts().FirstOrDefault(p => p.Id == Id);
    if (product == null)
    {
        return NotFound();
    }

    var basketJson = TempData["Basket"] as string ?? "[]";
    var basketItems = System.Text.Json.JsonSerializer.Deserialize<List<Product>>(basketJson);
    if (basketItems != null)
    {
        basketItems.Add(product);
    }
    TempData["Basket"] = System.Text.Json.JsonSerializer.Serialize(basketItems);

    return RedirectToPage("/Basket");
}



}
