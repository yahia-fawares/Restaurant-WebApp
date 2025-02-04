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
    public class UserCustomersController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public UserCustomersController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        // GET: UserCustomers
        public async Task<IActionResult> Index()
        {
              return _context.UserCustomers != null ? 
                          View(await _context.UserCustomers.ToListAsync()) :
                          Problem("Entity set 'ModelContext.UserCustomers'  is null.");
        }

        // GET: UserCustomers/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.UserCustomers == null)
            {
                return NotFound();
            }

            var userCustomer = await _context.UserCustomers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userCustomer == null)
            {
                return NotFound();
            }

            return View(userCustomer);
        }

        // GET: UserCustomers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserCustomers/Create\

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fname,Lname,ImgFile")] UserCustomer userCustomer)
        {
            //To upload Img 
           
            if (ModelState.IsValid)
            {
                if (userCustomer.ImgFile != null)
                {
                    string wwwRootPath = _webHostEnviroment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + userCustomer.ImgFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/Imges/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await userCustomer.ImgFile.CopyToAsync(fileStream);
                    }
                    userCustomer.ImagePath = fileName;
                }
                _context.Add(userCustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userCustomer);
        }

        // GET: UserCustomers/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.UserCustomers == null)
            {
                return NotFound();
            }

            var userCustomer = await _context.UserCustomers.FindAsync(id);
            if (userCustomer == null)
            {
                return NotFound();
            }
            return View(userCustomer);
        }

        // POST: UserCustomers/Edit/5

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Fname,Lname,ImgFile")] UserCustomer userCustomer)
        {
            
            if (id != userCustomer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (userCustomer.ImgFile != null)
                    {
                        string wwwRootPath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + userCustomer.ImgFile.FileName;
                        string path = Path.Combine(wwwRootPath + "/Imges/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await userCustomer.ImgFile.CopyToAsync(fileStream);
                        }
                        userCustomer.ImagePath = fileName;
                    }
                    _context.Update(userCustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserCustomerExists(userCustomer.Id))
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
            return View(userCustomer);
        }

        // GET: UserCustomers/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {

            if (id == null || _context.UserCustomers == null)
            {
                return NotFound();
            }

            var userCustomer = await _context.UserCustomers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userCustomer == null)
            {
                return NotFound();
            }

            return View(userCustomer);
        }

        // POST: UserCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.UserCustomers == null)
            {
                return Problem("Entity set 'ModelContext.UserCustomers'  is null.");
            }
            var userCustomer = await _context.UserCustomers.FindAsync(id);
            if (userCustomer != null)
            {
                _context.UserCustomers.Remove(userCustomer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserCustomerExists(decimal id)
        {
          return (_context.UserCustomers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
