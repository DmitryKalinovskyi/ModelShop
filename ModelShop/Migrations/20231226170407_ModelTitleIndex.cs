using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelShop.Migrations
{
    /// <inheritdoc />
    public partial class ModelTitleIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Models3D_Title",
                table: "Models3D",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Models3D_Title",
                table: "Models3D");
        }
    }
}
