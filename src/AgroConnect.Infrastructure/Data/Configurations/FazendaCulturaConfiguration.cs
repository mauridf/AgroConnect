using AgroConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroConnect.Infrastructure.Data.Configurations
{
    public class FazendaCulturaConfiguration : IEntityTypeConfiguration<FazendaCultura>
    {
        public void Configure(EntityTypeBuilder<FazendaCultura> builder)
        {
            builder.ToTable("FazendaCulturas");

            builder.HasKey(fc => fc.Id);

            builder.Property(fc => fc.AreaUtilizadaHectares)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(fc => fc.DataCriacao)
                .IsRequired();

            builder.Property(fc => fc.DataAtualizacao);

            builder.HasOne(fc => fc.Fazenda)
                .WithMany(f => f.FazendaCulturas)
                .HasForeignKey(fc => fc.FazendaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(fc => fc.Cultura)
                .WithMany(c => c.FazendaCulturas)
                .HasForeignKey(fc => fc.CulturaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Índice composto para evitar duplicatas
            builder.HasIndex(fc => new { fc.FazendaId, fc.CulturaId })
                .IsUnique();
        }
    }
}