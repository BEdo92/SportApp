﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportAndStepsApps.Migrations
{
    /// <inheritdoc />
    public partial class NumOfStepsDropped : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumOfSteps",
                table: "UserActivities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumOfSteps",
                table: "UserActivities",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
