using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Datos.Data.Config
{
    public class OrdenDeProduccionConfiguration : IEntityTypeConfiguration<OrdenDeProduccion>
    {
        public void Configure(EntityTypeBuilder<OrdenDeProduccion> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Numero).IsRequired();
            builder.Property(p => p.FechaInicio).IsRequired();
            builder.Property(p => p.FechaFin).IsRequired();
            builder.HasOne(b => b.Color).WithMany()
                .HasForeignKey(op => op.ColorId);
            builder.HasOne(b => b.Modelo).WithMany()
                .HasForeignKey(op => op.ModeloId);
        }
    }
}