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

            if (id == null)
            {
                return NotFound();
            }
            var proveedorConProductos = await _context.ProductoProveedores
                .Where(pp => pp.IdProveedor == id)
                .Include(pp => pp.Productos)   
                .Include(pp => pp.Proveedores)   
                .ToListAsync();  

            if (proveedorConProductos == null || !proveedorConProductos.Any())
            {
                return NotFound();
            }
            var provincias = await _context.Provincia.ToListAsync();
            var cantones = await _context.Canton.ToListAsync();
            var distritos = await _context.Distrito.ToListAsync();
            var viewModel = new ProveedorEditViewModel
            {
                Proveedor = proveedorConProductos.First().Proveedores, 
                ProductosProveedor = proveedorConProductos.Select(pp => pp.Productos),  
                Provincias = provincias.OrderBy(p => p.Nombre),
                Cantones = cantones.OrderBy(p => p.Nombre),
                Distritos = distritos.OrderBy(p => p.Nombre)
            };
            return View(viewModel);
        }

        // GET: Proveedor/Create
        public IActionResult Create()
        {
            var model = new ProveedorEditViewModel
            {
                Provincias = _context.Provincia,
                Productos = _context.Productos
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProveedorEditViewModel model, string SelectedProductIds)
        {
          

            // Agregar el proveedor al contexto
            _context.Add(model.Proveedor);
            await _context.SaveChangesAsync();

            // Obtener el ID del proveedor recién creado
            var proveedorId = model.Proveedor.IdProveedores;

            if (!string.IsNullOrEmpty(SelectedProductIds))
            {
                var selectedProductIds = JsonConvert.DeserializeObject<List<int>>(SelectedProductIds);

                foreach (var productoId in selectedProductIds)
                {
                    var productoProveedor = new ProductoProveedores
                    {
                        IdProveedor = proveedorId,
                        IdProducto = productoId
                    };
                    _context.ProductoProveedores.Add(productoProveedor);
                }
            }

            await _context.SaveChangesAsync();
            

            return RedirectToAction(nameof(Index));
        }


        // GET: Proveedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var proveedorConProductos = await _context.ProductoProveedores
                .Where(pp => pp.IdProveedor == id)
                .Include(pp => pp.Productos)   
                .Include(pp => pp.Proveedores)  
                .ToListAsync();  
            if (proveedorConProductos == null || !proveedorConProductos.Any())
            {
                return NotFound();
            }
            var provincias = await _context.Provincia.ToListAsync();
            var cantones = await _context.Canton.ToListAsync();
            var distritos = await _context.Distrito.ToListAsync();
            var Productos = await _context.Productos.ToListAsync();

            var viewModel = new ProveedorEditViewModel
            {
                Proveedor = proveedorConProductos.First().Proveedores, 
                ProductosProveedor = proveedorConProductos.Select(pp => pp.Productos),  
                Provincias = provincias.OrderBy(p=> p.Nombre),
                Cantones = cantones.OrderBy(p => p.Nombre),
                Distritos = distritos.OrderBy(p => p.Nombre),
                Productos = Productos.OrderBy(p => p.Nombre)
            };

            return View(viewModel);
        }



        // POST: Proveedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProveedorEditViewModel viewModel, string SelectedProductIds)
        {
            if (id != viewModel.Proveedor.IdProveedores)
            {
                return NotFound();
            }

            try
            {
                var proveedor = await _context.Proveedores.FindAsync(id);
                if (proveedor == null)
                {
                    return NotFound();
                }

                proveedor.Nombre = viewModel.Proveedor.Nombre;
                proveedor.Correo = viewModel.Proveedor.Correo;
                proveedor.Telefono1 = viewModel.Proveedor.Telefono1;
                proveedor.Telefono2 = viewModel.Proveedor.Telefono2;
                proveedor.IdProvincia = viewModel.Proveedor.IdProvincia;
                proveedor.IdCanton = viewModel.Proveedor.IdCanton;
                proveedor.IdDistrito = viewModel.Proveedor.IdDistrito;
                proveedor.DireccionExacta = viewModel.Proveedor.DireccionExacta;

                _context.Update(proveedor);

                // Eliminar los productos existentes
                var productosExistentes = _context.ProductoProveedores.Where(pp => pp.IdProveedor == id);
                _context.ProductoProveedores.RemoveRange(productosExistentes);

                // Agregar los nuevos productos
                if (!string.IsNullOrEmpty(SelectedProductIds))
                {
                    var selectedProductIds = JsonConvert.DeserializeObject<List<int>>(SelectedProductIds);

                    foreach (var productoId in selectedProductIds)
                    {
                        var productoProveedor = new ProductoProveedores
                        {
                            IdProveedor = id,
                            IdProducto = productoId
                        };
                        _context.ProductoProveedores.Add(productoProveedor);
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

        // GET: Proveedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var proveedorConProductos = await _context.ProductoProveedores
                .Where(pp => pp.IdProveedor == id)
                .Include(pp => pp.Proveedores)   
                .ToListAsync();  
            if (proveedorConProductos == null || !proveedorConProductos.Any())
            {
                return NotFound();
            }
            var provincias = await _context.Provincia.ToListAsync();
            var cantones = await _context.Canton.ToListAsync();
            var distritos = await _context.Distrito.ToListAsync();
            var viewModel = new ProveedorEditViewModel
            {
                Proveedor = proveedorConProductos.First().Proveedores, 
                Provincias = provincias.OrderBy(p => p.Nombre),
                Cantones = cantones.OrderBy(p => p.Nombre),
                Distritos = distritos.OrderBy(p => p.Nombre)
            };
            return View(viewModel);
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
