using ifxNetworks.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ifxNetworks.Infrastructure.Data.Configurations
{
    public class EntityConfiguration : IEntityTypeConfiguration<Entity>
    {
        public void Configure(EntityTypeBuilder<Entity> builder)
        {
            // Nombre de la tabla y esquema
            builder.ToTable("Entity", "dbo");

            // Definir la clave primaria
            builder.HasKey(e => e.Id);

            // Configuración de las propiedades
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.IsActive)
            .IsRequired();

            // Configurar la relación con la entidad 'Employee'
            builder.HasMany(e => e.Employees)
                .WithOne(e => e.Entity)
                .HasForeignKey(e => e.EntityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
