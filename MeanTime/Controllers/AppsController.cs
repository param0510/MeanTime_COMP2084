using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeanTime.Data;
using MeanTime.Models;

namespace MeanTime.Controllers
{
    public class AppsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Apps
        public async Task<IActionResult> Index(string genre)
        {
            var applicationDbContext = _context.Apps.Include(a => a.Genre);

            if (string.IsNullOrEmpty(genre))
            {
                //RedirectToAction("Index");
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                return View(await applicationDbContext.Where(a => a.Genre.Type.Equals(genre)).ToListAsync());
            }

        }

        // GET: Apps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Apps == null)
            {
                return NotFound();
            }

            var app = await _context.Apps
                .Include(a => a.Genre)
                .FirstOrDefaultAsync(m => m.AppId == id);
            if (app == null)
            {
                return NotFound();
            }

            return View(app);
        }

        // GET: Apps/Create
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "Type");
            return View();
        }

        // POST: Apps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppId,Name,Price,Size,MetaTag,Rating,Status,GenreId")] App app, IFormFile? Image)
        {
            if(Image != null)
            {
                var fileName = UtilityClass.UploadImage(Image);
                app.Image = fileName;
            }
            if (ModelState.IsValid)
            {
                _context.Add(app);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Create", "AppDetails");
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "Type", app.GenreId);
            return View(app);
        }

        // GET: Apps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Apps == null)
            {
                return NotFound();
            }

            var app = await _context.Apps.FindAsync(id);
            if (app == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "Type", app.GenreId);
            return View(app);
        }

        // POST: Apps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppId,Name,Price,Size,MetaTag,Rating,Status,GenreId")] App app, IFormFile? Image, string? OldImage)
        {
            app.Image = OldImage;
            if(Image != null)
            {
                var fileName = UtilityClass.UploadImage(Image);
                app.Image = fileName;
            }
            if (id != app.AppId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(app);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppExists(app.AppId))
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
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "Type", app.GenreId);
            return View(app);
        }

        // GET: Apps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Apps == null)
            {
                return NotFound();
            }

            var app = await _context.Apps
                .Include(a => a.Genre)
                .FirstOrDefaultAsync(m => m.AppId == id);
            if (app == null)
            {
                return NotFound();
            }

            return View(app);
        }

        // POST: Apps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Apps == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Apps'  is null.");
            }
            var app = await _context.Apps.FindAsync(id);
            if (app != null)
            {
                _context.Apps.Remove(app);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppExists(int id)
        {
          return _context.Apps.Any(e => e.AppId == id);
        }
    }
}
