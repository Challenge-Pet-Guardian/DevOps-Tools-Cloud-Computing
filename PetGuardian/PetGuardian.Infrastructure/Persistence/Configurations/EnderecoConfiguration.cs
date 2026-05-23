using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetGuardian.Domain.Entities;

namespace PetGuardian.Infrastructure.Persistence.Configurations;

public sealed class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.ToTable("PG_ENDERECOS");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("ID_ENDERECO");

        builder.Property(e => e.Cep).HasColumnName("CEP").HasMaxLength(8).IsRequired();
        builder.Property(e => e.Rua).HasColumnName("RUA").HasMaxLength(150).IsRequired();
        builder.Property(e => e.Numero).HasColumnName("NUMERO").HasMaxLength(5).IsRequired();

        builder.Property(e => e.BairroId).HasColumnName("ID_BAIRRO").IsRequired();
        builder.HasOne(e => e.Bairro)
            .WithMany(b => b.Enderecos)
            .HasForeignKey(e => e.BairroId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}