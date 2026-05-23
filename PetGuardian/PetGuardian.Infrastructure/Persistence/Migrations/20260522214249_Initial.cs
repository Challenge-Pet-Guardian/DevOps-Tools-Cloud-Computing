using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetGuardian.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PG_ESTADOS",
                columns: table => new
                {
                    ID_ESTADO = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    NOME_ESTADO = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PG_ESTADOS", x => x.ID_ESTADO);
                });

            migrationBuilder.CreateTable(
                name: "PG_RACAS",
                columns: table => new
                {
                    ID_RACA = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    NOME_RACA = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PG_RACAS", x => x.ID_RACA);
                });

            migrationBuilder.CreateTable(
                name: "PG_STATUS",
                columns: table => new
                {
                    ID_STATUS = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    NOME_STATUS = table.Column<string>(type: "NVARCHAR2(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PG_STATUS", x => x.ID_STATUS);
                });

            migrationBuilder.CreateTable(
                name: "PG_TELEFONES",
                columns: table => new
                {
                    ID_TELEFONE = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    NUM_DDD = table.Column<string>(type: "NVARCHAR2(2)", maxLength: 2, nullable: false),
                    NUM_TEL = table.Column<string>(type: "NVARCHAR2(9)", maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PG_TELEFONES", x => x.ID_TELEFONE);
                });

            migrationBuilder.CreateTable(
                name: "PG_TIPO_ATEND",
                columns: table => new
                {
                    ID_TIPO_ATEND = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    TIPO = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PG_TIPO_ATEND", x => x.ID_TIPO_ATEND);
                });

            migrationBuilder.CreateTable(
                name: "PG_CIDADES",
                columns: table => new
                {
                    ID_CIDADE = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    NOME_CIDADE = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    ID_ESTADO = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PG_CIDADES", x => x.ID_CIDADE);
                    table.ForeignKey(
                        name: "FK_PG_CIDADES_PG_ESTADOS_ID_ESTADO",
                        column: x => x.ID_ESTADO,
                        principalTable: "PG_ESTADOS",
                        principalColumn: "ID_ESTADO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PG_PETS",
                columns: table => new
                {
                    ID_PET = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    NOME = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    IDADE = table.Column<byte>(type: "NUMBER(2)", nullable: false),
                    SEXO = table.Column<string>(type: "NVARCHAR2(1)", maxLength: 1, nullable: false),
                    PORTE = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false),
                    CASTRADO = table.Column<string>(type: "NVARCHAR2(1)", maxLength: 1, nullable: false),
                    ID_RACA = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PG_PETS", x => x.ID_PET);
                    table.ForeignKey(
                        name: "FK_PG_PETS_PG_RACAS_ID_RACA",
                        column: x => x.ID_RACA,
                        principalTable: "PG_RACAS",
                        principalColumn: "ID_RACA",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PG_USUARIOS",
                columns: table => new
                {
                    ID_USUARIO = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    NOME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    SENHA = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    ID_TELEFONE = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PG_USUARIOS", x => x.ID_USUARIO);
                    table.ForeignKey(
                        name: "FK_PG_USUARIOS_PG_TELEFONES_ID_TELEFONE",
                        column: x => x.ID_TELEFONE,
                        principalTable: "PG_TELEFONES",
                        principalColumn: "ID_TELEFONE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PG_BAIRROS",
                columns: table => new
                {
                    ID_BAIRRO = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    NOME_BAIRRO = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    ID_CIDADE = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PG_BAIRROS", x => x.ID_BAIRRO);
                    table.ForeignKey(
                        name: "FK_PG_BAIRROS_PG_CIDADES_ID_CIDADE",
                        column: x => x.ID_CIDADE,
                        principalTable: "PG_CIDADES",
                        principalColumn: "ID_CIDADE",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PG_USUARIO_PET",
                columns: table => new
                {
                    ID_USUARIO = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    ID_PET = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    RESPON_PRINC = table.Column<string>(type: "NVARCHAR2(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PG_USUARIO_PET", x => new { x.ID_USUARIO, x.ID_PET });
                    table.ForeignKey(
                        name: "FK_PG_USUARIO_PET_PG_PETS_ID_PET",
                        column: x => x.ID_PET,
                        principalTable: "PG_PETS",
                        principalColumn: "ID_PET",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PG_USUARIO_PET_PG_USUARIOS_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "PG_USUARIOS",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PG_ENDERECOS",
                columns: table => new
                {
                    ID_ENDERECO = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    CEP = table.Column<string>(type: "NVARCHAR2(8)", maxLength: 8, nullable: false),
                    RUA = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    NUMERO = table.Column<string>(type: "NVARCHAR2(5)", maxLength: 5, nullable: false),
                    ID_BAIRRO = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PG_ENDERECOS", x => x.ID_ENDERECO);
                    table.ForeignKey(
                        name: "FK_PG_ENDERECOS_PG_BAIRROS_ID_BAIRRO",
                        column: x => x.ID_BAIRRO,
                        principalTable: "PG_BAIRROS",
                        principalColumn: "ID_BAIRRO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PG_CLINICAS",
                columns: table => new
                {
                    ID_CLINICA = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    NOME = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    ID_TELEFONE = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    ID_ENDERECO = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PG_CLINICAS", x => x.ID_CLINICA);
                    table.ForeignKey(
                        name: "FK_PG_CLINICAS_PG_ENDERECOS_ID_ENDERECO",
                        column: x => x.ID_ENDERECO,
                        principalTable: "PG_ENDERECOS",
                        principalColumn: "ID_ENDERECO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PG_CLINICAS_PG_TELEFONES_ID_TELEFONE",
                        column: x => x.ID_TELEFONE,
                        principalTable: "PG_TELEFONES",
                        principalColumn: "ID_TELEFONE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PG_USUARIO_ENDERECO",
                columns: table => new
                {
                    ID_USUARIO = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    ID_ENDERECO = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PG_USUARIO_ENDERECO", x => new { x.ID_USUARIO, x.ID_ENDERECO });
                    table.ForeignKey(
                        name: "FK_PG_USUARIO_ENDERECO_PG_ENDERECOS_ID_ENDERECO",
                        column: x => x.ID_ENDERECO,
                        principalTable: "PG_ENDERECOS",
                        principalColumn: "ID_ENDERECO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PG_USUARIO_ENDERECO_PG_USUARIOS_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "PG_USUARIOS",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PG_VETERINARIOS",
                columns: table => new
                {
                    ID_VETERINARIO = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    NOME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    SENHA = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    ID_TELEFONE = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    ID_CLINICA = table.Column<Guid>(type: "RAW(16)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PG_VETERINARIOS", x => x.ID_VETERINARIO);
                    table.ForeignKey(
                        name: "FK_PG_VETERINARIOS_PG_CLINICAS_ID_CLINICA",
                        column: x => x.ID_CLINICA,
                        principalTable: "PG_CLINICAS",
                        principalColumn: "ID_CLINICA",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PG_VETERINARIOS_PG_TELEFONES_ID_TELEFONE",
                        column: x => x.ID_TELEFONE,
                        principalTable: "PG_TELEFONES",
                        principalColumn: "ID_TELEFONE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PG_ATENDIMENTOS",
                columns: table => new
                {
                    ID_ATENDIMENTO = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    DATA = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ANOTACOES = table.Column<string>(type: "NVARCHAR2(300)", maxLength: 300, nullable: false),
                    VALOR = table.Column<decimal>(type: "NUMBER(10,2)", nullable: false),
                    ID_PET = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    ID_VETERINARIO = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    ID_TIPO_ATEND = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    ID_STATUS = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PG_ATENDIMENTOS", x => x.ID_ATENDIMENTO);
                    table.ForeignKey(
                        name: "FK_PG_ATENDIMENTOS_PG_PETS_ID_PET",
                        column: x => x.ID_PET,
                        principalTable: "PG_PETS",
                        principalColumn: "ID_PET",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PG_ATENDIMENTOS_PG_STATUS_ID_STATUS",
                        column: x => x.ID_STATUS,
                        principalTable: "PG_STATUS",
                        principalColumn: "ID_STATUS",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PG_ATENDIMENTOS_PG_TIPO_ATEND_ID_TIPO_ATEND",
                        column: x => x.ID_TIPO_ATEND,
                        principalTable: "PG_TIPO_ATEND",
                        principalColumn: "ID_TIPO_ATEND",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PG_ATENDIMENTOS_PG_VETERINARIOS_ID_VETERINARIO",
                        column: x => x.ID_VETERINARIO,
                        principalTable: "PG_VETERINARIOS",
                        principalColumn: "ID_VETERINARIO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PG_TAREFAS",
                columns: table => new
                {
                    ID_TAREFA = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    TITULO = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    PONTOS_TAREFA = table.Column<byte>(type: "NUMBER(3)", nullable: false),
                    DESCRICAO = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    CRIACAO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    PRAZO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    CONCLUSAO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    ID_USUARIO = table.Column<Guid>(type: "RAW(16)", nullable: true),
                    ID_PET = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    ID_STATUS = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    ID_VETERINARIO = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PG_TAREFAS", x => x.ID_TAREFA);
                    table.ForeignKey(
                        name: "FK_PG_TAREFAS_PG_PETS_ID_PET",
                        column: x => x.ID_PET,
                        principalTable: "PG_PETS",
                        principalColumn: "ID_PET",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PG_TAREFAS_PG_STATUS_ID_STATUS",
                        column: x => x.ID_STATUS,
                        principalTable: "PG_STATUS",
                        principalColumn: "ID_STATUS",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PG_TAREFAS_PG_USUARIOS_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "PG_USUARIOS",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PG_TAREFAS_PG_VETERINARIOS_ID_VETERINARIO",
                        column: x => x.ID_VETERINARIO,
                        principalTable: "PG_VETERINARIOS",
                        principalColumn: "ID_VETERINARIO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PG_ATENDIMENTOS_ID_PET",
                table: "PG_ATENDIMENTOS",
                column: "ID_PET");

            migrationBuilder.CreateIndex(
                name: "IX_PG_ATENDIMENTOS_ID_STATUS",
                table: "PG_ATENDIMENTOS",
                column: "ID_STATUS");

            migrationBuilder.CreateIndex(
                name: "IX_PG_ATENDIMENTOS_ID_TIPO_ATEND",
                table: "PG_ATENDIMENTOS",
                column: "ID_TIPO_ATEND");

            migrationBuilder.CreateIndex(
                name: "IX_PG_ATENDIMENTOS_ID_VETERINARIO",
                table: "PG_ATENDIMENTOS",
                column: "ID_VETERINARIO");

            migrationBuilder.CreateIndex(
                name: "IX_PG_BAIRROS_ID_CIDADE",
                table: "PG_BAIRROS",
                column: "ID_CIDADE");

            migrationBuilder.CreateIndex(
                name: "IX_PG_CIDADES_ID_ESTADO",
                table: "PG_CIDADES",
                column: "ID_ESTADO");

            migrationBuilder.CreateIndex(
                name: "IX_PG_CLINICAS_ID_ENDERECO",
                table: "PG_CLINICAS",
                column: "ID_ENDERECO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PG_CLINICAS_ID_TELEFONE",
                table: "PG_CLINICAS",
                column: "ID_TELEFONE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PG_ENDERECOS_ID_BAIRRO",
                table: "PG_ENDERECOS",
                column: "ID_BAIRRO");

            migrationBuilder.CreateIndex(
                name: "IX_PG_PETS_ID_RACA",
                table: "PG_PETS",
                column: "ID_RACA");

            migrationBuilder.CreateIndex(
                name: "IX_PG_TAREFAS_ID_PET",
                table: "PG_TAREFAS",
                column: "ID_PET");

            migrationBuilder.CreateIndex(
                name: "IX_PG_TAREFAS_ID_STATUS",
                table: "PG_TAREFAS",
                column: "ID_STATUS");

            migrationBuilder.CreateIndex(
                name: "IX_PG_TAREFAS_ID_USUARIO",
                table: "PG_TAREFAS",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_PG_TAREFAS_ID_VETERINARIO",
                table: "PG_TAREFAS",
                column: "ID_VETERINARIO");

            migrationBuilder.CreateIndex(
                name: "IX_PG_USUARIO_ENDERECO_ID_ENDERECO",
                table: "PG_USUARIO_ENDERECO",
                column: "ID_ENDERECO");

            migrationBuilder.CreateIndex(
                name: "IX_PG_USUARIO_PET_ID_PET",
                table: "PG_USUARIO_PET",
                column: "ID_PET");

            migrationBuilder.CreateIndex(
                name: "IX_PG_USUARIOS_EMAIL",
                table: "PG_USUARIOS",
                column: "EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PG_USUARIOS_ID_TELEFONE",
                table: "PG_USUARIOS",
                column: "ID_TELEFONE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PG_VETERINARIOS_EMAIL",
                table: "PG_VETERINARIOS",
                column: "EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PG_VETERINARIOS_ID_CLINICA",
                table: "PG_VETERINARIOS",
                column: "ID_CLINICA");

            migrationBuilder.CreateIndex(
                name: "IX_PG_VETERINARIOS_ID_TELEFONE",
                table: "PG_VETERINARIOS",
                column: "ID_TELEFONE",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PG_ATENDIMENTOS");

            migrationBuilder.DropTable(
                name: "PG_TAREFAS");

            migrationBuilder.DropTable(
                name: "PG_USUARIO_ENDERECO");

            migrationBuilder.DropTable(
                name: "PG_USUARIO_PET");

            migrationBuilder.DropTable(
                name: "PG_TIPO_ATEND");

            migrationBuilder.DropTable(
                name: "PG_STATUS");

            migrationBuilder.DropTable(
                name: "PG_VETERINARIOS");

            migrationBuilder.DropTable(
                name: "PG_PETS");

            migrationBuilder.DropTable(
                name: "PG_USUARIOS");

            migrationBuilder.DropTable(
                name: "PG_CLINICAS");

            migrationBuilder.DropTable(
                name: "PG_RACAS");

            migrationBuilder.DropTable(
                name: "PG_ENDERECOS");

            migrationBuilder.DropTable(
                name: "PG_TELEFONES");

            migrationBuilder.DropTable(
                name: "PG_BAIRROS");

            migrationBuilder.DropTable(
                name: "PG_CIDADES");

            migrationBuilder.DropTable(
                name: "PG_ESTADOS");
        }
    }
}
