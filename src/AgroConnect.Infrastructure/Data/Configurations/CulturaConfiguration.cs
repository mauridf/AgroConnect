using AgroConnect.Domain.Entities;
using AgroConnect.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroConnect.Infrastructure.Data.Configurations
{
    public class CulturaConfiguration : IEntityTypeConfiguration<Cultura>
    {
        public void Configure(EntityTypeBuilder<Cultura> builder)
        {
            builder.ToTable("Culturas");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Categoria)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(c => c.TempoColheita)
                .HasMaxLength(50);

            builder.Property(c => c.ExigenciaClimatica)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(c => c.Detalhes)
                .HasMaxLength(500);

            builder.Property(c => c.DataCriacao)
                .IsRequired();

            builder.Property(c => c.DataAtualizacao);
        }
    }
}