using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetGuardian.Domain.Entities;

namespace PetGuardian.Infrastructure.Persistence.Configurations;

public sealed class UsuarioEnderecoConfiguration : IEntityTypeConfiguration<UsuarioEndereco>
{
    public void Configure(EntityTypeBuilder<UsuarioEndereco> builder)
    {
        builder.ToTable("PG_USUARIO_ENDERECO");
        builder.HasKey(ue => new { ue.UsuarioId, ue.EnderecoId });

        builder.Property(ue => ue.UsuarioId).HasColumnName("ID_USUARIO").IsRequired();
        builder.Property(ue => ue.EnderecoId).HasColumnName("ID_ENDERECO").IsRequired();

        builder.HasOne(ue => ue.Usuario)
            .WithMany(u => u.Enderecos)
            .HasForeignKey(ue => ue.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ue => ue.Endereco)
            .WithMany()
            .HasForeignKey(ue => ue.EnderecoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}