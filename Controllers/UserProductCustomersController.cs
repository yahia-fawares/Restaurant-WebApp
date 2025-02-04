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
    public class UserProductCustomersController : Controller
    {
        private readonly ModelContext _context;

        public UserProductCustomersController(ModelContext context)
        {
            _context = context;
        }

        // GET: UserProductCustomers
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.UserProductCustomers.Include(u => u.Customer).Include(u => u.Product);
            return View(await modelContext.ToListAsync());
        }

        // GET: UserProductCustomers/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.UserProductCustomers == null)
            {
                return NotFound();
            }

            var userProductCustomer = await _context.UserProductCustomers
                .Include(u => u.Customer)
                .Include(u => u.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userProductCustomer == null)
            {
                return NotFound();
            }

            return View(userProductCustomer);
        }

        // GET: UserProductCustomers/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.UserCustomers, "Id", "Fname");
            ViewData["ProductId"] = new SelectList(_context.UserProducts, "Id", "Name");
            return View();
        }

        // POST: UserProductCustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,CustomerId,Quantity,DateFrom,DateTo")] UserProductCustomer userProductCustomer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userProductCustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.UserCustomers, "Id", "Id", userProductCustomer.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.UserProducts, "Id", "Id", userProductCustomer.ProductId);
            return View(userProductCustomer);
        }

        // GET: UserProductCustomers/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.UserProductCustomers == null)
            {
                return NotFound();
            }

            var userProductCustomer = await _context.UserProductCustomers.FindAsync(id);
            if (userProductCustomer == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.UserCustomers, "Id", "Fname", userProductCustomer.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.UserProducts, "Id", "Name", userProductCustomer.ProductId);
            return View(userProductCustomer);
        }

        // POST: UserProductCustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,ProductId,CustomerId,Quantity,DateFrom,DateTo")] UserProductCustomer userProductCustomer)
        {
            if (id != userProductCustomer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userProductCustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserProductCustomerExists(userProductCustomer.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.UserCustomers, "Id", "Id", userProductCustomer.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.UserProducts, "Id", "Id", userProductCustomer.ProductId);
            return View(userProductCustomer);
        }

        // GET: UserProductCustomers/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.UserProductCustomers == null)
            {
                return NotFound();
            }

            var userProductCustomer = await _context.UserProductCustomers
                .Include(u => u.Customer)
                .Include(u => u.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userProductCustomer == null)
            {
                return NotFound();
            }

            return View(userProductCustomer);
        }

        // POST: UserProductCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.UserProductCustomers == null)
            {
                return Problem("Entity set 'ModelContext.UserProductCustomers'  is null.");
            }
            var userProductCustomer = await _context.UserProductCustomers.FindAsync(id);
            if (userProductCustomer != null)
            {
                _context.UserProductCustomers.Remove(userProductCustomer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserProductCustomerExists(decimal id)
        {
          return (_context.UserProductCustomers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
