using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetGuardian.Domain.Entities;

namespace PetGuardian.Infrastructure.Persistence.Configurations;

public sealed class ClinicaConfiguration : IEntityTypeConfiguration<Clinica>
{
    public void Configure(EntityTypeBuilder<Clinica> builder)
    {
        builder.ToTable("PG_CLINICAS");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("ID_CLINICA");
        builder.Property(c => c.Nome).HasColumnName("NOME").HasMaxLength(30).IsRequired();

        builder.Property(c => c.TelefoneId).HasColumnName("ID_TELEFONE").IsRequired();
        builder.HasOne(c => c.Telefone)
            .WithOne()
            .HasForeignKey<Clinica>(c => c.TelefoneId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasIndex(c => c.TelefoneId).IsUnique();

        builder.Property(c => c.EnderecoId).HasColumnName("ID_ENDERECO").IsRequired();
        builder.HasOne(c => c.Endereco)
            .WithOne()
            .HasForeignKey<Clinica>(c => c.EnderecoId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasIndex(c => c.EnderecoId).IsUnique();

        builder.HasMany(c => c.Veterinarios)
            .WithOne(v => v.Clinica)
            .HasForeignKey(v => v.ClinicaId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}