using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetGuardian.Domain.Entities;

namespace PetGuardian.Infrastructure.Persistence.Configurations;

public sealed class UsuarioPetConfiguration : IEntityTypeConfiguration<UsuarioPet>
{
    public void Configure(EntityTypeBuilder<UsuarioPet> builder)
    {
        builder.ToTable("PG_USUARIO_PET");
        builder.HasKey(up => new { up.UsuarioId, up.PetId });

        builder.Property(up => up.UsuarioId).HasColumnName("ID_USUARIO").IsRequired();
        builder.Property(up => up.PetId).HasColumnName("ID_PET").IsRequired();

        // CHECK: respon_princ IN ('0','1') — CHAR(1)
        builder.Property(up => up.ResponPrinc)
            .HasColumnName("RESPON_PRINC")
            .HasMaxLength(1)
            .HasConversion(
                v => v ? "1" : "0",
                v => v == "1")
            .IsRequired();

        builder.HasOne(up => up.Usuario)
            .WithMany(u => u.PetsVinculados)
            .HasForeignKey(up => up.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(up => up.Pet)
            .WithMany(p => p.UsuariosPet)
            .HasForeignKey(up => up.PetId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}