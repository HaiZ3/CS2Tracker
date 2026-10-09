using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CS2Tracker_Core.Migrations
{
    /// <inheritdoc />
    public partial class SkinInfoRenamedProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemFloat",
                table: "SkinInfos",
                newName: "Float");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Float",
                table: "SkinInfos",
                newName: "ItemFloat");
        }
    }
}
