using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiGuachincheWeb.Migrations
{
    public partial class valoraciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "descripcion",
                table: "zona",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "valoracion",
                table: "restaurante",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "telefono",
                table: "restaurante",
                type: "char(9)",
                unicode: false,
                fixedLength: true,
                maxLength: 9,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(9)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 9,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "valoracion",
                table: "plato_restaurantes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ValoracionPlato",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    valoracion = table.Column<int>(type: "int", nullable: false),
                    platoRestauranteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValoracionPlato", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValoracionPlato_plato_restaurantes_platoRestauranteId",
                        column: x => x.platoRestauranteId,
                        principalTable: "plato_restaurantes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade,
                        onUpdate: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ValoracionRestaurante",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    valoracion = table.Column<int>(type: "int", nullable: false),
                    restauranteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValoracionRestaurante", x => x.id);
                    table.ForeignKey(
                        name: "FK_ValoracionRestaurante_restaurante_restauranteId",
                        column: x => x.restauranteId,
                        principalTable: "restaurante",
                        principalColumn: "RestauranteId",
                        onDelete: ReferentialAction.Cascade,
                        onUpdate: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ValoracionPlato");

            migrationBuilder.DropTable(
                name: "ValoracionRestaurante");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3bab990c-c21c-4268-bfc0-be5ac3dbee3a", "851f126d-4fa2-41c1-bb0e-37fad69f7173" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "560914ee-fd95-4231-876c-ea1895211d7d", "a64633ac-4732-4231-b4c8-39225161da02" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "23988eb0-912f-49be-8839-99036353c01c", "bad933f6-cc7f-476f-ad0a-9358a1e3b8d3" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23988eb0-912f-49be-8839-99036353c01c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3bab990c-c21c-4268-bfc0-be5ac3dbee3a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "560914ee-fd95-4231-876c-ea1895211d7d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "851f126d-4fa2-41c1-bb0e-37fad69f7173");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a64633ac-4732-4231-b4c8-39225161da02");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bad933f6-cc7f-476f-ad0a-9358a1e3b8d3");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "restaurante");

            migrationBuilder.AlterColumn<string>(
                name: "descripcion",
                table: "zona",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<int>(
                name: "valoracion",
                table: "restaurante",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "telefono",
                table: "restaurante",
                type: "char(9)",
                unicode: false,
                fixedLength: true,
                maxLength: 9,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(9)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 9);

            migrationBuilder.AlterColumn<int>(
                name: "valoracion",
                table: "plato_restaurantes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "555753b2-84a3-4a93-a41d-9864516b97cd", "fb9625e5-94e5-4ee5-ac33-b1fa48a8a310", "Default", "DEFAULT" },
                    { "9f58e5ee-1397-460c-975a-b8dda54503e1", "db59f895-b6e2-4a2d-86cb-87c940a4dba0", "Manager", "MANAGER" },
                    { "f162e4de-4550-4d27-87bb-356d4f65d58d", "8a8f1db8-09bd-44e1-8402-4fc81d621fec", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Apelllidos", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Nombre", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Telefono", "TwoFactorEnabled", "UserName", "isActive" },
                values: new object[,]
                {
                    { "60a8863f-af7b-40be-bd46-c0847a2ae8b4", 0, "Guachinche", "dfd2b1ab-74a9-4a2a-9318-58c4501b4b83", "Manager@guachinche.com", true, false, null, "Manager", "MANAGER@GUACHINCHE.COM", "MANAGER@GUACHINCHE.COM", "AQAAAAEAACcQAAAAEMgQR8e8SbNXvhKMbjizX2Jl4wkLswryFrG0gCVzt28hLT1v3vrCQCYtc1OTXq2N7g==", null, false, "0e0b49fb-cad7-4333-82c4-8c63497d1a0b", "922000111", false, "Manager@guachinche.com", true },
                    { "69d0b763-34a3-4bd5-ae5a-06e125932b2f", 0, "Bartolome Navarro", "d77d9e5d-5578-41f0-bff6-e58cd6be4e78", "Admin@guachinche.com", true, false, null, "Cesar", "ADMIN@GUACHINCHE.COM", "ADMIN@GUACHINCHE.COM", "AQAAAAEAACcQAAAAEOmMYsjpa90/RSnxaW0DIujEX1MKNMIbAr9xerJS8t8HqmZv3kF1d7ahF/Rysq4ycA==", null, false, "f10573d8-222d-41e6-9764-08227020f991", "922111333", false, "Admin@guachinche.com", true },
                    { "8a4e9a90-228d-41d0-9953-cc39fa494375", 0, "Guachinche", "f357f015-de86-4ab9-b168-7294039bb821", "User@guachinche.com", true, false, null, "User", "USER@GUACHINCHE.COM", "USER@GUACHINCHE.COM", "AQAAAAEAACcQAAAAEN1/a+fF1tmKeoeGt+CRgHPJS+Rgt3kKPRtZTI42sR+ZFtqaSI/yydU/Tf2b+xyj/g==", null, false, "82eeec92-eb5c-46e5-b1b0-aa45ba4e8c4d", "922456789", false, "User@guachinche.com", true }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "9f58e5ee-1397-460c-975a-b8dda54503e1", "60a8863f-af7b-40be-bd46-c0847a2ae8b4" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f162e4de-4550-4d27-87bb-356d4f65d58d", "69d0b763-34a3-4bd5-ae5a-06e125932b2f" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "555753b2-84a3-4a93-a41d-9864516b97cd", "8a4e9a90-228d-41d0-9953-cc39fa494375" });
        }
    }
}
