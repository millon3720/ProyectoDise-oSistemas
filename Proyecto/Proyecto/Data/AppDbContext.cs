using Microsoft.EntityFrameworkCore;
using Proyecto.Models;

namespace Proyecto.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Proyecto.Models.Productos> Productos { get; set; }
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración para Facturas
            modelBuilder.Entity<Facturas>()
                .Property(f => f.MontoDeuda)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Facturas>()
                .Property(f => f.MontoTotal)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Facturas>()
                .HasOne(f => f.Proveedor)
                .WithMany(p => p.Facturas)
                .HasForeignKey(f => f.IdProveedor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Facturas>()
                .HasOne(f => f.Usuario)
                .WithMany(u => u.Facturas)
                .HasForeignKey(f => f.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración para ProductoFactura
            modelBuilder.Entity<ProductoFactura>()
                .Property(pf => pf.Total)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ProductoFactura>()
                .HasOne(pf => pf.Factura)
                .WithMany(f => f.ProductoFactura)
                .HasForeignKey(pf => pf.IdFactura)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductoFactura>()
                .HasOne(pf => pf.Producto)
                .WithMany(p => p.ProductoFactura)
                .HasForeignKey(pf => pf.IdProducto)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración para Proveedores
            modelBuilder.Entity<Proveedores>()
                .HasOne(p => p.Canton)
                .WithMany(c => c.Proveedores)
                .HasForeignKey(p => p.IdCanton)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Proveedores>()
                .HasOne(p => p.Distrito)
                .WithMany(d => d.Proveedores)
                .HasForeignKey(p => p.IdDistrito)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Proveedores>()
                .HasOne(p => p.Provincia)
                .WithMany(p => p.Proveedores)
                .HasForeignKey(p => p.IdProvincia)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración para Usuarios
            modelBuilder.Entity<Usuarios>()
                .HasOne(u => u.Bodegas)
                .WithMany(b => b.Usuarios)
                .HasForeignKey(u => u.IdBodega)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración para Bodegas
            modelBuilder.Entity<Bodegas>()
                .HasOne(b => b.Canton)
                .WithMany(c => c.Bodegas)
                .HasForeignKey(b => b.IdCanton)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bodegas>()
                .HasOne(b => b.Distrito)
                .WithMany(d => d.Bodegas)
                .HasForeignKey(b => b.IdDistrito)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bodegas>()
                .HasOne(b => b.Provincia)
                .WithMany(p => p.Bodegas)
                .HasForeignKey(b => b.IdProvincia)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración para Productos
            modelBuilder.Entity<Productos>()
                .Property(p => p.Precio)
                .HasColumnType("decimal(18,2)");

            // Configuración para ProductoProveedores
            modelBuilder.Entity<ProductoProveedores>()
                .Property(pp => pp.Precio)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }


    }

}
