using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelShop.Migrations
{
    /// <inheritdoc />
    public partial class FollowerMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientFollowers",
                columns: table => new
                {
                    ClientFollowerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FollowerID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FollowingID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientFollowers", x => x.ClientFollowerId);
                    table.ForeignKey(
                        name: "FK_ClientFollowers_AspNetUsers_FollowerID",
                        column: x => x.FollowerID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClientFollowers_AspNetUsers_FollowingID",
                        column: x => x.FollowingID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientFollowers_FollowerID",
                table: "ClientFollowers",
                column: "FollowerID");

            migrationBuilder.CreateIndex(
                name: "IX_ClientFollowers_FollowingID",
                table: "ClientFollowers",
                column: "FollowingID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientFollowers");
        }
    }
}
