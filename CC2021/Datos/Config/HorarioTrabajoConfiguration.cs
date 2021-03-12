using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Datos.Data.Config
{
    public class HorarioTrabajoConfiguration : IEntityTypeConfiguration<HorarioTrabajo>
    {
        public void Configure(EntityTypeBuilder<HorarioTrabajo> builder)
        {
            builder.OwnsOne(h => h.Hermanado, he =>
            {
                he.WithOwner();
            });
            builder.OwnsOne(t => t.Turno, tu => tu.WithOwner());
            builder.OwnsOne(p => p.ParesPrimera, pp => pp.WithOwner());
            builder.OwnsMany(d => d.Hallazgos, h => h.WithOwner());
            builder.OwnsOne(o => o.OrdenDeProduccion, op => op.WithOwner());
        }
    }
}