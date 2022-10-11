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
    public class AppDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AppDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AppDetails.Include(a => a.App);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AppDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AppDetails == null)
            {
                return NotFound();
            }

            var appDetail = await _context.AppDetails
                .Include(a => a.App)
                .FirstOrDefaultAsync(m => m.AppId == id);
            if (appDetail == null)
            {
                return RedirectToAction("Create");
                //return NotFound();
            }

            return View(appDetail);
        }

        // GET: AppDetails/Create
        public IActionResult Create()
        {
            ViewData["AppId"] = new SelectList(_context.Apps.OrderByDescending(a => a.AppId), "AppId", "Name");
            return View();
        }

        // POST: AppDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppId,Owner,Mode,Description,Duration,TotalDataUsage,AvgMemoryUsage,Downloads,InstallDate")] AppDetail appDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appDetail);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Apps");
            }
            ViewData["AppId"] = new SelectList(_context.Apps, "AppId", "Name", appDetail.AppId);
            return View(appDetail);
        }

        // GET: AppDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AppDetails == null)
            {
                return NotFound();
            }

            var appDetail = await _context.AppDetails.FindAsync(id);
            if (appDetail == null)
            {
                return NotFound();
            }
            ViewData["AppId"] = new SelectList(_context.Apps, "AppId", "Name", appDetail.AppId);
            return View(appDetail);
        }

        // POST: AppDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppId,Owner,Mode,Description,Duration,TotalDataUsage,AvgMemoryUsage,Downloads,InstallDate")] AppDetail appDetail)
        {
            if (id != appDetail.AppId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppDetailExists(appDetail.AppId))
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
            ViewData["AppId"] = new SelectList(_context.Apps, "AppId", "Name", appDetail.AppId);
            return View(appDetail);
        }

        // GET: AppDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AppDetails == null)
            {
                return NotFound();
            }

            var appDetail = await _context.AppDetails
                .Include(a => a.App)
                .FirstOrDefaultAsync(m => m.AppId == id);
            if (appDetail == null)
            {
                return NotFound();
            }

            return View(appDetail);
        }

        // POST: AppDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AppDetails == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AppDetails'  is null.");
            }
            var appDetail = await _context.AppDetails.FindAsync(id);
            if (appDetail != null)
            {
                _context.AppDetails.Remove(appDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppDetailExists(int id)
        {
          return _context.AppDetails.Any(e => e.AppId == id);
        }
    }
}
