using System.Text;
using AgroConnect.Infrastructure;
using AgroConnect.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;

namespace AgroConnect.API.Extensions
{
    public static class ApiConfigurations
    {
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            // Configurações básicas
            builder.Services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AgroConnect API",
                    Version = "v1",
                    Description = "API para cadastro de produtores rurais, fazendas e culturas"
                });

                // Configuração para JWT
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            // Infraestrutura (DbContext, Repositórios, etc)
            builder.Services.AddInfrastructure(builder.Configuration);

            // Outros serviços da aplicação
            builder.Services.AddApplicationServices();

            // Configuração de CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            // Configuração do JWT
            builder.ConfigureJwt();

            builder.Services.AddHealthChecks()
                .AddDbContextCheck<AgroConnectDbContext>();

            return builder;
        }

        // Configuração do JWT
        public static void ConfigureJwt(this WebApplicationBuilder builder)
        {
            var jwtSettings = builder.Configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseDeveloperExceptionPage();

            // Middleware de erro personalizado
            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex}");
                    throw;
                }
            });

            if (app.Environment.IsDevelopment())
            {
                app.MapGet("/", () => Results.Redirect("/swagger"));

                using (var scope = app.Services.CreateScope())
                {
                    var dbContext = scope.ServiceProvider
                        .GetRequiredService<AgroConnectDbContext>();
                    dbContext.Database.Migrate();
                }
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AgroConnect API v1");
                c.RoutePrefix = "swagger";
                c.ConfigObject.DisplayRequestDuration = true;
            });

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapHealthChecks("/health");

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/error");
            }

            return app;
        }
    }
}