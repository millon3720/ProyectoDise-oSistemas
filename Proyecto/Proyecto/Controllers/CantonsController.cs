using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto.Data;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class CantonsController : Controller
    {
        private readonly AppDbContext _context;

        public CantonsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Cantons
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Canton.Include(c => c.Provincia);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Cantons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Canton == null)
            {
                return NotFound();
            }

            var canton = await _context.Canton
                .Include(c => c.Provincia)
                .FirstOrDefaultAsync(m => m.IdCanton == id);
            if (canton == null)
            {
                return NotFound();
            }

            return View(canton);
        }

        // GET: Cantons/Create
        public IActionResult Create()
        {
            ViewData["IdProvincia"] = new SelectList(_context.Provincia, "IdProvincia", "Nombre");
            return View();
        }

        // POST: Cantons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCanton,Nombre,IdProvincia")] Canton canton)
        {
            if (ModelState.IsValid)
            {
                _context.Add(canton);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProvincia"] = new SelectList(_context.Provincia, "IdProvincia", "Nombre", canton.IdProvincia);
            return View(canton);
        }

        // GET: Cantons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Canton == null)
            {
                return NotFound();
            }

            var canton = await _context.Canton.FindAsync(id);
            if (canton == null)
            {
                return NotFound();
            }
            ViewData["IdProvincia"] = new SelectList(_context.Provincia, "IdProvincia", "Nombre", canton.IdProvincia);
            return View(canton);
        }

        // POST: Cantons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCanton,Nombre,IdProvincia")] Canton canton)
        {
            if (id != canton.IdCanton)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(canton);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CantonExists(canton.IdCanton))
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
            ViewData["IdProvincia"] = new SelectList(_context.Provincia, "IdProvincia", "Nombre", canton.IdProvincia);
            return View(canton);
        }

        // GET: Cantons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Canton == null)
            {
                return NotFound();
            }

            var canton = await _context.Canton
                .Include(c => c.Provincia)
                .FirstOrDefaultAsync(m => m.IdCanton == id);
            if (canton == null)
            {
                return NotFound();
            }

            return View(canton);
        }

        // POST: Cantons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Canton == null)
            {
                return Problem("Entity set 'AppDbContext.Canton'  is null.");
            }
            var canton = await _context.Canton.FindAsync(id);
            if (canton != null)
            {
                _context.Canton.Remove(canton);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CantonExists(int id)
        {
          return (_context.Canton?.Any(e => e.IdCanton == id)).GetValueOrDefault();
        }
    }
}
