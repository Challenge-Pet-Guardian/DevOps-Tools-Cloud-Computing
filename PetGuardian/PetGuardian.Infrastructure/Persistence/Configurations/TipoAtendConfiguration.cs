using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetGuardian.Domain.Entities;

namespace PetGuardian.Infrastructure.Persistence.Configurations;

public sealed class TipoAtendConfiguration : IEntityTypeConfiguration<TipoAtend>
{
    public void Configure(EntityTypeBuilder<TipoAtend> builder)
    {
        builder.ToTable("PG_TIPO_ATEND");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnName("ID_TIPO_ATEND");

        builder.Property(t => t.Tipo)
            .HasColumnName("TIPO")
            .HasMaxLength(30)
            .IsRequired();
    }
}
