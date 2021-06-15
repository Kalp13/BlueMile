using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlueMile.Certification.Data.Migrations
{
    public partial class AddedCertificationRequests_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestStates",
                schema: "boat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CertificationRequests",
                schema: "boat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BoatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestStateId = table.Column<int>(type: "int", nullable: false),
                    RequestedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CertificationRequests_Boats_BoatId",
                        column: x => x.BoatId,
                        principalSchema: "boat",
                        principalTable: "Boats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CertificationRequests_RequestStates_RequestStateId",
                        column: x => x.RequestStateId,
                        principalSchema: "boat",
                        principalTable: "RequestStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "boat",
                table: "RequestStates",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsActive", "ModifiedBy", "ModifiedOn", "Name", "Order" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Requested", 1 },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "In Progress", 2 },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Rejected", 3 },
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Approved", 4 },
                    { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Requested", 5 },
                    { 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Requested", 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CertificationRequests_BoatId",
                schema: "boat",
                table: "CertificationRequests",
                column: "BoatId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificationRequests_RequestStateId",
                schema: "boat",
                table: "CertificationRequests",
                column: "RequestStateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CertificationRequests",
                schema: "boat");

            migrationBuilder.DropTable(
                name: "RequestStates",
                schema: "boat");
        }
    }
}
