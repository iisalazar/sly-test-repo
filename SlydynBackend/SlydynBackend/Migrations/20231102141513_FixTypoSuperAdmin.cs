using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SlydynBackend.Migrations
{
    /// <inheritdoc />
    public partial class FixTypoSuperAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "706dd4a9-afe3-4d09-8364-e97d9b188e9a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da75982b-8e7c-4c21-8dec-9a429b2f94d9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f03b5b8a-9fc0-4992-b5fb-dbeb01567c4e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "661cb1a2-bec5-4cdb-b2f4-15551d4fbea9", null, "SuperAdmin", "SUPERADMIN" },
                    { "a1ba286c-ff6d-4cd5-adbf-1d4f236598e8", null, "Consumer", "CONSUMER" },
                    { "dc38e01c-d8b5-4ac9-9a09-f940ac735bee", null, "Dealer", "DEALER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "661cb1a2-bec5-4cdb-b2f4-15551d4fbea9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1ba286c-ff6d-4cd5-adbf-1d4f236598e8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc38e01c-d8b5-4ac9-9a09-f940ac735bee");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "706dd4a9-afe3-4d09-8364-e97d9b188e9a", null, "SuperAdmin", "SUPER ADMIN" },
                    { "da75982b-8e7c-4c21-8dec-9a429b2f94d9", null, "Dealer", "DEALER" },
                    { "f03b5b8a-9fc0-4992-b5fb-dbeb01567c4e", null, "Consumer", "CONSUMER" }
                });
        }
    }
}
