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
            // Definir relaciones
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.categoriaid);

            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Proveedor)
                .WithMany(prov => prov.Productos)
                .HasForeignKey(p => p.proveedorid);

            // Mapear nombres de tablas a PostgreSQL en minúsculas
            modelBuilder.Entity<Producto>().ToTable("productos");
            modelBuilder.Entity<Categoria>().ToTable("categorias");
            modelBuilder.Entity<Proveedor>().ToTable("proveedores");

            base.OnModelCreating(modelBuilder);
        }
    }
}
