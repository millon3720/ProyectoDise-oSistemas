namespace ProyectoDiseñoSistemas.Data
{
    using Microsoft.EntityFrameworkCore;
    using ProyectoDiseñoSistemas.Models;
    using System.Collections.Generic;
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Canton> Cantones { get; set; }
        public DbSet<Distrito> Distritos { get; set; }
        public DbSet<Bodega> Bodegas { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Pedidos> Pedidos { get; set; }
        public DbSet<ProductoPedido> ProductosPedidos { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<ProductoFactura> ProductosFacturas { get; set; }

    }

}
