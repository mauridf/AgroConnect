using AgroConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroConnect.Infrastructure.Data.Configurations
{
    public class FazendaConfiguration : IEntityTypeConfiguration<Fazenda>
    {
        public void Configure(EntityTypeBuilder<Fazenda> builder)
        {
            builder.ToTable("Fazendas");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(f => f.CNPJ)
                .HasMaxLength(14);

            builder.HasIndex(f => f.CNPJ)
                .IsUnique();

            builder.Property(f => f.AreaTotalHectares)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(f => f.AreaAgricultavelHectares)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(f => f.AreaVegetacaoHectares)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(f => f.AreaConstruidaHectares)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.OwnsOne(f => f.Endereco, endereco =>
            {
                endereco.Property(e => e.Logradouro)
                    .HasColumnName("EnderecoFazenda")
                    .IsRequired()
                    .HasMaxLength(200);

                endereco.Property(e => e.Cidade)
                    .IsRequired()
                    .HasMaxLength(100);

                endereco.Property(e => e.UF)
                    .HasMaxLength(2)
                    .IsRequired();

                endereco.Property(e => e.CEP)
                    .HasMaxLength(10);

                endereco.HasIndex(e => e.UF);
            });

            builder.Property(f => f.DataCriacao)
                .IsRequired();

            builder.Property(f => f.DataAtualizacao);

            builder.HasOne(f => f.Produtor)
                .WithMany(p => p.Fazendas)
                .HasForeignKey(f => f.ProdutorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}