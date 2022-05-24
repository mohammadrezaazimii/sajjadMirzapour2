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
    public class ProNewsController : Controller
    {
        private readonly Context _context;

        public ProNewsController(Context context)
        {
            _context = context;
        }

        // GET: ProNews
        public async Task<IActionResult> Index()
        {
              return View(await _context.ProNews.ToListAsync());
        }

        // GET: ProNews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProNews == null)
            {
                return NotFound();
            }

            var proNews = await _context.ProNews
                .FirstOrDefaultAsync(m => m.id == id);
            if (proNews == null)
            {
                return NotFound();
            }

            return View(proNews);
        }

        // GET: ProNews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProNews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Writer,NewsTitle,ShortDescription,Description,date,Picture")] ProNews proNews)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proNews);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proNews);
        }

        // GET: ProNews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProNews == null)
            {
                return NotFound();
            }

            var proNews = await _context.ProNews.FindAsync(id);
            if (proNews == null)
            {
                return NotFound();
            }
            return View(proNews);
        }

        // POST: ProNews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Writer,NewsTitle,ShortDescription,Description,date,Picture")] ProNews proNews)
        {
            if (id != proNews.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proNews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProNewsExists(proNews.id))
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
            return View(proNews);
        }

        // GET: ProNews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProNews == null)
            {
                return NotFound();
            }

            var proNews = await _context.ProNews
                .FirstOrDefaultAsync(m => m.id == id);
            if (proNews == null)
            {
                return NotFound();
            }

            return View(proNews);
        }

        // POST: ProNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProNews == null)
            {
                return Problem("Entity set 'Context.ProNews'  is null.");
            }
            var proNews = await _context.ProNews.FindAsync(id);
            if (proNews != null)
            {
                _context.ProNews.Remove(proNews);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProNewsExists(int id)
        {
          return _context.ProNews.Any(e => e.id == id);
        }
    }
}
