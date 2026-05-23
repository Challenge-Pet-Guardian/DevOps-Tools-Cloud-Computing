using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetGuardian.Domain.Entities;

namespace PetGuardian.Infrastructure.Persistence.Configurations;

public sealed class StatusConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        builder.ToTable("PG_STATUS");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnName("ID_STATUS");
        builder.Property(s => s.NomeStatus).HasColumnName("NOME_STATUS").HasMaxLength(15).IsRequired();
    }
}