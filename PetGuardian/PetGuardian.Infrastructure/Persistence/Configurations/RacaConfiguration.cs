using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetGuardian.Domain.Entities;

namespace PetGuardian.Infrastructure.Persistence.Configurations;

public sealed class RacaConfiguration : IEntityTypeConfiguration<Raca>
{
    public void Configure(EntityTypeBuilder<Raca> builder)
    {
        builder.ToTable("PG_RACAS");
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .HasColumnName("ID_RACA");

        builder.Property(r => r.NomeRaca)
            .HasColumnName("NOME_RACA")
            .HasMaxLength(30)
            .IsRequired();
    }
}
