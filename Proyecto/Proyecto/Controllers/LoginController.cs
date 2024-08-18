using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Data;
using Proyecto.Models;
using System.Net;
using Microsoft.EntityFrameworkCore;


namespace Proyecto.Controllers
{
    public class LoginController : Controller
    {
        private AppDbContext _db;
        // GET: Login
        public LoginController(AppDbContext db)
        {
            _db = db;
        }
        public ActionResult Index()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }
        public IActionResult RevisarLogin(string Usuario, string Clave)
        {

            List<Usuarios> Login = _db.Usuarios.Where(tp => tp.Usuario == Usuario && tp.Clave == Clave).ToList();

            if (Login != null)
            {
                foreach (var item in Login)
                {
                    HttpContext.Session.SetString("Login", "True");
                    HttpContext.Session.SetInt32("IdUsuario", item.IdUsuario);
                    HttpContext.Session.SetString("Nombre", item.Nombre);
                    HttpContext.Session.SetString("Usuario", item.Usuario);
                    HttpContext.Session.SetString("Puesto", item.Puesto);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login");
            }

        }
        
    }
}
