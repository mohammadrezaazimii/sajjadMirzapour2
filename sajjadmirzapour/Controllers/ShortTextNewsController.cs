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
    public class ShortTextNewsController : Controller
    {
        private readonly Context _context;

        public ShortTextNewsController(Context context)
        {
            _context = context;
        }

        // GET: ShortTextNews
        public async Task<IActionResult> Index()
        {
              return View(await _context.ShortTextNews.ToListAsync());
        }

        // GET: ShortTextNews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ShortTextNews == null)
            {
                return NotFound();
            }

            var shortTextNews = await _context.ShortTextNews
                .FirstOrDefaultAsync(m => m.id == id);
            if (shortTextNews == null)
            {
                return NotFound();
            }

            return View(shortTextNews);
        }

        // GET: ShortTextNews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShortTextNews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Description")] ShortTextNews shortTextNews)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shortTextNews);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shortTextNews);
        }

        // GET: ShortTextNews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ShortTextNews == null)
            {
                return NotFound();
            }

            var shortTextNews = await _context.ShortTextNews.FindAsync(id);
            if (shortTextNews == null)
            {
                return NotFound();
            }
            return View(shortTextNews);
        }

        // POST: ShortTextNews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Description")] ShortTextNews shortTextNews)
        {
            if (id != shortTextNews.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shortTextNews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShortTextNewsExists(shortTextNews.id))
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
            return View(shortTextNews);
        }

        // GET: ShortTextNews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ShortTextNews == null)
            {
                return NotFound();
            }

            var shortTextNews = await _context.ShortTextNews
                .FirstOrDefaultAsync(m => m.id == id);
            if (shortTextNews == null)
            {
                return NotFound();
            }

            return View(shortTextNews);
        }

        // POST: ShortTextNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ShortTextNews == null)
            {
                return Problem("Entity set 'Context.ShortTextNews'  is null.");
            }
            var shortTextNews = await _context.ShortTextNews.FindAsync(id);
            if (shortTextNews != null)
            {
                _context.ShortTextNews.Remove(shortTextNews);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShortTextNewsExists(int id)
        {
          return _context.ShortTextNews.Any(e => e.id == id);
        }
    }
}
