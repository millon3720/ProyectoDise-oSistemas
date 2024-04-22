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
    public class ProductoFacturasController : Controller
    {
        private readonly AppDbContext _context;

        public ProductoFacturasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ProductoFacturas
        public async Task<IActionResult> Index()
        {
              return _context.ProductoFactura != null ? 
                          View(await _context.ProductoFactura.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.ProductoFactura'  is null.");
        }

        // GET: ProductoFacturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductoFactura == null)
            {
                return NotFound();
            }

            var productoFactura = await _context.ProductoFactura
                .FirstOrDefaultAsync(m => m.IdProductoFactura == id);
            if (productoFactura == null)
            {
                return NotFound();
            }

            return View(productoFactura);
        }

        // GET: ProductoFacturas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductoFacturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProductoFactura,IdFactura,IdProducto,Cantidad")] ProductoFactura productoFactura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productoFactura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productoFactura);
        }

        // GET: ProductoFacturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductoFactura == null)
            {
                return NotFound();
            }

            var productoFactura = await _context.ProductoFactura.FindAsync(id);
            if (productoFactura == null)
            {
                return NotFound();
            }
            return View(productoFactura);
        }

        // POST: ProductoFacturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProductoFactura,IdFactura,IdProducto,Cantidad")] ProductoFactura productoFactura)
        {
            if (id != productoFactura.IdProductoFactura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productoFactura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoFacturaExists(productoFactura.IdProductoFactura))
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
            return View(productoFactura);
        }

        // GET: ProductoFacturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductoFactura == null)
            {
                return NotFound();
            }

            var productoFactura = await _context.ProductoFactura
                .FirstOrDefaultAsync(m => m.IdProductoFactura == id);
            if (productoFactura == null)
            {
                return NotFound();
            }

            return View(productoFactura);
        }

        // POST: ProductoFacturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductoFactura == null)
            {
                return Problem("Entity set 'AppDbContext.ProductoFactura'  is null.");
            }
            var productoFactura = await _context.ProductoFactura.FindAsync(id);
            if (productoFactura != null)
            {
                _context.ProductoFactura.Remove(productoFactura);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoFacturaExists(int id)
        {
          return (_context.ProductoFactura?.Any(e => e.IdProductoFactura == id)).GetValueOrDefault();
        }
    }
}
