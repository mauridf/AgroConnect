using AgroConnect.Application.Interfaces;
using AgroConnect.Application.Services;
using AgroConnect.Infrastructure.Data.Repositories;
using AgroConnect.Infrastructure.Services;

namespace AgroConnect.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Registro dos serviços de aplicação
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IProdutorRuralService, ProdutorRuralService>();
            services.AddScoped<IFazendaService, FazendaService>();
            //services.AddScoped<ICulturaService, CulturaService>();
            //services.AddScoped<IDashboardService, DashboardService>();

            // Repositórios
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IProdutorRuralRepository, ProdutorRuralRepository>();
            services.AddScoped<IFazendaRepository, FazendaRepository>();

            // Serviços de infraestrutura
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            // AutoMapper
            services.AddAutoMapper(typeof(ApplicationProfile).Assembly);

            return services;
        }
    }
}