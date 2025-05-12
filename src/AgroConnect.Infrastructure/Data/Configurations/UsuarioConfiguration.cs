using AgroConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroConnect.Infrastructure.Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.NomeUsuario)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.SenhaHash)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.TipoUsuario)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(u => u.DataCriacao)
                .IsRequired();

            builder.Property(u => u.DataAtualizacao);
        }
    }
}