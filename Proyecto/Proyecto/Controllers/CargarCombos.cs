using Microsoft.AspNetCore.Mvc;
using Proyecto.Data;
using Microsoft.EntityFrameworkCore;

namespace Proyecto.Controllers
{
    public class CargarCombos : Controller
    {
        private readonly AppDbContext _context;
        public CargarCombos(AppDbContext context)
        {
            _context= context;
        }
        [HttpGet]
        public async Task<IActionResult> GetCantones(int provinciaId)
        {
            var cantones = await _context.Canton
                .Where(c => c.IdProvincia == provinciaId)
                .Select(c => new { idCanton = c.IdCanton, nombre = c.Nombre })
                .ToListAsync();

            return Json(cantones);
        }

        [HttpGet]
        public async Task<IActionResult> GetDistritos(int cantonId)
        {
            var distritos = await _context.Distrito
                .Where(d => d.IdCanton == cantonId)
                .Select(d => new { idDistrito = d.IdDistrito, nombre = d.Nombre })
                .ToListAsync();

            return Json(distritos);
        }
        [HttpGet]
        public async Task<IActionResult> GetProvincias()
        {
            var provincias = await _context.Provincia
                .Select(p => new { idProvincia = p.IdProvincia, nombre = p.Nombre }) // Asegúrate de que las propiedades sean correctas
                .ToListAsync();
            return Json(provincias);
        }

    }
}
