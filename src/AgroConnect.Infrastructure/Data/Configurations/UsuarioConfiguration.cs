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

            // Configuração básica
            builder.Property(u => u.NomeUsuario)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(u => u.NomeUsuario)
                .IsUnique();

            builder.Property(u => u.SenhaHash)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(u => u.TipoUsuario)
                .IsRequired()
                .HasConversion<string>();

            // Configuração do email - CORREÇÃO AQUI
            builder.Property(u => u.Email)
                .HasMaxLength(100);

            builder.HasIndex(u => u.Email)
                .IsUnique()
                .HasFilter("\"Email\" IS NOT NULL"); // Aspas duplas em vez de colchetes

            builder.Property(u => u.EmailConfirmado)
                .HasDefaultValue(false);

            // Configuração de tokens - CORREÇÕES AQUI
            builder.Property(u => u.EmailConfirmationToken)
                .HasMaxLength(100);

            builder.HasIndex(u => u.EmailConfirmationToken)
                .HasFilter("\"EmailConfirmationToken\" IS NOT NULL"); // Aspas duplas

            builder.Property(u => u.PasswordResetToken)
                .HasMaxLength(100);

            builder.HasIndex(u => u.PasswordResetToken)
                .HasFilter("\"PasswordResetToken\" IS NOT NULL"); // Aspas duplas

            builder.Property(u => u.PasswordResetTokenExpires);

            // Auditoria
            builder.Property(u => u.DataCriacao)
                .IsRequired();

            builder.Property(u => u.DataAtualizacao);
        }
    }
}