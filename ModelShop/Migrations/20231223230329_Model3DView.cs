﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelShop.Migrations
{
    /// <inheritdoc />
    public partial class Model3DView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "Models3D",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Views",
                table: "Models3D");
        }
    }
}
