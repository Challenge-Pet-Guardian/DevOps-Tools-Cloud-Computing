using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetGuardian.Domain.Entities;
using PetGuardian.Domain.Enums;

namespace PetGuardian.Infrastructure.Persistence.Configurations;

public sealed class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("PG_PETS");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("ID_PET");

        builder.Property(p => p.Nome).HasColumnName("NOME").HasMaxLength(30).IsRequired();
        builder.Property(p => p.Idade).HasColumnName("IDADE").HasColumnType("NUMBER(2)").IsRequired();

        // CHECK: sexo IN ('M','F')
        builder.Property(p => p.Sexo)
            .HasColumnName("SEXO")
            .HasMaxLength(1)
            .HasConversion(
                v => v == SexoPet.Macho ? "M" : "F",
                v => v == "M" ? SexoPet.Macho : SexoPet.Femea)
            .IsRequired();

        // CHECK: porte IN ('GRANDE','MEDIO','PEQUENO')
        builder.Property(p => p.Porte)
            .HasColumnName("PORTE")
            .HasMaxLength(10)
            .HasConversion(
                v => v.ToString().ToUpperInvariant(),
                v => Enum.Parse<PortePet>(v, true))
            .IsRequired();

        // CHECK: castrado IN ('0','1') — CHAR(1)
        builder.Property(p => p.Castrado)
            .HasColumnName("CASTRADO")
            .HasMaxLength(1)
            .HasConversion(
                v => v ? "1" : "0",
                v => v == "1")
            .IsRequired();

        builder.Property(p => p.RacaId).HasColumnName("ID_RACA").IsRequired();
        builder.HasOne(p => p.Raca)
            .WithMany(r => r.Pets)
            .HasForeignKey(p => p.RacaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Atendimentos)
            .WithOne(a => a.Pet)
            .HasForeignKey(a => a.PetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Tarefas)
            .WithOne(t => t.Pet)
            .HasForeignKey(t => t.PetId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}