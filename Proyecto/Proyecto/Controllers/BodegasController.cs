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
            if (id == null)
            {
                return NotFound();
            }
            var bodegasConProductos = await _context.ProductosBodega
                .Where(pp => pp.IdBodega == id)
                .Include(pp => pp.Productos)
                .Include(pp => pp.Bodegas)
                .ToListAsync();

            if (bodegasConProductos == null || !bodegasConProductos.Any())
            {
                return NotFound();
            }
            var provincias = await _context.Provincia.ToListAsync();
            var cantones = await _context.Canton.ToListAsync();
            var distritos = await _context.Distrito.ToListAsync();
            var viewModel = new BodegasModelo
            {
                Bodegas = bodegasConProductos.First().Bodegas,
                ProductosBodegas = bodegasConProductos, 
                Provincias = provincias.OrderBy(p => p.Nombre),
                Cantones = cantones.OrderBy(p => p.Nombre),
                Distritos = distritos.OrderBy(p => p.Nombre)
            };

            return View(viewModel);
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
            if (id == null)
            {
                return NotFound();
            }
            var bodegasConProductos = await _context.ProductosBodega
                .Where(pp => pp.IdBodega == id)
                .Include(pp => pp.Productos)
                .Include(pp => pp.Bodegas)
                .ToListAsync();

            if (bodegasConProductos == null || !bodegasConProductos.Any())
            {
                return NotFound();
            }
            var provincias = await _context.Provincia.ToListAsync();
            var cantones = await _context.Canton.ToListAsync();
            var distritos = await _context.Distrito.ToListAsync();
            var Productos = await _context.Productos.ToListAsync();
            var viewModel = new BodegasModelo
            {
                Bodegas = bodegasConProductos.First().Bodegas,
                ProductosBodegas = bodegasConProductos,
                Provincias = provincias.OrderBy(p => p.Nombre),
                Cantones = cantones.OrderBy(p => p.Nombre),
                Distritos = distritos.OrderBy(p => p.Nombre),
                Productos=Productos.OrderBy(p=>p.Nombre)

            };

            return View(viewModel);
        }

        // POST: Bodegas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBodegas,Nombre,IdProvincia,IdCanton,IdDistrito,DireccionExacta")] Bodegas bodegas, string productosData)
        {
            if (id != bodegas.IdBodegas)
            {
                return NotFound();
            }

            try
            {
                // Deserializar los datos de productos
                var productos = JsonConvert.DeserializeObject<List<ProductosBodega>>(productosData);

                // Obtener los productos existentes para la bodega
                var productosExistentes = _context.ProductosBodega.Where(pp => pp.IdBodega == id).ToList();

                // Verificar si los productos existen en la tabla Productos
                var idsProductosExistentes = _context.Productos.Select(p => p.IdProductos).ToHashSet();
                var idsProductos = productos.Select(p => p.IdProducto).ToHashSet();

                if (!idsProductos.All(id => idsProductosExistentes.Contains(id)))
                {
                    // Manejar el caso cuando uno o más IdProducto no existen
                    ModelState.AddModelError("", "Uno o más productos no existen en la base de datos.");
                    return View(bodegas); // O redirige a la página de error
                }

                // Eliminar los productos que no están en la lista actual
                var productosIds = productos.Select(p => p.IdProducto).ToHashSet();
                var productosParaEliminar = productosExistentes.Where(pe => !productosIds.Contains(pe.IdProducto)).ToList();
                _context.ProductosBodega.RemoveRange(productosParaEliminar);

                // Actualizar o agregar productos
                foreach (var producto in productos)
                {
                    var existingProduct = productosExistentes.FirstOrDefault(p => p.IdProducto == producto.IdProducto);

                    if (existingProduct != null)
                    {
                        // Actualizar el producto existente
                        existingProduct.FechaIngreso = producto.FechaIngreso;
                        existingProduct.FechaVencimiento = producto.FechaVencimiento;
                        _context.ProductosBodega.Update(existingProduct);
                    }
                    else
                    {
                        // Agregar nuevo producto
                        var nuevoProducto = new ProductosBodega
                        {
                            IdBodega = id,
                            IdProducto = producto.IdProducto,
                            FechaIngreso = producto.FechaIngreso,
                            FechaVencimiento = producto.FechaVencimiento,
                            Cantidad = producto.Cantidad
                        };
                        _context.ProductosBodega.Add(nuevoProducto);
                    }
                }

                // Actualizar la bodega
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
            catch (Exception ex)
            {
                // Manejar excepciones generales
                ModelState.AddModelError("", $"Se produjo un error: {ex.Message}");
                return View(bodegas);
            }

            return RedirectToAction(nameof(Index));
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
