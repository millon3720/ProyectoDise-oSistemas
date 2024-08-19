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
    public class UsuariosController : Controller
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }


        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return View(usuarios);
        }



        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.IdUsuario == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }
        // GET: Usuarios/Index
        public async Task<IActionResult> Usuario()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return View(usuarios);
        }




        // GET: Usuarios/Create

        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario,Cedula,Nombre,Apellidos,Telefono,Correo,Usuario,Clave,Puesto,IdBodega")] Usuarios usuarios)
        {

            _context.Add(usuarios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }





        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null || _context.Usuarios == null)
            //{
            //    return NotFound();
            //}

            //var usuarios = await _context.Usuarios.FindAsync(id);
            //if (usuarios == null)
            //{
            //    return NotFound();
            //}
            //ViewData["IdBodegas"] = new SelectList(_context.Bodegas, "IdBodegas", "DireccionExacta", usuarios.IdBodegas);
            //return View(usuarios);
            return View("Edit");
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,Cedula,Nombre,Apellidos,Telefono,Puesto,IdBodegas")] Usuarios usuarios)
        {
            if (id != usuarios.IdUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuariosExists(usuarios.IdUsuario))
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
            ViewData["IdBodegas"] = new SelectList(_context.Bodegas, "IdBodegas", "DireccionExacta", usuarios.IdBodega);
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            //    if (id == null || _context.Usuarios == null)
            //    {
            //        return NotFound();
            //    }

            //    var usuarios = await _context.Usuarios
            //        .Include(u => u.Bodegas)
            //        .FirstOrDefaultAsync(m => m.IdUsuario == id);
            //    if (usuarios == null)
            //    {
            //        return NotFound();
            //    }

            return View("delete");
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'AppDbContext.Usuarios'  is null.");
            }
            var usuarios = await _context.Usuarios.FindAsync(id);
            if (usuarios != null)
            {
                _context.Usuarios.Remove(usuarios);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuariosExists(int id)
        {
            return (_context.Usuarios?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        }
    }
}
