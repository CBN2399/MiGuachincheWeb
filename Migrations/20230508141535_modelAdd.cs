using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiGuachincheWeb.Migrations
{
    public partial class modelAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Apelllidos = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "estadoReservas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estadoReservas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tipo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tipoRestaurante",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipoRestaurante", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "zona",
                columns: table => new
                {
                    Zona_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__zona__CD7EA2967C6981B9", x => x.Zona_id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "plato",
                columns: table => new
                {
                    PlatoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    tipoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plato", x => x.PlatoId);
                    table.ForeignKey(
                        name: "fk_tipo",
                        column: x => x.tipoId,
                        principalTable: "tipo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade,
                        onUpdate: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "restaurante",
                columns: table => new
                {
                    RestauranteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Rest_Url = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    telefono = table.Column<string>(type: "char(9)", unicode: false, fixedLength: true, maxLength: 9, nullable: true),
                    valoracion = table.Column<int>(type: "int", nullable: true),
                    Id_tipo = table.Column<int>(type: "int", nullable: false),
                    zonaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_restaurante", x => x.RestauranteId);
                    table.ForeignKey(
                        name: "fk_tipoRest",
                        column: x => x.Id_tipo,
                        principalTable: "tipoRestaurante",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_zona",
                        column: x => x.zonaId,
                        principalTable: "zona",
                        principalColumn: "Zona_id",
                        onDelete: ReferentialAction.Cascade,
                        onUpdate: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "plato_restaurantes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    plato_Id = table.Column<int>(type: "int", nullable: false),
                    restaurante_Id = table.Column<int>(type: "int", nullable: false),
                    valoracion = table.Column<int>(type: "int", nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: false),
                    PlatoId = table.Column<int>(type: "int", nullable: true),
                    RestauranteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plato_restaurantes", x => x.id);
                    table.ForeignKey(
                        name: "FK_plato_restaurantes_plato_PlatoId",
                        column: x => x.PlatoId,
                        principalTable: "plato",
                        principalColumn: "PlatoId");
                    table.ForeignKey(
                        name: "FK_plato_restaurantes_restaurante_RestauranteId",
                        column: x => x.RestauranteId,
                        principalTable: "restaurante",
                        principalColumn: "RestauranteId",
                        onDelete: ReferentialAction.Cascade,
                        onUpdate: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaReserva = table.Column<DateTime>(type: "datetime2", nullable: false),
                    customerUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    restauranteId = table.Column<int>(type: "int", nullable: false),
                    estadoReservaId = table.Column<int>(type: "int", nullable: false),
                    numeroComensales = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.Id);
                    table.ForeignKey(
                        name: "fk_estado_reserva",
                        column: x => x.estadoReservaId,
                        principalTable: "estadoReservas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade,
                        onUpdate: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_reserva_restaurante",
                        column: x => x.restauranteId,
                        principalTable: "restaurante",
                        principalColumn: "RestauranteId",
                        onDelete: ReferentialAction.Cascade,
                        onUpdate: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_reservas",
                        column: x => x.customerUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade,
                        onUpdate: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userRestaurantes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuario_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    restaurante_Id = table.Column<int>(type: "int", nullable: false),
                    RestauranteId = table.Column<int>(type: "int", nullable: true),
                    customUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userRestaurantes", x => x.id);
                    table.ForeignKey(
                        name: "FK_userRestaurantes_AspNetUsers_customUserId",
                        column: x => x.customUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_userRestaurantes_restaurante_RestauranteId",
                        column: x => x.RestauranteId,
                        principalTable: "restaurante",
                        principalColumn: "RestauranteId");
                });

            migrationBuilder.CreateTable(
                name: "userPlatoRestaurante",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuario_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    plato_restaurante_Id = table.Column<int>(type: "int", nullable: false),
                    plato_restauranteid = table.Column<int>(type: "int", nullable: true),
                    custom_userId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userPlatoRestaurante", x => x.id);
                    table.ForeignKey(
                        name: "FK_userPlatoRestaurante_AspNetUsers_custom_userId",
                        column: x => x.custom_userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade,
                        onUpdate: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userPlatoRestaurante_plato_restaurantes_plato_restauranteid",
                        column: x => x.plato_restauranteid,
                        principalTable: "plato_restaurantes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade,
                        onUpdate: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "estadoReservas",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 3, "Finalizada" },
                    { 2, "Cancelada" },
                    { 1, "Activa" }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ifk_PlatoTipo",
                table: "plato",
                column: "tipoId");

            migrationBuilder.CreateIndex(
                name: "IX_plato_restaurantes_PlatoId",
                table: "plato_restaurantes",
                column: "PlatoId");

            migrationBuilder.CreateIndex(
                name: "IX_plato_restaurantes_RestauranteId",
                table: "plato_restaurantes",
                column: "RestauranteId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_customerUserId",
                table: "Reservas",
                column: "customerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_estadoReservaId",
                table: "Reservas",
                column: "estadoReservaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_restauranteId",
                table: "Reservas",
                column: "restauranteId");

            migrationBuilder.CreateIndex(
                name: "ifk_restTipoRest",
                table: "restaurante",
                column: "Id_tipo");

            migrationBuilder.CreateIndex(
                name: "ifk_zonaRest",
                table: "restaurante",
                column: "zonaId");

            migrationBuilder.CreateIndex(
                name: "IX_userPlatoRestaurante_custom_userId",
                table: "userPlatoRestaurante",
                column: "custom_userId");

            migrationBuilder.CreateIndex(
                name: "IX_userPlatoRestaurante_plato_restauranteid",
                table: "userPlatoRestaurante",
                column: "plato_restauranteid");

            migrationBuilder.CreateIndex(
                name: "IX_userRestaurantes_customUserId",
                table: "userRestaurantes",
                column: "customUserId");

            migrationBuilder.CreateIndex(
                name: "IX_userRestaurantes_RestauranteId",
                table: "userRestaurantes",
                column: "RestauranteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "userPlatoRestaurante");

            migrationBuilder.DropTable(
                name: "userRestaurantes");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "estadoReservas");

            migrationBuilder.DropTable(
                name: "plato_restaurantes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "plato");

            migrationBuilder.DropTable(
                name: "restaurante");

            migrationBuilder.DropTable(
                name: "tipo");

            migrationBuilder.DropTable(
                name: "tipoRestaurante");

            migrationBuilder.DropTable(
                name: "zona");
        }
    }
}
