using InventarioApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarioApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Asegurar que los nombres de las tablas coincidan con PostgreSQL (en minúsculas)
            modelBuilder.Entity<Producto>().ToTable("productos");
            modelBuilder.Entity<Categoria>().ToTable("categorias");
            modelBuilder.Entity<Proveedor>().ToTable("proveedores");

            base.OnModelCreating(modelBuilder);
        }
    }
}
