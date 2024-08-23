using ifxNetworks.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ifxNetworks.Infrastructure.Data.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole", "Security");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.IdUser)
               .IsRequired();

            builder.Property(e => e.IdRole)
               .IsRequired();

            builder.HasOne(d => d.Users)
                   .WithMany(p => p.UserRoles)
                   .HasForeignKey(d => d.IdUser)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK__UserRole__IdUser__6477ECF3");

            builder.HasOne(d => d.Roles)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_RoleXUser_IdRol");
        }
    }
}
