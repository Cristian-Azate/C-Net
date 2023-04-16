using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CNet.Migrations
{
    /// <inheritdoc />
    public partial class InitData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tarea",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Categoria",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "CategoriId", "Descripcion", "Nombre", "Peso" },
                values: new object[,]
                {
                    { new Guid("642f2d1e-6e5d-4f40-9260-c7ba12807202"), null, "Actividades Personales", 50 },
                    { new Guid("642f2d1e-6e5d-4f40-9260-c7ba1280727c"), null, "Actividades Pendientes", 20 }
                });

            migrationBuilder.InsertData(
                table: "Tarea",
                columns: new[] { "TareaId", "CategoriaId", "Descripcion", "Fecha_Creacion", "Prioridad_Tarea", "Titulo" },
                values: new object[,]
                {
                    { new Guid("642f2d1e-6e5d-4f40-9260-c7ba12807210"), new Guid("642f2d1e-6e5d-4f40-9260-c7ba1280727c"), null, new DateTime(2023, 1, 26, 17, 57, 10, 17, DateTimeKind.Local).AddTicks(5350), 1, "Pago de servicios" },
                    { new Guid("642f2d1e-6e5d-4f40-9260-c7ba12807211"), new Guid("642f2d1e-6e5d-4f40-9260-c7ba12807202"), null, new DateTime(2023, 1, 26, 17, 57, 10, 17, DateTimeKind.Local).AddTicks(5363), 0, "Terminar serie netflix" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "TareaId",
                keyValue: new Guid("642f2d1e-6e5d-4f40-9260-c7ba12807210"));

            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "TareaId",
                keyValue: new Guid("642f2d1e-6e5d-4f40-9260-c7ba12807211"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriId",
                keyValue: new Guid("642f2d1e-6e5d-4f40-9260-c7ba12807202"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriId",
                keyValue: new Guid("642f2d1e-6e5d-4f40-9260-c7ba1280727c"));

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tarea",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Categoria",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
