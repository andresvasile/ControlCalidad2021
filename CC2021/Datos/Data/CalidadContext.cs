using System.Reflection;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;

namespace Datos.Data
{
    public class CalidadContext : DbContext
    {
        public CalidadContext(DbContextOptions<CalidadContext> options ) :
            base(options)
        {
            
        }
        public DbSet<Color> Colores { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<OrdenDeProduccion> Ordenes{ get; set; }
        public DbSet<LineaDeTrabajo> Lineas{ get; set; }
        public DbSet<Defecto> Defectos { get; set; }
        public DbSet<Usuario> Usuarios{ get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<HorarioTrabajo> HorariosDeTrabajo{ get; set; }
        public DbSet<Primera> Primeras{ get; set; }
        public DbSet<Hallazgo> Hallazgos{ get; set; }
        public DbSet<Hermanado> Hermanados{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }

}