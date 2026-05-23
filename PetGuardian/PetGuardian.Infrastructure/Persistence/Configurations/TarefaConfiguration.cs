using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetGuardian.Domain.Entities;

namespace PetGuardian.Infrastructure.Persistence.Configurations;

public sealed class TarefaConfiguration : IEntityTypeConfiguration<Tarefa>
{
    public void Configure(EntityTypeBuilder<Tarefa> builder)
    {
        builder.ToTable("PG_TAREFAS");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).HasColumnName("ID_TAREFA");

        builder.Property(t => t.Titulo).HasColumnName("TITULO").HasMaxLength(30).IsRequired();
        builder.Property(t => t.PontosTarefa).HasColumnName("PONTOS_TAREFA").HasColumnType("NUMBER(3)").IsRequired();
        builder.Property(t => t.Descricao).HasColumnName("DESCRICAO").HasMaxLength(200).IsRequired();
        builder.Property(t => t.Criacao).HasColumnName("CRIACAO").IsRequired();
        builder.Property(t => t.Prazo).HasColumnName("PRAZO").IsRequired();
        builder.Property(t => t.Conclusao).HasColumnName("CONCLUSAO").IsRequired(false);

        // Nullable — usuário pode ser atribuído depois
        builder.Property(t => t.UsuarioId).HasColumnName("ID_USUARIO").IsRequired(false);
        builder.HasIndex(t => t.UsuarioId);

        builder.Property(t => t.PetId).HasColumnName("ID_PET").IsRequired();
        builder.HasOne(t => t.Pet)
            .WithMany(p => p.Tarefas)
            .HasForeignKey(t => t.PetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(t => t.StatusId).HasColumnName("ID_STATUS").IsRequired();
        builder.HasOne(t => t.Status)
            .WithMany(s => s.Tarefas)
            .HasForeignKey(t => t.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(t => t.VeterinarioId).HasColumnName("ID_VETERINARIO").IsRequired();
    }
}
