using Storefront.Constants;
using Storefront.Models;
using System.Text.Json;
namespace Storefront.Services
{
    public class BasketService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BasketService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<Product> GetBasket()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var json = session.GetString(SessionKeys.BasketKey);

            if (string.IsNullOrEmpty(json))
                return new List<Product>();

            return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
        }

        public void AddToBasket(Product product)
        {
            var basket = GetBasket();
            basket.Add(product);
            SaveBasket(basket);
        }


        public void SaveBasket(List<Product> basket)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var json = JsonSerializer.Serialize(basket);
            session.SetString(SessionKeys.BasketKey, json);
        }

        public void Clear()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            session.Remove(SessionKeys.BasketKey);
            session.Remove(SessionKeys.ShippingInfoIdKey);
        }
    }
}