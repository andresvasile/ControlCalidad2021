using System;
using Dominio.Entities;
using Dominio.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Datos.Data.Config
{
    public class DefectosConfiguration : IEntityTypeConfiguration<Defecto>
    {
        public void Configure(EntityTypeBuilder<Defecto> builder)
        {
            builder.Property(t => t.TipoDefecto)
                .HasConversion(
                    p => p.ToString(),
                    p => (TipoDefecto)Enum.Parse(typeof(TipoDefecto), p)
                    );
        }
    }
}