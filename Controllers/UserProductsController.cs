using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyRestaurant.Models;

namespace MyRestaurant.Controllers
{
    public class UserProductsController : Controller
    {
        private readonly ModelContext _context;

        public UserProductsController(ModelContext context)
        {
            _context = context;
        }

        // GET: UserProducts
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.UserProducts.Include(u => u.Category);
            return View(await modelContext.ToListAsync());
        }

        // GET: UserProducts/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.UserProducts == null)
            {
                return NotFound();
            }

            var userProduct = await _context.UserProducts
                .Include(u => u.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userProduct == null)
            {
                return NotFound();
            }

            return View(userProduct);
        }

        // GET: UserProducts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.UserCategories, "Id", "CategoryName");
            return View();
        }

        // POST: UserProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Sale,Price,CategoryId")] UserProduct userProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.UserCategories, "Id", "Id", userProduct.CategoryId);
            return View(userProduct);
        }

        // GET: UserProducts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.UserProducts == null)
            {
                return NotFound();
            }

            var userProduct = await _context.UserProducts.FindAsync(id);
            if (userProduct == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.UserCategories, "Id", "CategoryName", userProduct.CategoryId);
            return View(userProduct);
        }

        // POST: UserProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name,Sale,Price,CategoryId")] UserProduct userProduct)
        {
            if (id != userProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserProductExists(userProduct.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.UserCategories, "Id", "Id", userProduct.CategoryId);
            return View(userProduct);
        }

        // GET: UserProducts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.UserProducts == null)
            {
                return NotFound();
            }

            var userProduct = await _context.UserProducts
                .Include(u => u.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userProduct == null)
            {
                return NotFound();
            }

            return View(userProduct);
        }

        // POST: UserProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.UserProducts == null)
            {
                return Problem("Entity set 'ModelContext.UserProducts'  is null.");
            }
            var userProduct = await _context.UserProducts.FindAsync(id);
            if (userProduct != null)
            {
                _context.UserProducts.Remove(userProduct);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserProductExists(decimal id)
        {
          return (_context.UserProducts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
