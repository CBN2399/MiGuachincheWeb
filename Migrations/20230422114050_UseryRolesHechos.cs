using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiGuachincheWeb.Migrations
{
    public partial class UseryRolesHechos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "337d01c6-34cc-4c4e-b4b9-0625cf1e44d2", "89510659-243f-4dfa-a2f1-4d31d277a4bc", "Default", "DEFAULT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "40008872-b164-4cb1-bcf7-c949f1936642", "588191e5-cb99-43a4-9ff0-24fe59159d34", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "44824541-cff3-4467-be52-c523617c0779", 0, "8ef972f5-5699-4cce-b537-f17d4626c5bb", "Admin@guachinche.com", true, false, null, "ADMIN@GUACHINCHE.COM", "ADMIN@GUACHINCHE.COM", "AQAAAAEAACcQAAAAECXw9Weu3q69TRruBszVufgJ2IQ5bur5bIfgef3khY0+C2G1gSsabnjDN3iLVLCEUg==", null, false, "ff6d4a12-8170-40a2-a572-bffd2acdb263", false, "Admin@guachinche.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "40008872-b164-4cb1-bcf7-c949f1936642", "44824541-cff3-4467-be52-c523617c0779" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "337d01c6-34cc-4c4e-b4b9-0625cf1e44d2");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "40008872-b164-4cb1-bcf7-c949f1936642", "44824541-cff3-4467-be52-c523617c0779" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40008872-b164-4cb1-bcf7-c949f1936642");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44824541-cff3-4467-be52-c523617c0779");
        }
    }
}
