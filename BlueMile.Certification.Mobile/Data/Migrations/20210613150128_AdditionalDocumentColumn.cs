using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlueMile.Certification.Data.Migrations
{
    public partial class AdditionalDocumentColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                schema: "leg",
                table: "LegalEntityDocuments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                schema: "item",
                table: "ItemDocuments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                schema: "boat",
                table: "BoatDocuments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8f0fa92c-6b34-4c88-8504-be6b6a4ec52f"),
                columns: new[] { "Email", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "admin@bluemile.co.za", "ADMIN@BLUEMILE.CO.ZA", "ADMIN@BLUEMILE.CO.ZA", "admin@bluemile.co.za" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                schema: "leg",
                table: "LegalEntityDocuments");

            migrationBuilder.DropColumn(
                name: "FilePath",
                schema: "item",
                table: "ItemDocuments");

            migrationBuilder.DropColumn(
                name: "FilePath",
                schema: "boat",
                table: "BoatDocuments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8f0fa92c-6b34-4c88-8504-be6b6a4ec52f"),
                columns: new[] { "Email", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "Admin@pinaster.co.za", "ADMIN@PINASTER.CO.ZA", "ADMIN@PINASTER.CO.ZA", "Admin@pinaster.co.za" });
        }
    }
}
