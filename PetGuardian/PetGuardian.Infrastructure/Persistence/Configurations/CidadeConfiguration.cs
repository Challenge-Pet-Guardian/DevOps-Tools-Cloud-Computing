using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetGuardian.Domain.Entities;

namespace PetGuardian.Infrastructure.Persistence.Configurations;

public sealed class CidadeConfiguration : IEntityTypeConfiguration<Cidade>
{
    public void Configure(EntityTypeBuilder<Cidade> builder)
    {
        builder.ToTable("PG_CIDADES");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("ID_CIDADE");

        builder.Property(c => c.NomeCidade)
            .HasColumnName("NOME_CIDADE")
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(c => c.EstadoId)
            .HasColumnName("ID_ESTADO")
            .IsRequired();

        builder.HasOne(c => c.Estado)
            .WithMany(e => e.Cidades)
            .HasForeignKey(c => c.EstadoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
