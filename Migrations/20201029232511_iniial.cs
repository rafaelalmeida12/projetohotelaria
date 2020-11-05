using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoHotelaria.Migrations
{
    public partial class iniial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Acomodacao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    Capacidade = table.Column<int>(nullable: false),
                    Tipo = table.Column<int>(nullable: false),
                    TipoStatusAcomodacao = table.Column<int>(nullable: false),
                    Observacao = table.Column<string>(nullable: true),
                    ValorDiaria = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acomodacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hospede",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Cpf = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospede", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true),
                    Valor = table.Column<double>(nullable: false),
                    QuantidadeEstoque = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<int>(nullable: false),
                    QuantidadeHospede = table.Column<int>(nullable: false),
                    EntradaPrevista = table.Column<DateTime>(nullable: false),
                    SaidaPrevista = table.Column<DateTime>(nullable: false),
                    DataConfirmacao = table.Column<DateTime>(nullable: false),
                    Antecipacao = table.Column<double>(nullable: true),
                    ValorConsumo = table.Column<double>(nullable: false),
                    ValorTotal = table.Column<double>(nullable: false),
                    ValorReserva = table.Column<double>(nullable: false),
                    HospedeId = table.Column<int>(nullable: false),
                    AcomodacaoId = table.Column<int>(nullable: false),
                    TipoStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reserva_Acomodacao_AcomodacaoId",
                        column: x => x.AcomodacaoId,
                        principalTable: "Acomodacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserva_Hospede_HospedeId",
                        column: x => x.HospedeId,
                        principalTable: "Hospede",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consumo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ReservaId = table.Column<int>(nullable: false),
                    CodigoReserva = table.Column<int>(nullable: false),
                    ProdutoId = table.Column<int>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    Valor = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consumo_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consumo_Reserva_ReservaId",
                        column: x => x.ReservaId,
                        principalTable: "Reserva",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consumo_ProdutoId",
                table: "Consumo",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumo_ReservaId",
                table: "Consumo",
                column: "ReservaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_AcomodacaoId",
                table: "Reserva",
                column: "AcomodacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_HospedeId",
                table: "Reserva",
                column: "HospedeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consumo");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropTable(
                name: "Acomodacao");

            migrationBuilder.DropTable(
                name: "Hospede");
        }
    }
}
