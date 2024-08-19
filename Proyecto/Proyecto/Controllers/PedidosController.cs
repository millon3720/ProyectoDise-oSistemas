using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Proyecto.Data;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class PedidosController : Controller
    {
        private readonly AppDbContext _context;

        public PedidosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Pedidos
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Pedidos.Include(p => p.Proveedores).Include(p => p.Usuario);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedidos = await _context.Pedidos
                .FirstOrDefaultAsync(m => m.IdPedidos == id);
            if (pedidos == null)
            {
                return NotFound();
            }
            return View(pedidos);
        }

        // GET: Pedidos/Create
        public IActionResult Create()
        {
            var model = new PedidoEditViewModel
            {
                Proveedores = _context.Proveedores,
                Productos = _context.Productos
            };
            return View(model);
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PedidoEditViewModel model, string SelectedProductIds)
        {

            
            _context.Add(model.Pedidos);
            await _context.SaveChangesAsync();

            // Obtener el ID del proveedor recién creado
            var pedidoId = model.Pedidos.IdPedidos;

            if (!string.IsNullOrEmpty(SelectedProductIds))
            {
                var selectedProductIds = JsonConvert.DeserializeObject<List<int>>(SelectedProductIds);

                foreach (var productoId in selectedProductIds)
                {
                    var productosPedidos = new ProductosPedidos
                    {
                        IdPedidos = pedidoId,
                        IdProducto = productoId
                    };
                    _context.ProductosPedidos.Add(productosPedidos);
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
       
        // GET: Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedidos = await _context.Pedidos.FindAsync(id);
            if (pedidos == null)
            {
                return NotFound();
            }
            return View("Edit");
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPedidos,IdProveedor,Fecha,IdUsuario")] Pedidos pedidos)
        {
            if (id != pedidos.IdPedidos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedidos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidosExists(pedidos.IdPedidos))
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
            return View(pedidos);
        }

        // GET: Pedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedidos = await _context.Pedidos
                .FirstOrDefaultAsync(m => m.IdPedidos == id);
            if (pedidos == null)
            {
                return NotFound();
            }

            return View(pedidos);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pedidos == null)
            {
                return Problem("Entity set 'AppDbContext.Pedidos'  is null.");
            }
            var pedidos = await _context.Pedidos.FindAsync(id);
            if (pedidos != null)
            {
                _context.Pedidos.Remove(pedidos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidosExists(int id)
        {
          return (_context.Pedidos?.Any(e => e.IdPedidos == id)).GetValueOrDefault();
        }
    }
}
