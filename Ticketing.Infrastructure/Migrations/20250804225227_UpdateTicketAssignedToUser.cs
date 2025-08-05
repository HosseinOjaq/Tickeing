using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ticketing.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTicketAssignedToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5b482958-bf10-414a-8ec1-8f6bae60ed5c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ce4484a2-0865-40e3-9cde-e271a22b90a2"));

            migrationBuilder.AlterColumn<Guid>(
                name: "AssignedToUserId",
                table: "Tickets",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "Password", "Role" },
                values: new object[,]
                {
                    { new Guid("40e38eee-1a9c-43f9-902a-ddd31a84705b"), "hosseinojaq123@gmail.com", "hossein ojaq", "gmLTi09Kdn4/k9TfEdBXTWGqC3WYUz4ajgsJWZrtf18=", 2 },
                    { new Guid("7d5eadb4-e8e8-4f24-87fe-7468ba991ef8"), "admin@gmail.com", "admin", "JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("40e38eee-1a9c-43f9-902a-ddd31a84705b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7d5eadb4-e8e8-4f24-87fe-7468ba991ef8"));

            migrationBuilder.AlterColumn<Guid>(
                name: "AssignedToUserId",
                table: "Tickets",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "Password", "Role" },
                values: new object[,]
                {
                    { new Guid("5b482958-bf10-414a-8ec1-8f6bae60ed5c"), "admin@gmail.com", "admin", "JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=", 1 },
                    { new Guid("ce4484a2-0865-40e3-9cde-e271a22b90a2"), "hosseinojaq123@gmail.com", "hossein ojaq", "gmLTi09Kdn4/k9TfEdBXTWGqC3WYUz4ajgsJWZrtf18=", 2 }
                });
        }
    }
}
