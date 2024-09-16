using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleFinanceiro.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitalMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NomeCompleto = table.Column<string>(type: "text", nullable: false),
                    RegistroGeral = table.Column<string>(type: "text", nullable: false),
                    CadastroDePessoaFisica = table.Column<string>(type: "text", nullable: false),
                    DataDeNascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    ValorEmConta = table.Column<decimal>(type: "numeric", nullable: false),
                    ValorInvestido = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContasAPagar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    QuantidadeDeParcelas = table.Column<short>(type: "smallint", nullable: false),
                    ValorAPagar = table.Column<decimal>(type: "numeric", nullable: false),
                    PessoasId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdateOn = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContasAPagar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContasAPagar_Pessoas_PessoasId",
                        column: x => x.PessoasId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContasAPagar_PessoasId",
                table: "ContasAPagar",
                column: "PessoasId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContasAPagar");

            migrationBuilder.DropTable(
                name: "Pessoas");
        }
    }
}
