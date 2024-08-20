using System;
using System.Collections.Generic;
using System.Globalization;
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
        public async Task<IActionResult> Create()
        {
          
            var provincias = await _context.Provincia.ToListAsync();
            var cantones = await _context.Canton.ToListAsync();
            var distritos = await _context.Distrito.ToListAsync();
            var Productos = await _context.Productos.ToListAsync();
            var viewModel = new BodegasModelo
            {
                Provincias = provincias.OrderBy(p => p.Nombre),
                Cantones = cantones.OrderBy(p => p.Nombre),
                Distritos = distritos.OrderBy(p => p.Nombre),
                Productos = Productos.OrderBy(p => p.Nombre)

            };

            return View(viewModel);
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
                _context.Update(bodegas);
                await _context.SaveChangesAsync();

                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Eliminar los productos existentes para la bodega
                        var productosExistentes = _context.ProductosBodega.Where(pb => pb.IdBodega == bodegas.IdBodegas);
                        _context.ProductosBodega.RemoveRange(productosExistentes);
                        await _context.SaveChangesAsync(); // Guardar cambios antes de agregar nuevos productos

                        // Deserializar los datos de productos
                        var productosDataList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(productosData);

                        var productosBodega = new List<ProductosBodega>();

                        // Convertir los datos deserializados en objetos ProductosBodega
                        foreach (var item in productosDataList)
                        {
                            if (int.TryParse(item["IdProducto"], out int idProducto))
                            {
                                var productoBodega = new ProductosBodega
                                {
                                    IdBodega = bodegas.IdBodegas,
                                    IdProducto = idProducto,
                                    FechaIngreso = DateTime.ParseExact(item["FechaIngreso"], "d/M/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                                    FechaVencimiento = DateTime.ParseExact(item["FechaVencimiento"], "d/M/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
                                };

                                productosBodega.Add(productoBodega);
                            }
                        }

                        // Agregar nuevos productos a la bodega
                        _context.ProductosBodega.AddRange(productosBodega);

                        // Guardar todos los cambios en la base de datos
                        await _context.SaveChangesAsync();

                        // Confirmar la transacción
                        await transaction.CommitAsync();
                    }
                    catch
                    {
                        // Deshacer la transacción en caso de error
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
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
