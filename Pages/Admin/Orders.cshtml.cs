using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Storefront.Data;
using Storefront.Models;

namespace Storefront.Pages.Admin
{
    public class OrdersModel : PageModel
    {
        private readonly ShopContext _context;

        public OrdersModel(ShopContext context)
        {
            _context = context;
        }

        public List<Order> Orders { get; set; } = new();
        public string? Query { get; set; }

        public async Task<IActionResult> OnPostAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
            {
                return NotFound();
            }

            // Extract the posted status value
            var newStatus = Request.Form["Orders[0].OrderStatus"];

            // Update the order
            order.OrderStatus = newStatus;

            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task OnGetAsync(string? q)
        {
            Query = q;

            var orders = _context.Orders
                .Include(o => o.ShippingInfo)
                .Include(o => o.Items)
                    .ThenInclude(oi => oi.Product)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(q))
            {
                orders = orders.Where(o =>
                    o.Id.ToString().Contains(q) ||
                    o.ShippingInfo.FullName.Contains(q) ||
                    o.ShippingInfo.Email.Contains(q) ||
                    o.ShippingInfo.Postcode.Contains(q)
                );
            }

            Orders = await orders
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }
    }
}