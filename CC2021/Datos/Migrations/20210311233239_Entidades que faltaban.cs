using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Datos.Migrations
{
    public partial class Entidadesquefaltaban : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Empleado_EmpleadoId",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Empleado",
                table: "Empleado");

            migrationBuilder.RenameTable(
                name: "Empleado",
                newName: "Empleados");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Empleados",
                table: "Empleados",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Hermanados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HermanadoPrimera = table.Column<int>(type: "int", nullable: false),
                    HermanadoSegunda = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hermanados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lineas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    EmpleadoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lineas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lineas_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ordenes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LineaIniciadaPorId = table.Column<int>(type: "int", nullable: true),
                    EmpleadoOrdenId = table.Column<int>(type: "int", nullable: true),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    Objetivo = table.Column<int>(type: "int", nullable: false),
                    ModeloId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    LineaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ordenes_Colores_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ordenes_Empleados_EmpleadoOrdenId",
                        column: x => x.EmpleadoOrdenId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ordenes_Empleados_LineaIniciadaPorId",
                        column: x => x.LineaIniciadaPorId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ordenes_Lineas_LineaId",
                        column: x => x.LineaId,
                        principalTable: "Lineas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ordenes_Modelos_ModeloId",
                        column: x => x.ModeloId,
                        principalTable: "Modelos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HorariosDeTrabajo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrdenDeProduccionId = table.Column<int>(type: "int", nullable: false),
                    HermanadoId = table.Column<int>(type: "int", nullable: true),
                    TurnoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorariosDeTrabajo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HorariosDeTrabajo_Hermanados_HermanadoId",
                        column: x => x.HermanadoId,
                        principalTable: "Hermanados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HorariosDeTrabajo_Ordenes_OrdenDeProduccionId",
                        column: x => x.OrdenDeProduccionId,
                        principalTable: "Ordenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HorariosDeTrabajo_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hallazgos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    EmpleadoDeCalidadId = table.Column<int>(type: "int", nullable: true),
                    TipoPie = table.Column<int>(type: "int", nullable: false),
                    DefectoId = table.Column<int>(type: "int", nullable: true),
                    HorarioTrabajoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hallazgos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hallazgos_Defectos_DefectoId",
                        column: x => x.DefectoId,
                        principalTable: "Defectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hallazgos_Empleados_EmpleadoDeCalidadId",
                        column: x => x.EmpleadoDeCalidadId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hallazgos_HorariosDeTrabajo_HorarioTrabajoId",
                        column: x => x.HorarioTrabajoId,
                        principalTable: "HorariosDeTrabajo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Primeras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    EmpleadoDeCalidadId = table.Column<int>(type: "int", nullable: true),
                    HorarioTrabajoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Primeras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Primeras_Empleados_EmpleadoDeCalidadId",
                        column: x => x.EmpleadoDeCalidadId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Primeras_HorariosDeTrabajo_HorarioTrabajoId",
                        column: x => x.HorarioTrabajoId,
                        principalTable: "HorariosDeTrabajo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hallazgos_DefectoId",
                table: "Hallazgos",
                column: "DefectoId");

            migrationBuilder.CreateIndex(
                name: "IX_Hallazgos_EmpleadoDeCalidadId",
                table: "Hallazgos",
                column: "EmpleadoDeCalidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Hallazgos_HorarioTrabajoId",
                table: "Hallazgos",
                column: "HorarioTrabajoId");

            migrationBuilder.CreateIndex(
                name: "IX_HorariosDeTrabajo_HermanadoId",
                table: "HorariosDeTrabajo",
                column: "HermanadoId");

            migrationBuilder.CreateIndex(
                name: "IX_HorariosDeTrabajo_OrdenDeProduccionId",
                table: "HorariosDeTrabajo",
                column: "OrdenDeProduccionId");

            migrationBuilder.CreateIndex(
                name: "IX_HorariosDeTrabajo_TurnoId",
                table: "HorariosDeTrabajo",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Lineas_EmpleadoId",
                table: "Lineas",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_ColorId",
                table: "Ordenes",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_EmpleadoOrdenId",
                table: "Ordenes",
                column: "EmpleadoOrdenId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_LineaId",
                table: "Ordenes",
                column: "LineaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_LineaIniciadaPorId",
                table: "Ordenes",
                column: "LineaIniciadaPorId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_ModeloId",
                table: "Ordenes",
                column: "ModeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Primeras_EmpleadoDeCalidadId",
                table: "Primeras",
                column: "EmpleadoDeCalidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Primeras_HorarioTrabajoId",
                table: "Primeras",
                column: "HorarioTrabajoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Empleados_EmpleadoId",
                table: "Usuarios",
                column: "EmpleadoId",
                principalTable: "Empleados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Empleados_EmpleadoId",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "Hallazgos");

            migrationBuilder.DropTable(
                name: "Primeras");

            migrationBuilder.DropTable(
                name: "HorariosDeTrabajo");

            migrationBuilder.DropTable(
                name: "Hermanados");

            migrationBuilder.DropTable(
                name: "Ordenes");

            migrationBuilder.DropTable(
                name: "Lineas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Empleados",
                table: "Empleados");

            migrationBuilder.RenameTable(
                name: "Empleados",
                newName: "Empleado");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Empleado",
                table: "Empleado",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Empleado_EmpleadoId",
                table: "Usuarios",
                column: "EmpleadoId",
                principalTable: "Empleado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
