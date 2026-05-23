using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetGuardian.Domain.Entities;

namespace PetGuardian.Infrastructure.Persistence.Configurations;

public sealed class BairroConfiguration : IEntityTypeConfiguration<Bairro>
{
    public void Configure(EntityTypeBuilder<Bairro> builder)
    {
        builder.ToTable("PG_BAIRROS");
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasColumnName("ID_BAIRRO");

        builder.Property(b => b.NomeBairro)
            .HasColumnName("NOME_BAIRRO")
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(b => b.CidadeId)
            .HasColumnName("ID_CIDADE")
            .IsRequired();

        builder.HasOne(b => b.Cidade)
            .WithMany(c => c.Bairros)
            .HasForeignKey(b => b.CidadeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
