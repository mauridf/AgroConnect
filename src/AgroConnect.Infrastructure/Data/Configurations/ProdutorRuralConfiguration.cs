using AgroConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroConnect.Infrastructure.Data.Configurations
{
    public class ProdutorRuralConfiguration : IEntityTypeConfiguration<ProdutorRural>
    {
        public void Configure(EntityTypeBuilder<ProdutorRural> builder)
        {
            builder.ToTable("ProdutoresRurais");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.CPF)
                .HasMaxLength(11)
                .IsRequired();

            builder.HasIndex(p => p.CPF)
                .IsUnique();

            builder.Property(p => p.Email)
                .HasMaxLength(100);

            builder.Property(p => p.Telefone)
                .HasMaxLength(20)
                .IsRequired();

            builder.OwnsOne(p => p.Endereco, endereco =>
            {
                endereco.Property(e => e.Logradouro)
                    .HasColumnName("EnderecoCompleto")
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

            builder.Property(p => p.DataCriacao)
                .IsRequired();

            builder.Property(p => p.DataAtualizacao);

            builder.HasMany(p => p.Fazendas)
                .WithOne(f => f.Produtor)
                .HasForeignKey(f => f.ProdutorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}