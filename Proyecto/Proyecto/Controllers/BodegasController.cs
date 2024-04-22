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
    public class BodegasController : Controller
    {
        private readonly AppDbContext _context;

        public BodegasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Bodegas
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Bodegas.Include(b => b.Canton).Include(b => b.Distrito).Include(b => b.Provincia);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Bodegas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bodegas == null)
            {
                return NotFound();
            }

            var bodegas = await _context.Bodegas
                .Include(b => b.Canton)
                .Include(b => b.Distrito)
                .Include(b => b.Provincia)
                .FirstOrDefaultAsync(m => m.IdBodegas == id);
            if (bodegas == null)
            {
                return NotFound();
            }

            return View(bodegas);
        }

        // GET: Bodegas/Create
        public IActionResult Create()
        {
            ViewData["IdCanton"] = new SelectList(_context.Canton, "IdCanton", "Nombre");
            ViewData["IdDistrito"] = new SelectList(_context.Distrito, "IdDistrito", "Nombre");
            ViewData["IdProvincia"] = new SelectList(_context.Provincia, "IdProvincia", "Nombre");
            return View();
        }

        // POST: Bodegas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBodegas,IdProvincia,IdCanton,IdDistrito,DireccionExacta")] Bodegas bodegas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bodegas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCanton"] = new SelectList(_context.Canton, "IdCanton", "Nombre", bodegas.IdCanton);
            ViewData["IdDistrito"] = new SelectList(_context.Distrito, "IdDistrito", "Nombre", bodegas.IdDistrito);
            ViewData["IdProvincia"] = new SelectList(_context.Provincia, "IdProvincia", "Nombre", bodegas.IdProvincia);
            return View(bodegas);
        }

        // GET: Bodegas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            return View("Edit");

            //if (id == null || _context.Bodegas == null)
            //{
            //    return NotFound();
            //}

            //var bodegas = await _context.Bodegas.FindAsync(id);
            //if (bodegas == null)
            //{
            //    return NotFound();
            //}
            //ViewData["IdCanton"] = new SelectList(_context.Canton, "IdCanton", "Nombre", bodegas.IdCanton);
            //ViewData["IdDistrito"] = new SelectList(_context.Distrito, "IdDistrito", "Nombre", bodegas.IdDistrito);
            //ViewData["IdProvincia"] = new SelectList(_context.Provincia, "IdProvincia", "Nombre", bodegas.IdProvincia);
            //return View(bodegas);
        }

        // POST: Bodegas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBodegas,IdProvincia,IdCanton,IdDistrito,DireccionExacta")] Bodegas bodegas)
        {
            if (id != bodegas.IdBodegas)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bodegas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BodegasExists(bodegas.IdBodegas))
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
            ViewData["IdCanton"] = new SelectList(_context.Canton, "IdCanton", "Nombre", bodegas.IdCanton);
            ViewData["IdDistrito"] = new SelectList(_context.Distrito, "IdDistrito", "Nombre", bodegas.IdDistrito);
            ViewData["IdProvincia"] = new SelectList(_context.Provincia, "IdProvincia", "Nombre", bodegas.IdProvincia);
            return View(bodegas);
        }

        // GET: Bodegas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bodegas == null)
            {
                return NotFound();
            }

            var bodegas = await _context.Bodegas
                .Include(b => b.Canton)
                .Include(b => b.Distrito)
                .Include(b => b.Provincia)
                .FirstOrDefaultAsync(m => m.IdBodegas == id);
            if (bodegas == null)
            {
                return NotFound();
            }

            return View(bodegas);
        }

        // POST: Bodegas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bodegas == null)
            {
                return Problem("Entity set 'AppDbContext.Bodegas'  is null.");
            }
            var bodegas = await _context.Bodegas.FindAsync(id);
            if (bodegas != null)
            {
                _context.Bodegas.Remove(bodegas);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BodegasExists(int id)
        {
          return (_context.Bodegas?.Any(e => e.IdBodegas == id)).GetValueOrDefault();
        }
    }
}
