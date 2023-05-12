using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiGuachincheWeb.Migrations
{
    public partial class AddCamposToEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_reservas",
                table: "Reservas");

            migrationBuilder.AddColumn<string>(
                name: "ImagenURL",
                table: "zona",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "restaurante",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "customerUserId",
                table: "Reservas",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "numeroComensales",
                table: "Reservas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImagenURL",
                table: "plato",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_user_reservas",
                table: "Reservas",
                column: "customerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_reservas",
                table: "Reservas");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9f58e5ee-1397-460c-975a-b8dda54503e1", "60a8863f-af7b-40be-bd46-c0847a2ae8b4" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f162e4de-4550-4d27-87bb-356d4f65d58d", "69d0b763-34a3-4bd5-ae5a-06e125932b2f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "555753b2-84a3-4a93-a41d-9864516b97cd", "8a4e9a90-228d-41d0-9953-cc39fa494375" });

            migrationBuilder.DeleteData(
                table: "estadoReservas",
                keyColumn: "Id",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "estadoReservas",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "555753b2-84a3-4a93-a41d-9864516b97cd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f58e5ee-1397-460c-975a-b8dda54503e1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f162e4de-4550-4d27-87bb-356d4f65d58d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "60a8863f-af7b-40be-bd46-c0847a2ae8b4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "69d0b763-34a3-4bd5-ae5a-06e125932b2f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8a4e9a90-228d-41d0-9953-cc39fa494375");

            migrationBuilder.DropColumn(
                name: "ImagenURL",
                table: "zona");

            migrationBuilder.DropColumn(
                name: "numeroComensales",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "ImagenURL",
                table: "plato");

            migrationBuilder.AlterColumn<string>(
                name: "customerUserId",
                table: "Reservas",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "27402d78-a987-438f-a4df-3884633b951a", "5ded6073-515f-4f28-8930-a4c1a0821244", "Default", "DEFAULT" },
                    { "c674eca3-6b26-4840-b76f-0403d136a2e5", "52ccb179-41e3-45f0-b7cc-112d47ba692c", "Admin", "ADMIN" },
                    { "cfca3dac-e77f-46ef-b720-72494df08f62", "b2fd87bb-9ec4-4869-ab77-6a0f13cb5a95", "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Apelllidos", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Nombre", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Telefono", "TwoFactorEnabled", "UserName", "isActive" },
                values: new object[,]
                {
                    { "93c890cb-8693-4321-8073-6483f59cb28d", 0, "Bartolome Navarro", "4c2ee536-5c55-43b7-9375-a658ff48b8da", "Admin@guachinche.com", true, false, null, "Cesar", "ADMIN@GUACHINCHE.COM", "ADMIN@GUACHINCHE.COM", "AQAAAAEAACcQAAAAEL5p7Rz0WbGe0HeO7JT/SDm8gnafR5WQv6RHZqGl7cnCBk4j0NyUNOGCp1uSR8C0OQ==", null, false, "48607bc9-67bc-4e55-ad80-ac871ec42187", "922111333", false, "Admin@guachinche.com", true },
                    { "9e148492-0392-44ed-a1f7-91239d86b406", 0, "Guachinche", "a3226d5a-2d35-4b90-9e1c-908428f4f2fa", "Manager@guachinche.com", true, false, null, "Manager", "MANAGER@GUACHINCHE.COM", "MANAGER@GUACHINCHE.COM", "AQAAAAEAACcQAAAAEEqYdw6NGKargh/nFC9SMwRgJ5bUNJ43wGDB5xE8rijxOVu69tj5kDoR24qvjnOe/A==", null, false, "ee101bec-f15b-469d-a500-30bb2c49f8cd", "922000111", false, "Manager@guachinche.com", true },
                    { "b24fd563-79b5-4d27-abed-bb34d1756a5f", 0, "Guachinche", "084c57f0-5fea-4347-a3c8-0736e918d3ca", "User@guachinche.com", true, false, null, "User", "USER@GUACHINCHE.COM", "USER@GUACHINCHE.COM", "AQAAAAEAACcQAAAAEIKv+Qt1TaiV5pTqo+aC12Jex47aCvuZWPQvXmLSqGMB2LBTx0p8R3ERP3+6NxwmAA==", null, false, "2c99be1e-6bbc-4592-ba97-d0be42d0f8c2", "922456789", false, "User@guachinche.com", true }
                });

            migrationBuilder.UpdateData(
                table: "estadoReservas",
                keyColumn: "Id",
                keyValue: -3,
                column: "Name",
                value: "Finalizada");

            migrationBuilder.UpdateData(
                table: "estadoReservas",
                keyColumn: "Id",
                keyValue: -2,
                column: "Name",
                value: "Cancelada");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c674eca3-6b26-4840-b76f-0403d136a2e5", "93c890cb-8693-4321-8073-6483f59cb28d" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "cfca3dac-e77f-46ef-b720-72494df08f62", "9e148492-0392-44ed-a1f7-91239d86b406" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "27402d78-a987-438f-a4df-3884633b951a", "b24fd563-79b5-4d27-abed-bb34d1756a5f" });

            migrationBuilder.AddForeignKey(
                name: "fk_user_reservas",
                table: "Reservas",
                column: "customerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
