using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpendWise.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordResetFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Usuarios",
                newName: "Senha");

            migrationBuilder.AddColumn<string>(
                name: "PasswordResetToken",
                table: "Usuarios",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordResetTokenExpiry",
                table: "Usuarios",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LimiteMoeda",
                table: "Categorias",
                type: "character varying(3)",
                maxLength: 3,
                nullable: true,
                defaultValue: "BRL");

            migrationBuilder.AddColumn<decimal>(
                name: "LimiteValor",
                table: "Categorias",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Prioridade",
                table: "Categorias",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FechamentosMensais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    AnoMes = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false, comment: "Formato YYYY-MM"),
                    DataFechamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    TotalReceitas = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TotalDespesas = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    SaldoFinal = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Observacoes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FechamentosMensais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FechamentosMensais_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orcamentos_mensais",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ano_mes = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    Valor = table.Column<decimal>(type: "numeric(14,2)", precision: 18, scale: 2, nullable: false),
                    Moeda = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false, defaultValue: "BRL"),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orcamentos_mensais", x => x.id);
                    table.CheckConstraint("ck_orcamento_anomes_formato", "ano_mes ~ '^[0-9]{4}-(0[1-9]|1[0-2])$'");
                    table.CheckConstraint("ck_orcamento_valor_positivo", "valor >= 0");
                    table.ForeignKey(
                        name: "FK_orcamentos_mensais_Usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FechamentosMensais_AnoMes",
                table: "FechamentosMensais",
                column: "AnoMes");

            migrationBuilder.CreateIndex(
                name: "IX_FechamentosMensais_Status",
                table: "FechamentosMensais",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_FechamentosMensais_Usuario_AnoMes",
                table: "FechamentosMensais",
                columns: new[] { "UsuarioId", "AnoMes" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FechamentosMensais_UsuarioId",
                table: "FechamentosMensais",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "ix_orcamento_anomes",
                table: "orcamentos_mensais",
                column: "ano_mes");

            migrationBuilder.CreateIndex(
                name: "IX_OrcamentosMensais_Usuario_Periodo",
                table: "orcamentos_mensais",
                columns: new[] { "usuario_id", "ano_mes" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FechamentosMensais");

            migrationBuilder.DropTable(
                name: "orcamentos_mensais");

            migrationBuilder.DropColumn(
                name: "PasswordResetToken",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "PasswordResetTokenExpiry",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "LimiteMoeda",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "LimiteValor",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "Prioridade",
                table: "Categorias");

            migrationBuilder.RenameColumn(
                name: "Senha",
                table: "Usuarios",
                newName: "PasswordHash");
        }
    }
}
