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
            //if (id == null)
            //{
            //    return NotFound();
            //}
            //var productoPedidos = await _context.ProductosPedidos
            //    .Where(pp => pp.IdPedidos == id)
            //    .Include(pp => pp.Producto)
            //    .Include(pp => pp.Pedido)
            //    .ToListAsync();

            //if (productoPedidos == null || !productoPedidos.Any())
            //{
            //    return NotFound();
            //}
            //var proveedores = await _context.Proveedores.ToListAsync();
            //var usuarios = await _context.Usuarios.ToListAsync();
            //var Productos = await _context.Productos.ToListAsync();
            //var viewModel = new PedidoEditViewModel

            //{
            //    Pedidos = productoPedidos.First().Pedido,
            //    ProductosPedidos = productoPedidos.Select(pp => pp.Producto),
            //    Usuarios = productoPedidos.OrderBy(p => p.Usuario),
            //    Cantones = cantones.OrderBy(p => p.Nombre),
            //    Distritos = distritos.OrderBy(p => p.Nombre)
            //};
            //return View(viewModel);
            return View("Details");
        }

        // GET: Pedidos/Create
        public IActionResult Create()
        {
            var model = new PedidoEditViewModel
            {
                Proveedores = _context.Proveedores,
                Productos = _context.Productos,
                Usuarios = _context.Usuarios
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
            if (id == null)
            {
                return NotFound();
            }
            var productosPedidos = await _context.ProductosPedidos
                .Where(pp => pp.IdPedidos == id)
                .Include(pp => pp.Producto)
                .ToListAsync();
            if (productosPedidos == null || !productosPedidos.Any())
            {
                return NotFound();
            }
            var proveedores = await _context.Proveedores.ToListAsync();
            var usuarios = await _context.Usuarios.ToListAsync();
            var Productos = await _context.Productos.ToListAsync();

            var viewModel = new PedidoEditViewModel
            {
                Pedidos = productosPedidos.First().Pedido,
                ProductosPedidos = productosPedidos.Select(pp => pp.Producto),
                Proveedores = proveedores.OrderBy(p => p.Nombre),
                Usuarios = usuarios.OrderBy(p => p.Nombre),
                Productos = Productos.OrderBy(p => p.Nombre)
            };
            return View(viewModel);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PedidoEditViewModel viewModel, string SelectedProductIds)
        {
            if (id != viewModel.Pedidos.IdPedidos)
            {
                return NotFound();
            }
            try
            {
                var pedidos = await _context.Pedidos.FindAsync(id);
                if (pedidos == null)
                {
                    return NotFound();
                }

                pedidos.IdProveedores = viewModel.Pedidos.IdProveedores;
                pedidos.Fecha = viewModel.Pedidos.Fecha;
                pedidos.IdUsuario = viewModel.Pedidos.IdUsuario;

                _context.Update(pedidos);

                // Eliminar los productos existentes
                var productosExistentes = _context.ProductosPedidos.Where(pp => pp.IdPedidos == id);
                _context.ProductosPedidos.RemoveRange(productosExistentes);

                // Agregar los nuevos productos
                if (!string.IsNullOrEmpty(SelectedProductIds))
                {
                    var selectedProductIds = JsonConvert.DeserializeObject<List<int>>(SelectedProductIds);

                    foreach (var productoId in selectedProductIds)
                    {
                        var productosPedidos = new ProductosPedidos
                        {
                            IdPedidos = id,
                            IdProducto = productoId
                        };
                        _context.ProductosPedidos.Add(productosPedidos);
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                // Manejo de excepciones en caso de concurrencia
                return View(viewModel);
            }
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
