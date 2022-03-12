using CmsShoppingCart.Infrastructure;
using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {

        private readonly CmsShoppingCartContext _context;

        public CategoriesController(CmsShoppingCartContext context)
        {
            _context = context;

        }

        //GET /admin/categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categroies.OrderBy(x => x.Sorting).ToListAsync());
        }


        //GET /admin/categories/create
        public IActionResult Create() => View();


        //POST  /admin/categories/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = category.Name.ToLower().Replace(" ", "-");
                category.Sorting = 100;

                var slug = await _context.Categroies.FirstOrDefaultAsync(x => x.Slug == category.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The category already exists.");
                    return View(category);
                }

                _context.Add(category);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The category has been added!";

                return RedirectToAction("Index");
            }

            return View(category);
        }

        //GET /admin/categories/edit/id
        public async Task<IActionResult> Edit(int id)
        {
            Category category = await _context.Categroies.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }


        //POST  /admin/categories/edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = category.Name.ToLower().Replace(" ", "-");

                var slug = await _context.Categroies.Where(u => u.Id != id).FirstOrDefaultAsync(x => x.Slug == category.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The category already exists.");
                    return View(category);
                }

                _context.Update(category);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The category has been edited!";

                return RedirectToAction("Edit", new { id });
            }

            return View(category);
        }


        //GET /admin/categories/delete/id
        public async Task<IActionResult> Delete(int id)
        {
            Category category = await _context.Categroies.FindAsync(id);
            if (category == null)
            {
                TempData["Error"] = "The cateogory does not exist!";
            }
            else
            {
                _context.Categroies.Remove(category);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The category has been removed!";
            }

            return RedirectToAction("Index");
        }


        //POST  /admin/categories/reorder
        [HttpPost]
        public async Task<IActionResult> Reorder(int[] id)
        {
            int count = 1;

            foreach (var categoryId in id)
            {
                Category category = await _context.Categroies.FindAsync(categoryId);
                category.Sorting = count;
                _context.Update(category);
                await _context.SaveChangesAsync();
                count++;
            }

            return Ok();
        }
    }
}
