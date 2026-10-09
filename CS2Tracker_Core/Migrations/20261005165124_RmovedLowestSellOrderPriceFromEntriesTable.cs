using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CS2Tracker_Core.Migrations
{
    /// <inheritdoc />
    public partial class RmovedLowestSellOrderPriceFromEntriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellOrderPice",
                table: "Entries");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "SellOrderPice",
                table: "Entries",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
