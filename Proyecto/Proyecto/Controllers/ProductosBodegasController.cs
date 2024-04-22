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
    public class ProductosBodegasController : Controller
    {
        private readonly AppDbContext _context;

        public ProductosBodegasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ProductosBodegas
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ProductosBodega.Include(p => p.Bodegas).Include(p => p.Productos);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ProductosBodegas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductosBodega == null)
            {
                return NotFound();
            }

            var productosBodega = await _context.ProductosBodega
                .Include(p => p.Bodegas)
                .Include(p => p.Productos)
                .FirstOrDefaultAsync(m => m.IdProductoBodega == id);
            if (productosBodega == null)
            {
                return NotFound();
            }

            return View(productosBodega);
        }

        // GET: ProductosBodegas/Create
        public IActionResult Create()
        {
            ViewData["IdBodega"] = new SelectList(_context.Bodegas, "IdBodegas", "DireccionExacta");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProductos", "Descripcion");
            return View();
        }

        // POST: ProductosBodegas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProductoBodega,IdBodega,IdProducto,FechaIngreso,FechaVencimiento,Cantidad")] ProductosBodega productosBodega)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productosBodega);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBodega"] = new SelectList(_context.Bodegas, "IdBodegas", "DireccionExacta", productosBodega.IdBodega);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProductos", "Descripcion", productosBodega.IdProducto);
            return View(productosBodega);
        }

        // GET: ProductosBodegas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductosBodega == null)
            {
                return NotFound();
            }

            var productosBodega = await _context.ProductosBodega.FindAsync(id);
            if (productosBodega == null)
            {
                return NotFound();
            }
            ViewData["IdBodega"] = new SelectList(_context.Bodegas, "IdBodegas", "DireccionExacta", productosBodega.IdBodega);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProductos", "Descripcion", productosBodega.IdProducto);
            return View(productosBodega);
        }

        // POST: ProductosBodegas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProductoBodega,IdBodega,IdProducto,FechaIngreso,FechaVencimiento,Cantidad")] ProductosBodega productosBodega)
        {
            if (id != productosBodega.IdProductoBodega)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productosBodega);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductosBodegaExists(productosBodega.IdProductoBodega))
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
            ViewData["IdBodega"] = new SelectList(_context.Bodegas, "IdBodegas", "DireccionExacta", productosBodega.IdBodega);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProductos", "Descripcion", productosBodega.IdProducto);
            return View(productosBodega);
        }

        // GET: ProductosBodegas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductosBodega == null)
            {
                return NotFound();
            }

            var productosBodega = await _context.ProductosBodega
                .Include(p => p.Bodegas)
                .Include(p => p.Productos)
                .FirstOrDefaultAsync(m => m.IdProductoBodega == id);
            if (productosBodega == null)
            {
                return NotFound();
            }

            return View(productosBodega);
        }

        // POST: ProductosBodegas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductosBodega == null)
            {
                return Problem("Entity set 'AppDbContext.ProductosBodega'  is null.");
            }
            var productosBodega = await _context.ProductosBodega.FindAsync(id);
            if (productosBodega != null)
            {
                _context.ProductosBodega.Remove(productosBodega);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductosBodegaExists(int id)
        {
          return (_context.ProductosBodega?.Any(e => e.IdProductoBodega == id)).GetValueOrDefault();
        }
    }
}
