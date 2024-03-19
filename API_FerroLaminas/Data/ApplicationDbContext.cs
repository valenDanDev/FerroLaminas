using Microsoft.EntityFrameworkCore;
using API_FerroLaminas.Models;

namespace API_FerroLaminas.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Material> Materiales { get; set; }
        public DbSet<Cotizacion> Cotizaciones { get; set; }
        public DbSet<OrdenDeTrabajo> OrdenesDeTrabajo { get; set; }

        // Agrega DbSet para otras entidades aquí

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cotizacion>()
                .Property(c => c.PrecioTotal)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Material>()
                .Property(m => m.Precio)
                .HasColumnType("decimal(18, 2)");

            // Agrega configuraciones adicionales aquí si es necesario

            base.OnModelCreating(modelBuilder);
        }
    }
}
