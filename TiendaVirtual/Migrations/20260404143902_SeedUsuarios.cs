using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TiendaVirtual.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Correo", "Nombre", "Rol", "celular" },
                values: new object[,]
                {
                    { 1, "admin@tienda.com", "Carlos Ramírez", "Administrador", "3001234567" },
                    { 2, "cajero@tienda.com", "Laura Gómez", "Cajero", "3109876543" },
                    { 3, "bodeguero@tienda.com", "Andrés Torres", "Bodeguero", "3154561234" },
                    { 4, "cliente1@correo.com", "María Pérez", "Cliente", "3201112233" },
                    { 5, "cliente2@correo.com", "Juan Mora", "Cliente", "3253334455" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
