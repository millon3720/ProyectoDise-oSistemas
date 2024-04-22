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
    public class DistritoesController : Controller
    {
        private readonly AppDbContext _context;

        public DistritoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Distritoes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Distrito.Include(d => d.Canton);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Distritoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Distrito == null)
            {
                return NotFound();
            }

            var distrito = await _context.Distrito
                .Include(d => d.Canton)
                .FirstOrDefaultAsync(m => m.IdDistrito == id);
            if (distrito == null)
            {
                return NotFound();
            }

            return View(distrito);
        }

        // GET: Distritoes/Create
        public IActionResult Create()
        {
            ViewData["IdCanton"] = new SelectList(_context.Canton, "IdCanton", "Nombre");
            return View();
        }

        // POST: Distritoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDistrito,Nombre,IdCanton")] Distrito distrito)
        {
            if (ModelState.IsValid)
            {
                _context.Add(distrito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCanton"] = new SelectList(_context.Canton, "IdCanton", "Nombre", distrito.IdCanton);
            return View(distrito);
        }

        // GET: Distritoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Distrito == null)
            {
                return NotFound();
            }

            var distrito = await _context.Distrito.FindAsync(id);
            if (distrito == null)
            {
                return NotFound();
            }
            ViewData["IdCanton"] = new SelectList(_context.Canton, "IdCanton", "Nombre", distrito.IdCanton);
            return View(distrito);
        }

        // POST: Distritoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDistrito,Nombre,IdCanton")] Distrito distrito)
        {
            if (id != distrito.IdDistrito)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(distrito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DistritoExists(distrito.IdDistrito))
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
            ViewData["IdCanton"] = new SelectList(_context.Canton, "IdCanton", "Nombre", distrito.IdCanton);
            return View(distrito);
        }

        // GET: Distritoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Distrito == null)
            {
                return NotFound();
            }

            var distrito = await _context.Distrito
                .Include(d => d.Canton)
                .FirstOrDefaultAsync(m => m.IdDistrito == id);
            if (distrito == null)
            {
                return NotFound();
            }

            return View(distrito);
        }

        // POST: Distritoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Distrito == null)
            {
                return Problem("Entity set 'AppDbContext.Distrito'  is null.");
            }
            var distrito = await _context.Distrito.FindAsync(id);
            if (distrito != null)
            {
                _context.Distrito.Remove(distrito);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DistritoExists(int id)
        {
          return (_context.Distrito?.Any(e => e.IdDistrito == id)).GetValueOrDefault();
        }
    }
}
