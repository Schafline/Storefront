namespace Storefront.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public decimal Total { get; set; }

        public string OrderStatus { get;set; }

        public List<OrderItem> Items { get; set; } = new();
    }
}