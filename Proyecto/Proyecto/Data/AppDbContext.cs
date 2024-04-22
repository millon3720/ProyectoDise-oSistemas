using Microsoft.EntityFrameworkCore;
using Proyecto.Models;

namespace Proyecto.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Proyecto.Models.Bodegas> Bodegas { get; set; } = default!;
        public DbSet<Proyecto.Models.Canton> Canton { get; set; } = default!;
        public DbSet<Proyecto.Models.Distrito> Distrito { get; set; } = default!;
        public DbSet<Proyecto.Models.Facturas> Facturas { get; set; } = default!;
        public DbSet<Proyecto.Models.Pedidos> Pedidos { get; set; } = default!;
        public DbSet<Proyecto.Models.ProductoFactura> ProductoFactura { get; set; } = default!;
        public DbSet<Proyecto.Models.ProductosPedidos> ProductosPedidos { get; set; } = default!;
        public DbSet<Proyecto.Models.Proveedores> Proveedores { get; set; } = default!;
        public DbSet<Proyecto.Models.Provincia> Provincia { get; set; } = default!;
        public DbSet<Proyecto.Models.Usuarios> Usuarios { get; set; } = default!;
        public DbSet<Proyecto.Models.ProductosBodega> ProductosBodega { get; set; } = default!;
        public DbSet<Proyecto.Models.ProductoProveedores> ProductoProveedores { get; set; } = default!;


    }
}
