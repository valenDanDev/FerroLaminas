using Microsoft.EntityFrameworkCore;
using API_FerroLaminas.Models;
using System.Data;

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
        public DbSet<Calibre> Calibres { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Usuario> Usuarios{ get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Material> Materiales { get; set; }
        public DbSet<Cotizacion> Cotizaciones { get; set; }
        public DbSet<OrdenDeTrabajo> OrdenesDeTrabajo { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<EstadoOrdenTrabajo> EstadosOrdenTrabajo { get; set; }

        public DbSet<Seguimiento> Seguimientos { get; set; }

        // Agrega DbSet para otras entidades aquí

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cotizacion>()
                .HasOne(c => c.Cliente)
                .WithMany(c => c.Cotizaciones)
                .HasForeignKey(c => c.ClienteId);

            modelBuilder.Entity<Cotizacion>()
                .HasOne(c => c.Material)
                .WithMany(m => m.Cotizaciones)
                .HasForeignKey(c => c.MaterialId);

            modelBuilder.Entity<Cotizacion>()
                .HasOne(c => c.Servicio)
                .WithMany(s => s.Cotizaciones)
                .HasForeignKey(c => c.ServicioId); // Corrección

            modelBuilder.Entity<Calibre>()
                .Property(c => c.PrecioPorKilo)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Cotizacion>()
                .HasOne(c => c.Proyecto)
                .WithMany()
                .HasForeignKey(c => c.ProyectoId); // Corrección

            modelBuilder.Entity<Cotizacion>()
                .Property(c => c.PrecioTotal)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Material>()
                .Property(m => m.PrecioPorKilo)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Material>()
                .Property(m => m.StockKilos)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Cotizacion>()
                .Property(c => c.PesoLamina)
                .HasColumnType("decimal(18, 2)");


            modelBuilder.Entity<Servicio>()
                .Property(s => s.PrecioPorKilo)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Cotizacion>()
                 .HasOne(c => c.OrdenDeTrabajo)  // Cotizacion tiene una OrdenDeTrabajo
                 .WithOne(o => o.Cotizacion)     // OrdenDeTrabajo tiene una Cotizacion
                 .HasForeignKey<OrdenDeTrabajo>(o => o.CotizacionId);  // Clave foránea en OrdenDeTrabajo

            modelBuilder.Entity<Ubicacion>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.RolId)
                .IsRequired(); // Opcional, dependiendo de tu caso

            modelBuilder.Entity<Seguimiento>()
                .Property(s => s.Avance)
                .HasColumnType("decimal(3, 2)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
