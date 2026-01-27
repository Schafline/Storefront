using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Storefront.Data;
using Storefront.Models;
using Storefront.Services;

// This handler is called by JavaScript after PayPal approves the payment.
// Because it's not submitted from an HTML form, it doesn't include an antiforgery token.
// We disable antiforgery here so the JSON request isn't rejected by the framework.
[IgnoreAntiforgeryToken]
public class BasketModel : PageModel
{
    private readonly IConfiguration _config;
    private readonly BasketService _basketService;
    private readonly ShopContext _context;
    private readonly EmailService _emailService;

    public BasketModel(IConfiguration config, BasketService basketService, EmailService emailService, ShopContext context)
    {
        _config = config;
        _basketService = basketService;
        _emailService = emailService;
        _context = context;
    }

    public string PayPalClientId { get; private set; }
    public List<Product> BasketItems { get; set; } = new();
    [BindProperty]
    public int Id { get; set; }

    public decimal TotalPrice { get; set; }

    public IActionResult OnPostRemove()
    {
        var basket = _basketService.GetBasket();
        var itemToRemove = basket.FirstOrDefault(p => p.Id == Id);

        if (itemToRemove != null)
        {
            basket.Remove(itemToRemove);
            _basketService.SaveBasket(basket);
        }

        return RedirectToPage();
    }


    public void OnGet()
    {
        BasketItems = _basketService.GetBasket();
        TotalPrice = BasketItems.Sum(p => p.Price);
        PayPalClientId = _config["PayPal:SandboxClientId"];
    }

    public async Task<IActionResult> OnPostCompleteOrderAsync([FromBody] PayPalOrderInfo info)
    {
        // Load the basket from session
        var cart = _basketService.GetBasket();

        var order = new Order
        {
            OrderDate = DateTime.UtcNow
        };

        foreach (var product in cart)
        {
            order.Items.Add(new OrderItem
            {
                ProductId = product.Id,
                Quantity = 1,
                ProductName = product.Name,
                Price = product.Price
            });
        }

        order.Total = order.Items.Sum(i => i.Price);
        order.OrderStatus = "Paid";
        order.VerificationCode = Random.Shared.Next(100000, 999999).ToString();
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        await _emailService.SendEmailAsync(
            "Order Confirmation",
            $"Thank you for your order! Your verification code is: {order.VerificationCode}"
        );

        // Clear basket
        _basketService.Clear();

        return new JsonResult(new { orderId = order.Id });
    }

    public class PayPalOrderInfo
    {
        public string OrderId { get; set; }
        public string PayerId { get; set; }
    }
}