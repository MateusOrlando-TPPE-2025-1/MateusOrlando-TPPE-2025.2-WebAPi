using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpendWise.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateMetasTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Metas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ValorObjetivo = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ValorObjetivo_Moeda = table.Column<string>(type: "text", nullable: false),
                    Prazo = table.Column<DateTime>(type: "date", nullable: false),
                    ValorAtual = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ValorAtual_Moeda = table.Column<string>(type: "text", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsAtiva = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    DataAlcancada = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Metas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Metas_DataAlcancada",
                table: "Metas",
                column: "DataAlcancada");

            migrationBuilder.CreateIndex(
                name: "IX_Metas_UsuarioId",
                table: "Metas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Metas_UsuarioId_IsAtiva",
                table: "Metas",
                columns: new[] { "UsuarioId", "IsAtiva" });

            migrationBuilder.CreateIndex(
                name: "IX_Metas_UsuarioId_Prazo",
                table: "Metas",
                columns: new[] { "UsuarioId", "Prazo" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Metas");
        }
    }
}
