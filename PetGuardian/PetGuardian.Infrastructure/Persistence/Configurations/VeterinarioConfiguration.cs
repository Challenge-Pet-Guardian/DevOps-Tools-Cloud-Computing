using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetGuardian.Domain.Entities;

namespace PetGuardian.Infrastructure.Persistence.Configurations;

public sealed class VeterinarioConfiguration : IEntityTypeConfiguration<Veterinario>
{
    public void Configure(EntityTypeBuilder<Veterinario> builder)
    {
        builder.ToTable("PG_VETERINARIOS");
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id).HasColumnName("ID_VETERINARIO");

        builder.Property(v => v.Nome).HasColumnName("NOME").HasMaxLength(100).IsRequired();
        builder.Property(v => v.Email).HasColumnName("EMAIL").HasMaxLength(50).IsRequired();
        builder.HasIndex(v => v.Email).IsUnique();
        builder.Property(v => v.Senha).HasColumnName("SENHA").HasMaxLength(20).IsRequired();

        builder.Property(v => v.TelefoneId).HasColumnName("ID_TELEFONE").IsRequired();
        builder.HasOne(v => v.Telefone)
            .WithOne()
            .HasForeignKey<Veterinario>(v => v.TelefoneId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasIndex(v => v.TelefoneId).IsUnique();

        // Nullable
        builder.Property(v => v.ClinicaId).HasColumnName("ID_CLINICA").IsRequired(false);

        builder.HasMany(v => v.Atendimentos)
            .WithOne(a => a.Veterinario)
            .HasForeignKey(a => a.VeterinarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(v => v.Tarefas)
            .WithOne(t => t.Veterinario)
            .HasForeignKey(t => t.VeterinarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}