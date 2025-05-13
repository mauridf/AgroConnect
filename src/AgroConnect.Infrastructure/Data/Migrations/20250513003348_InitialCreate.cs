using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgroConnect.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:pgcrypto", ",,")
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "Culturas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Categoria = table.Column<string>(type: "text", nullable: false),
                    TempoColheita = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ExigenciaClimatica = table.Column<string>(type: "text", nullable: false),
                    Detalhes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Culturas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoresRurais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CPF = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Telefone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    EnderecoCompleto = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Endereco_Cidade = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Endereco_UF = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    Endereco_CEP = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoresRurais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NomeUsuario = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SenhaHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    TipoUsuario = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EmailConfirmado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    PasswordResetToken = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PasswordResetTokenExpires = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EmailConfirmationToken = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fazendas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CNPJ = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: true),
                    EnderecoFazenda = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Endereco_Cidade = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Endereco_UF = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    Endereco_CEP = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    AreaTotalHectares = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    AreaAgricultavelHectares = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    AreaVegetacaoHectares = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    AreaConstruidaHectares = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fazendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fazendas_ProdutoresRurais_ProdutorId",
                        column: x => x.ProdutorId,
                        principalTable: "ProdutoresRurais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Revoked = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FazendaCulturas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FazendaId = table.Column<Guid>(type: "uuid", nullable: false),
                    CulturaId = table.Column<Guid>(type: "uuid", nullable: false),
                    AreaUtilizadaHectares = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FazendaCulturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FazendaCulturas_Culturas_CulturaId",
                        column: x => x.CulturaId,
                        principalTable: "Culturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FazendaCulturas_Fazendas_FazendaId",
                        column: x => x.FazendaId,
                        principalTable: "Fazendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Culturas_Nome",
                table: "Culturas",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FazendaCulturas_CulturaId",
                table: "FazendaCulturas",
                column: "CulturaId");

            migrationBuilder.CreateIndex(
                name: "IX_FazendaCulturas_FazendaId_CulturaId",
                table: "FazendaCulturas",
                columns: new[] { "FazendaId", "CulturaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fazendas_CNPJ",
                table: "Fazendas",
                column: "CNPJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fazendas_Endereco_UF",
                table: "Fazendas",
                column: "Endereco_UF");

            migrationBuilder.CreateIndex(
                name: "IX_Fazendas_ProdutorId",
                table: "Fazendas",
                column: "ProdutorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoresRurais_CPF",
                table: "ProdutoresRurais",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoresRurais_Endereco_UF",
                table: "ProdutoresRurais",
                column: "Endereco_UF");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UsuarioId",
                table: "RefreshTokens",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true,
                filter: "\"Email\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_EmailConfirmationToken",
                table: "Usuarios",
                column: "EmailConfirmationToken",
                filter: "\"EmailConfirmationToken\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_NomeUsuario",
                table: "Usuarios",
                column: "NomeUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PasswordResetToken",
                table: "Usuarios",
                column: "PasswordResetToken",
                filter: "\"PasswordResetToken\" IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FazendaCulturas");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Culturas");

            migrationBuilder.DropTable(
                name: "Fazendas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "ProdutoresRurais");
        }
    }
}
