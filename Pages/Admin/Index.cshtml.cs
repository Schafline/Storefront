using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using storefront.Data;
using storefront.Models;

namespace storefront.Pages_Admin
{
    public class IndexModel : PageModel
    {
        private readonly storefront.Data.ShopContext _context;

        public IndexModel(storefront.Data.ShopContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Product = await _context.Products.ToListAsync();
        }
    }
}
