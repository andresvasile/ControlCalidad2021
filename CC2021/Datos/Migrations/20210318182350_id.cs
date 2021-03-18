using Microsoft.EntityFrameworkCore.Migrations;

namespace Datos.Migrations
{
    public partial class id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hallazgos_HorariosDeTrabajo_HorarioTrabajoId",
                table: "Hallazgos");

            migrationBuilder.AlterColumn<int>(
                name: "HorarioTrabajoId",
                table: "Hallazgos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Hallazgos_HorariosDeTrabajo_HorarioTrabajoId",
                table: "Hallazgos",
                column: "HorarioTrabajoId",
                principalTable: "HorariosDeTrabajo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hallazgos_HorariosDeTrabajo_HorarioTrabajoId",
                table: "Hallazgos");

            migrationBuilder.AlterColumn<int>(
                name: "HorarioTrabajoId",
                table: "Hallazgos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Hallazgos_HorariosDeTrabajo_HorarioTrabajoId",
                table: "Hallazgos",
                column: "HorarioTrabajoId",
                principalTable: "HorariosDeTrabajo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
