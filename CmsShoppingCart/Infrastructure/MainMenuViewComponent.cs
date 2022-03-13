using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Infrastructure
{

    public class MainMenuViewComponent : ViewComponent
    {
        private readonly CmsShoppingCartContext _context;

        public MainMenuViewComponent(CmsShoppingCartContext context)
        {
            _context = context;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var pages = await GetPageAsync();
            return View(pages);
        }

        private Task<List<Page>> GetPageAsync()
        {
            return _context.Pages.OrderBy(x => x.Sorting).ToListAsync();
        }
    }
}
