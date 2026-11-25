using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Storefront.Models;
using Storefront.Constants;
using Storefront.Data;
using Storefront.Services;

namespace Storefront.Pages;

[IgnoreAntiforgeryToken]
public class BasketModel : PageModel
{
  private readonly IConfiguration _config;
  private readonly BasketService _basketService;
  private readonly ShopContext _context;
  private readonly EmailService _emailService;

  public BasketModel(
    IConfiguration config,
    BasketService basketService,
    EmailService emailService,
    ShopContext context)
  {
    _config = config;
    _basketService = basketService;
    _emailService = emailService;
    _context = context;
  }


  public string PayPalClientId
  { get; private set; }

  public List<Product> BasketItems
  { get; set; } = new();

  [BindProperty]
  public int Id { get; set; }

  public decimal TotalPrice
  { get; set; }

  public bool HasShippingInfo { get; private set; }

  public IActionResult OnPostRemove()
  {
    var basket = _basketService.GetBasket();
    var itemToRemove =
      basket.FirstOrDefault(p => p.Id == Id);

    if (itemToRemove != null)
    {
      basket.Remove(itemToRemove);
      _basketService.SaveBasket(basket);
      if (!basket.Any())
      {
        HttpContext.Session.Remove(
          SessionKeys
            .ShippingInfoIdKey);
      }
    }

    return RedirectToPage();
  }

  public void OnGet()
  {
    HasShippingInfo =
     HttpContext.Session.GetInt32(
       SessionKeys.ShippingInfoIdKey) != null;
    BasketItems = _basketService.GetBasket();
    TotalPrice = BasketItems.Sum(p => p.Price);
    PayPalClientId = _config["PayPal:SandboxClientId"];
  }

  public async Task<IActionResult>
    OnPostCompleteOrderAsync(
      [FromBody] PayPalOrderInfo info)
  {
    var cart = _basketService.GetBasket();

    var shippingInfoId =
      HttpContext.Session.GetInt32(
        SessionKeys.ShippingInfoIdKey);

    var order = new Order
    {
      ShippingInfoId = shippingInfoId
    };

    foreach (var product in cart)
    {
      order.Items.Add(
        new OrderItem
        {
          ProductId = product.Id,
          Quantity = 1,
          ProductName = product.Name,
          Price = product.Price
        });
    }

    order.Total =
      order.Items.Sum(i => i.Price);
    order.OrderStatus =
      OrderStatusConstants.Paid;
    order.VerificationCode =
        Random.Shared.Next(100000, 999999).ToString();
    _context.Orders.Add(order);

    try
    {
      await _context.SaveChangesAsync();
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine(
        "Database save failed: " +
        ex.Message);
      throw;
    }
    var shippingInfo = await _context
      .ShippingInfos
      .FirstOrDefaultAsync(s =>
        s.Id == shippingInfoId);

    if (shippingInfo == null)
    {
      Console.Error.WriteLine(
        $"No shipping info found for " +
        $"ID {shippingInfoId}");
    }
    else
    {
      try
      {
        await _emailService.SendEmailAsync(
          shippingInfo.Email,
          "Order Confirmation",
          "Thank you for your order! " +
          "Your verification code is: " +
          order.VerificationCode);
      }
      catch (Exception ex)
      {
        Console.Error.WriteLine(
          "Email failed: " + ex.Message);
      }
    }

    _basketService.Clear();

    return new JsonResult(
      new { orderId = order.Id });
  }

  public class PayPalOrderInfo
  {
    public string OrderId { get; set; }
    public string PayerId { get; set; }
  }

}