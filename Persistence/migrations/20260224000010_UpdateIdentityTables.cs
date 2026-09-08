using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MySurveyBasket.Persistence.migrations
{
    /// <inheritdoc />
    public partial class UpdateIdentityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "019c8cc9-ae28-7297-8c16-218c5d09c614", "019c8cc9-ae28-7297-8c16-218d6827b635", false, false, "Admin", "ADMIN" },
                    { "019c8cc9-ae28-7297-8c16-218e439b8b31", "019c8cc9-ae28-7297-8c16-218f5684b874", true, false, "Member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "019c8cc9-ae28-7297-8c16-2189777a6c06", 0, "019c8cc9-ae28-7297-8c16-218a9e5e9737", "admin@survey-basket.com", true, "Survey Basket", "Admin", false, null, "ADMIN@SURVEY-BASKET.COM", "ADMIN@SURVEY-BASKET.COM", "AQAAAAIAAYagAAAAEDPeG908eDH0EbFZ43pILNzkndEhnodfP1TS1QLlOUYcDSEdn0y/k2CLRI3PgxJeQQ==", null, false, "019c8cc9ae2872978c16218b0941a0fd", false, "admin@survey-basket.com" });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "permissions", "polls:read", "019c8cc9-ae28-7297-8c16-218c5d09c614" },
                    { 2, "permissions", "polls:add", "019c8cc9-ae28-7297-8c16-218c5d09c614" },
                    { 3, "permissions", "polls:update", "019c8cc9-ae28-7297-8c16-218c5d09c614" },
                    { 4, "permissions", "polls:delete", "019c8cc9-ae28-7297-8c16-218c5d09c614" },
                    { 5, "permissions", "questions:read", "019c8cc9-ae28-7297-8c16-218c5d09c614" },
                    { 6, "permissions", "questions:add", "019c8cc9-ae28-7297-8c16-218c5d09c614" },
                    { 7, "permissions", "questions:update", "019c8cc9-ae28-7297-8c16-218c5d09c614" },
                    { 8, "permissions", "users:read", "019c8cc9-ae28-7297-8c16-218c5d09c614" },
                    { 9, "permissions", "users:add", "019c8cc9-ae28-7297-8c16-218c5d09c614" },
                    { 10, "permissions", "users:update", "019c8cc9-ae28-7297-8c16-218c5d09c614" },
                    { 11, "permissions", "roles:read", "019c8cc9-ae28-7297-8c16-218c5d09c614" },
                    { 12, "permissions", "roles:add", "019c8cc9-ae28-7297-8c16-218c5d09c614" },
                    { 13, "permissions", "roles:update", "019c8cc9-ae28-7297-8c16-218c5d09c614" },
                    { 14, "permissions", "results:read", "019c8cc9-ae28-7297-8c16-218c5d09c614" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "019c8cc9-ae28-7297-8c16-218c5d09c614", "019c8cc9-ae28-7297-8c16-2189777a6c06" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "019c8cc9-ae28-7297-8c16-218e439b8b31");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "019c8cc9-ae28-7297-8c16-218c5d09c614", "019c8cc9-ae28-7297-8c16-2189777a6c06" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "019c8cc9-ae28-7297-8c16-218c5d09c614");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "019c8cc9-ae28-7297-8c16-2189777a6c06");
        }
    }
}
