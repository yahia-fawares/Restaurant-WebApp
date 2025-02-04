using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyRestaurant.Models;

namespace MyRestaurant.Controllers
{
    public class UserCategoriesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;


        public UserCategoriesController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }


        // GET: UserCategories
        public async Task<IActionResult> Index()
        {
              return _context.UserCategories != null ? 
                          View(await _context.UserCategories.ToListAsync()) :
                          Problem("Entity set 'ModelContext.UserCategories'  is null.");
        }

        // GET: UserCategories/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.UserCategories == null)
            {
                return NotFound();
            }

            var userCategory = await _context.UserCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userCategory == null)
            {
                return NotFound();
            }

            return View(userCategory);
        }

        // GET: UserCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserCategories/Create

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryName,ImgFile")] UserCategory userCategory)
        {
            //To upload Img 
           

            if (ModelState.IsValid)
            {
                if (userCategory.ImgFile != null)
                {
                    string wwwRootPath = _webHostEnviroment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + userCategory.ImgFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/Imges/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await userCategory.ImgFile.CopyToAsync(fileStream);
                    }
                    userCategory.ImagePath = fileName;
                }


                _context.Add(userCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userCategory);
        }

        // GET: UserCategories/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.UserCategories == null)
            {
                return NotFound();
            }

            var userCategory = await _context.UserCategories.FindAsync(id);
            if (userCategory == null)
            {
                return NotFound();
            }
            return View(userCategory);
        }

        // POST: UserCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,CategoryName,ImgFile")] UserCategory userCategory)
        {
            
            if (id != userCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (userCategory.ImgFile != null)
                    {
                        string wwwRootPath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + userCategory.ImgFile.FileName;
                        string path = Path.Combine(wwwRootPath + "/Imges/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await userCategory.ImgFile.CopyToAsync(fileStream);
                        }
                        userCategory.ImagePath = fileName;
                    }
                    _context.Update(userCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserCategoryExists(userCategory.Id))
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
            return View(userCategory);
        }

        // GET: UserCategories/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.UserCategories == null)
            {
                return NotFound();
            }

            var userCategory = await _context.UserCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userCategory == null)
            {
                return NotFound();
            }

            return View(userCategory);
        }

        // POST: UserCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            
            if (_context.UserCategories == null)
            {
                return Problem("Entity set 'ModelContext.UserCategories'  is null.");
            }
            var userCategory = await _context.UserCategories.FindAsync(id);
            if (userCategory != null)
            {
               
                _context.UserCategories.Remove(userCategory);
            }
           

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserCategoryExists(decimal id)
        {
          return (_context.UserCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
