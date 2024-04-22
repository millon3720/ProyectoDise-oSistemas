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
    public class ProductosPedidosController : Controller
    {
        private readonly AppDbContext _context;

        public ProductosPedidosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ProductosPedidos
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ProductosPedidos.Include(p => p.Pedido).Include(p => p.Producto);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ProductosPedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductosPedidos == null)
            {
                return NotFound();
            }

            var productosPedidos = await _context.ProductosPedidos
                .Include(p => p.Pedido)
                .Include(p => p.Producto)
                .FirstOrDefaultAsync(m => m.IdProductosPedidos == id);
            if (productosPedidos == null)
            {
                return NotFound();
            }

            return View(productosPedidos);
        }

        // GET: ProductosPedidos/Create
        public IActionResult Create()
        {
            ViewData["IdPedidos"] = new SelectList(_context.Pedidos, "IdPedidos", "IdPedidos");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProductos", "Descripcion");
            return View();
        }

        // POST: ProductosPedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProductosPedidos,IdPedidos,IdProducto,Cantidad")] ProductosPedidos productosPedidos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productosPedidos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPedidos"] = new SelectList(_context.Pedidos, "IdPedidos", "IdPedidos", productosPedidos.IdPedidos);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProductos", "Descripcion", productosPedidos.IdProducto);
            return View(productosPedidos);
        }

        // GET: ProductosPedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductosPedidos == null)
            {
                return NotFound();
            }

            var productosPedidos = await _context.ProductosPedidos.FindAsync(id);
            if (productosPedidos == null)
            {
                return NotFound();
            }
            ViewData["IdPedidos"] = new SelectList(_context.Pedidos, "IdPedidos", "IdPedidos", productosPedidos.IdPedidos);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProductos", "Descripcion", productosPedidos.IdProducto);
            return View(productosPedidos);
        }

        // POST: ProductosPedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProductosPedidos,IdPedidos,IdProducto,Cantidad")] ProductosPedidos productosPedidos)
        {
            if (id != productosPedidos.IdProductosPedidos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productosPedidos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductosPedidosExists(productosPedidos.IdProductosPedidos))
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
            ViewData["IdPedidos"] = new SelectList(_context.Pedidos, "IdPedidos", "IdPedidos", productosPedidos.IdPedidos);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProductos", "Descripcion", productosPedidos.IdProducto);
            return View(productosPedidos);
        }

        // GET: ProductosPedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductosPedidos == null)
            {
                return NotFound();
            }

            var productosPedidos = await _context.ProductosPedidos
                .Include(p => p.Pedido)
                .Include(p => p.Producto)
                .FirstOrDefaultAsync(m => m.IdProductosPedidos == id);
            if (productosPedidos == null)
            {
                return NotFound();
            }

            return View(productosPedidos);
        }

        // POST: ProductosPedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductosPedidos == null)
            {
                return Problem("Entity set 'AppDbContext.ProductosPedidos'  is null.");
            }
            var productosPedidos = await _context.ProductosPedidos.FindAsync(id);
            if (productosPedidos != null)
            {
                _context.ProductosPedidos.Remove(productosPedidos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductosPedidosExists(int id)
        {
          return (_context.ProductosPedidos?.Any(e => e.IdProductosPedidos == id)).GetValueOrDefault();
        }
    }
}
