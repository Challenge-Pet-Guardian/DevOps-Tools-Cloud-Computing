using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetGuardian.Domain.Entities;

namespace PetGuardian.Infrastructure.Persistence.Configurations;

public sealed class AtendimentoConfiguration : IEntityTypeConfiguration<Atendimento>
{
    public void Configure(EntityTypeBuilder<Atendimento> builder)
    {
        builder.ToTable("PG_ATENDIMENTOS");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasColumnName("ID_ATENDIMENTO");

        builder.Property(a => a.Data).HasColumnName("DATA").IsRequired();
        builder.Property(a => a.Anotacoes).HasColumnName("ANOTACOES").HasMaxLength(300).IsRequired();
        builder.Property(a => a.Valor).HasColumnName("VALOR").HasColumnType("NUMBER(10,2)").IsRequired();

        builder.Property(a => a.PetId).HasColumnName("ID_PET").IsRequired();
        builder.HasOne(a => a.Pet)
            .WithMany(p => p.Atendimentos)
            .HasForeignKey(a => a.PetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(a => a.VeterinarioId).HasColumnName("ID_VETERINARIO").IsRequired();

        builder.Property(a => a.TipoAtendId).HasColumnName("ID_TIPO_ATEND").IsRequired();
        builder.HasOne(a => a.TipoAtend)
            .WithMany(t => t.Atendimentos)
            .HasForeignKey(a => a.TipoAtendId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(a => a.StatusId).HasColumnName("ID_STATUS").IsRequired();
        builder.HasOne(a => a.Status)
            .WithMany(s => s.Atendimentos)
            .HasForeignKey(a => a.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}