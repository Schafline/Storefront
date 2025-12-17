using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Storefront.Data;
using Storefront.Models;

namespace Storefront.Pages_Admin
{
    public class DetailsModel : PageModel
    {
        private readonly Storefront.Data.ShopContext _context;

        public DetailsModel(Storefront.Data.ShopContext context)
        {
            _context = context;
        }

        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);

            if (product is not null)
            {
                Product = product;

                return Page();
            }

            return NotFound();
        }
    }
}
