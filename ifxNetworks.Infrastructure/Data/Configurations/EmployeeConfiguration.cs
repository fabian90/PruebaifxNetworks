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
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            // Nombre de la tabla y esquema
            builder.ToTable("Employee", "dbo");

            // Definir la clave primaria
            builder.HasKey(e => e.Id);

            // Configuración de las propiedades
            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.DateOfBirth);

            builder.Property(e => e.Position)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);

            builder.Property(e => e.EntityId)
                .IsRequired();
            builder.Property(e => e.IsActive)
            .IsRequired();

            // Configurar la relación con la entidad 'Entity'
            builder.HasOne(e => e.Entity)
                .WithMany(e => e.Employees)
                .HasForeignKey(e => e.EntityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
