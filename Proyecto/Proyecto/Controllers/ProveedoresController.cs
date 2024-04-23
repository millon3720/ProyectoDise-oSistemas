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
    public class ProveedoresController : Controller
    {
        private readonly AppDbContext _context;

        public ProveedoresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Proveedores
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Proveedores.Include(p => p.Canton).Include(p => p.Distrito).Include(p => p.Provincia);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Proveedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            return View("Details");
            //if (id == null || _context.Proveedores == null)
            //{
            //    return NotFound();
            //}

            //var proveedores = await _context.Proveedores
            //    .Include(p => p.Canton)
            //    .Include(p => p.Distrito)
            //    .Include(p => p.Provincia)
            //    .FirstOrDefaultAsync(m => m.IdProveedores == id);
            //if (proveedores == null)
            //{
            //    return NotFound();
            //}

            //return View(proveedores);
        }

        // GET: Proveedores/Create
        public IActionResult Create()
        {
            ViewData["IdCanton"] = new SelectList(_context.Canton, "IdCanton", "Nombre");
            ViewData["IdDistrito"] = new SelectList(_context.Distrito, "IdDistrito", "Nombre");
            ViewData["IdProvincia"] = new SelectList(_context.Provincia, "IdProvincia", "Nombre");
            return View();
        }

        // POST: Proveedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProveedores,Cedula,Correo,Nombre,Telefono,IdProvincia,IdCanton,IdDistrito,DireccionExacta")] Proveedores proveedores)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proveedores);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCanton"] = new SelectList(_context.Canton, "IdCanton", "Nombre", proveedores.IdCanton);
            ViewData["IdDistrito"] = new SelectList(_context.Distrito, "IdDistrito", "Nombre", proveedores.IdDistrito);
            ViewData["IdProvincia"] = new SelectList(_context.Provincia, "IdProvincia", "Nombre", proveedores.IdProvincia);
            return View(proveedores);
        }

        // GET: Proveedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            return View("Edit");
            //if (id == null || _context.Proveedores == null)
            //{
            //    return NotFound();
            //}

            //var proveedores = await _context.Proveedores.FindAsync(id);
            //if (proveedores == null)
            //{
            //    return NotFound();
            //}
            //ViewData["IdCanton"] = new SelectList(_context.Canton, "IdCanton", "Nombre", proveedores.IdCanton);
            //ViewData["IdDistrito"] = new SelectList(_context.Distrito, "IdDistrito", "Nombre", proveedores.IdDistrito);
            //ViewData["IdProvincia"] = new SelectList(_context.Provincia, "IdProvincia", "Nombre", proveedores.IdProvincia);
            //return View(proveedores);
        }

        // POST: Proveedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProveedores,Cedula,Correo,Nombre,Telefono,IdProvincia,IdCanton,IdDistrito,DireccionExacta")] Proveedores proveedores)
        {
            if (id != proveedores.IdProveedores)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proveedores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProveedoresExists(proveedores.IdProveedores))
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
            ViewData["IdCanton"] = new SelectList(_context.Canton, "IdCanton", "Nombre", proveedores.IdCanton);
            ViewData["IdDistrito"] = new SelectList(_context.Distrito, "IdDistrito", "Nombre", proveedores.IdDistrito);
            ViewData["IdProvincia"] = new SelectList(_context.Provincia, "IdProvincia", "Nombre", proveedores.IdProvincia);
            return View(proveedores);
        }

        // GET: Proveedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            return View("Delete");
            //if (id == null || _context.Proveedores == null)
            //{
            //    return NotFound();
            //}

            //var proveedores = await _context.Proveedores
            //    .Include(p => p.Canton)
            //    .Include(p => p.Distrito)
            //    .Include(p => p.Provincia)
            //    .FirstOrDefaultAsync(m => m.IdProveedores == id);
            //if (proveedores == null)
            //{
            //    return NotFound();
            //}

            //return View(proveedores);
        }

        // POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Proveedores == null)
            {
                return Problem("Entity set 'AppDbContext.Proveedores'  is null.");
            }
            var proveedores = await _context.Proveedores.FindAsync(id);
            if (proveedores != null)
            {
                _context.Proveedores.Remove(proveedores);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProveedoresExists(int id)
        {
          return (_context.Proveedores?.Any(e => e.IdProveedores == id)).GetValueOrDefault();
        }
    }
}
