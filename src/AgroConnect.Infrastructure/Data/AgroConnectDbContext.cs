using AgroConnect.Domain.Entities;
using AgroConnect.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection;

namespace AgroConnect.Infrastructure.Data
{
    public class AgroConnectDbContext : DbContext
    {
        public AgroConnectDbContext(DbContextOptions<AgroConnectDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<ProdutorRural> ProdutoresRurais { get; set; }
        public DbSet<Fazenda> Fazendas { get; set; }
        public DbSet<Cultura> Culturas { get; set; }
        public DbSet<FazendaCultura> FazendaCulturas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplicar todas as configurações que implementam IEntityTypeConfiguration<T>
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Configuração para o PostgreSQL
            if (Database.IsNpgsql())
            {
                // Configurar para usar o fuso horário UTC por padrão
                modelBuilder.HasPostgresExtension("uuid-ossp")
                           .HasPostgresExtension("pgcrypto");

                // Configurar todas as propriedades DateTime para usar UTC
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    foreach (var property in entityType.GetProperties())
                    {
                        if (property.ClrType == typeof(DateTime))
                        {
                            property.SetValueConverter(
                                new ValueConverter<DateTime, DateTime>(
                                    v => v.ToUniversalTime(),
                                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                        }

                        if (property.ClrType == typeof(DateTime?))
                        {
                            property.SetValueConverter(
                                new ValueConverter<DateTime?, DateTime?>(
                                    v => v.HasValue ? v.Value.ToUniversalTime() : v,
                                    v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v));
                        }
                    }
                }
            }
        }
    }
}