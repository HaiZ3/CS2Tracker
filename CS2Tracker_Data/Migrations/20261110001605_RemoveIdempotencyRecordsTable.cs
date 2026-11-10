using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CS2Tracker_Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIdempotencyRecordsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Idempotency_Records");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Idempotency_Records",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusCode = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idempotency_Records", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Idempotency_Records_UserId_Key",
                table: "Idempotency_Records",
                columns: new[] { "UserId", "Key" },
                unique: true);
        }
    }
}
