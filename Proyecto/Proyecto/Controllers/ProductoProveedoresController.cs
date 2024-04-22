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
    public class ProductoProveedoresController : Controller
    {
        private readonly AppDbContext _context;

        public ProductoProveedoresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ProductoProveedores
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ProductoProveedores.Include(p => p.Productos).Include(p => p.Proveedores);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ProductoProveedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductoProveedores == null)
            {
                return NotFound();
            }

            var productoProveedores = await _context.ProductoProveedores
                .Include(p => p.Productos)
                .Include(p => p.Proveedores)
                .FirstOrDefaultAsync(m => m.IdProductoProveedores == id);
            if (productoProveedores == null)
            {
                return NotFound();
            }

            return View(productoProveedores);
        }

        // GET: ProductoProveedores/Create
        public IActionResult Create()
        {
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProductos", "Descripcion");
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedores", "Cedula");
            return View();
        }

        // POST: ProductoProveedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProductoProveedores,IdProveedor,IdProducto,Precio")] ProductoProveedores productoProveedores)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productoProveedores);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProductos", "Descripcion", productoProveedores.IdProducto);
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedores", "Cedula", productoProveedores.IdProveedor);
            return View(productoProveedores);
        }

        // GET: ProductoProveedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductoProveedores == null)
            {
                return NotFound();
            }

            var productoProveedores = await _context.ProductoProveedores.FindAsync(id);
            if (productoProveedores == null)
            {
                return NotFound();
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProductos", "Descripcion", productoProveedores.IdProducto);
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedores", "Cedula", productoProveedores.IdProveedor);
            return View(productoProveedores);
        }

        // POST: ProductoProveedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProductoProveedores,IdProveedor,IdProducto,Precio")] ProductoProveedores productoProveedores)
        {
            if (id != productoProveedores.IdProductoProveedores)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productoProveedores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoProveedoresExists(productoProveedores.IdProductoProveedores))
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
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProductos", "Descripcion", productoProveedores.IdProducto);
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedores", "Cedula", productoProveedores.IdProveedor);
            return View(productoProveedores);
        }

        // GET: ProductoProveedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductoProveedores == null)
            {
                return NotFound();
            }

            var productoProveedores = await _context.ProductoProveedores
                .Include(p => p.Productos)
                .Include(p => p.Proveedores)
                .FirstOrDefaultAsync(m => m.IdProductoProveedores == id);
            if (productoProveedores == null)
            {
                return NotFound();
            }

            return View(productoProveedores);
        }

        // POST: ProductoProveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductoProveedores == null)
            {
                return Problem("Entity set 'AppDbContext.ProductoProveedores'  is null.");
            }
            var productoProveedores = await _context.ProductoProveedores.FindAsync(id);
            if (productoProveedores != null)
            {
                _context.ProductoProveedores.Remove(productoProveedores);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoProveedoresExists(int id)
        {
          return (_context.ProductoProveedores?.Any(e => e.IdProductoProveedores == id)).GetValueOrDefault();
        }
    }
}
