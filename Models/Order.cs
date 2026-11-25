namespace Storefront.Models;

public class Order : IAuditable
{
  public int Id { get; set; }

  public decimal Total { get; set; }

  public string OrderStatus { get; set; }

  public List<OrderItem> Items { get; set; }
    = new();

  public string? VerificationCode
  { get; set; }

  public int? ShippingInfoId
  { get; set; }

  public ShippingInfo ShippingInfo
  { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; }

}