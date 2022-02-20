using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Andor.Migrations
{
    public partial class primeira : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Imagens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_tipo = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dados = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(13)", nullable: true),
                    CRNM = table.Column<string>(type: "nvarchar(15)", nullable: true),
                    CPF = table.Column<string>(type: "nvarchar(11)", nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(60)", nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    CEP = table.Column<int>(type: "int", nullable: false),
                    UF = table.Column<string>(type: "nvarchar(2)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Sexo = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime", nullable: true),
                    Nacionalidade = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime", nullable: true),
                    Classe = table.Column<string>(type: "nvarchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Experiencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_pessoa = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Cargo = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Instituicao = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Inicio = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fim = table.Column<DateTime>(type: "datetime", nullable: true),
                    PessoaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiencias_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Formacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_pessoa = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Instituicao = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Inicio = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fim = table.Column<DateTime>(type: "datetime", nullable: true),
                    Situacao = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    PessoaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Formacoes_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Moradias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_pessoa = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(15)", nullable: true),
                    Preco = table.Column<decimal>(type: "money", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(60)", nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    CEP = table.Column<int>(type: "int", nullable: false),
                    UF = table.Column<string>(type: "nvarchar(2)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    NomeContato = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    TelefoneContato = table.Column<string>(type: "nvarchar(13)", nullable: true),
                    EmailContato = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime", nullable: false),
                    PessoaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moradias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Moradias_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trabalhos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_pessoa = table.Column<int>(type: "int", nullable: false),
                    Instituicao = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Atividade = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Salario = table.Column<decimal>(type: "money", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(60)", nullable: true),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    CEP = table.Column<int>(type: "int", nullable: false),
                    UF = table.Column<string>(type: "nvarchar(2)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    NomeContato = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    TelefoneContato = table.Column<string>(type: "nvarchar(13)", nullable: true),
                    EmailContato = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime", nullable: false),
                    PessoaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trabalhos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trabalhos_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Experiencias_PessoaId",
                table: "Experiencias",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_Formacoes_PessoaId",
                table: "Formacoes",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_Moradias_PessoaId",
                table: "Moradias",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_Trabalhos_PessoaId",
                table: "Trabalhos",
                column: "PessoaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Experiencias");

            migrationBuilder.DropTable(
                name: "Formacoes");

            migrationBuilder.DropTable(
                name: "Imagens");

            migrationBuilder.DropTable(
                name: "Moradias");

            migrationBuilder.DropTable(
                name: "Trabalhos");

            migrationBuilder.DropTable(
                name: "Pessoas");
        }
    }
}
