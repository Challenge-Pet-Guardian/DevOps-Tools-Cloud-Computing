using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetGuardian.Domain.Entities;

namespace PetGuardian.Infrastructure.Persistence.Configurations;

public sealed class TelefoneConfiguration : IEntityTypeConfiguration<Telefone>
{
    public void Configure(EntityTypeBuilder<Telefone> builder)
    {
        builder.ToTable("PG_TELEFONES");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnName("ID_TELEFONE");

        builder.Property(t => t.NumDdd)
            .HasColumnName("NUM_DDD")
            .HasMaxLength(2)
            .IsRequired();

        builder.Property(t => t.NumTel)
            .HasColumnName("NUM_TEL")
            .HasMaxLength(9)
            .IsRequired();
    }
}
