using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetGuardian.Domain.Entities;

namespace PetGuardian.Infrastructure.Persistence.Configurations;

public sealed class EstadoConfiguration : IEntityTypeConfiguration<Estado>
{
    public void Configure(EntityTypeBuilder<Estado> builder)
    {
        builder.ToTable("PG_ESTADOS");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("ID_ESTADO");

        builder.Property(e => e.NomeEstado)
            .HasColumnName("NOME_ESTADO")
            .HasMaxLength(30)
            .IsRequired();
    }
}
