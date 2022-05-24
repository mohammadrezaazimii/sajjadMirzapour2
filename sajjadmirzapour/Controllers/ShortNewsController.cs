using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sajjadmirzapour;
using sajjadmirzapour.Models;

namespace sajjadmirzapour.Controllers
{
    public class ShortNewsController : Controller
    {
        private readonly Context _context;

        public ShortNewsController(Context context)
        {
            _context = context;
        }

        // GET: ShortNews
        public async Task<IActionResult> Index()
        {
              return View(await _context.ShortNews.ToListAsync());
        }

        // GET: ShortNews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ShortNews == null)
            {
                return NotFound();
            }

            var shortNews = await _context.ShortNews
                .FirstOrDefaultAsync(m => m.id == id);
            if (shortNews == null)
            {
                return NotFound();
            }

            return View(shortNews);
        }

        // GET: ShortNews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShortNews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Writer,NewsTitle,ShortDescription,Description,date,Picture")] ShortNews shortNews)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shortNews);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shortNews);
        }

        // GET: ShortNews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ShortNews == null)
            {
                return NotFound();
            }

            var shortNews = await _context.ShortNews.FindAsync(id);
            if (shortNews == null)
            {
                return NotFound();
            }
            return View(shortNews);
        }

        // POST: ShortNews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Writer,NewsTitle,ShortDescription,Description,date,Picture")] ShortNews shortNews)
        {
            if (id != shortNews.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shortNews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShortNewsExists(shortNews.id))
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
            return View(shortNews);
        }

        // GET: ShortNews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ShortNews == null)
            {
                return NotFound();
            }

            var shortNews = await _context.ShortNews
                .FirstOrDefaultAsync(m => m.id == id);
            if (shortNews == null)
            {
                return NotFound();
            }

            return View(shortNews);
        }

        // POST: ShortNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ShortNews == null)
            {
                return Problem("Entity set 'Context.ShortNews'  is null.");
            }
            var shortNews = await _context.ShortNews.FindAsync(id);
            if (shortNews != null)
            {
                _context.ShortNews.Remove(shortNews);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShortNewsExists(int id)
        {
          return _context.ShortNews.Any(e => e.id == id);
        }
    }
}
