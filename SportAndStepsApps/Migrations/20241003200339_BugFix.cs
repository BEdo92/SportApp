using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportAndStepsApps.Migrations
{
    /// <inheritdoc />
    public partial class BugFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SportId",
                table: "UserActivities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SportId",
                table: "UserActivities",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
