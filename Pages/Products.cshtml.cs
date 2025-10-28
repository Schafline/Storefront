using Microsoft.AspNetCore.Mvc.RazorPages;
using storefront.Models;

public class ProductsModel : PageModel
{
    public List<Product> Products { get; set; } = new();
    public void OnGet()
    {
        Products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "T-shirt",
                Description = "Soft cotton T-shirt",
                Price = 19.99m,
                ImageUrl = "/images/tshirt.jpg"
            },
            new Product
            {
                Id = 2,
                Name = "Sneakers",
                Description = "Comfortable running shoes",
                Price = 49.99m,
                ImageUrl = "/images/sneakers.jpg"
            }
        };
    }
}
