using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgendaDeTurnos.Migrations
{
    public partial class Default : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prestacion",
                columns: table => new
                {
                    PrestacionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    Duracion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Precio = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestacion", x => x.PrestacionId);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Apellido = table.Column<string>(type: "TEXT", nullable: false),
                    Dni = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Telefono = table.Column<string>(type: "TEXT", nullable: false),
                    Direccion = table.Column<string>(type: "TEXT", nullable: true),
                    FechaAlta = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Password = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    ObraSocial = table.Column<string>(type: "TEXT", nullable: true),
                    Matricula = table.Column<string>(type: "TEXT", nullable: true),
                    HoraInicio = table.Column<DateTime>(type: "TEXT", nullable: true),
                    HoraFin = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PrestacionId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Prestacion_PrestacionId",
                        column: x => x.PrestacionId,
                        principalTable: "Prestacion",
                        principalColumn: "PrestacionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Turno",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Confirmado = table.Column<bool>(type: "INTEGER", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DescripcionCancelacion = table.Column<string>(type: "TEXT", nullable: true),
                    PacienteId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ProfesionalId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turno_Usuario_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Turno_Usuario_ProfesionalId",
                        column: x => x.ProfesionalId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Turno_PacienteId",
                table: "Turno",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Turno_ProfesionalId",
                table: "Turno",
                column: "ProfesionalId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_PrestacionId",
                table: "Usuario",
                column: "PrestacionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Turno");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Prestacion");
        }
    }
}
