using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelShop.Migrations
{
    /// <inheritdoc />
    public partial class OrderChangesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_Models3D_AspNetUsers_ClientID",
                table: "Client_Models3D");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Client_Models3D",
                table: "Client_Models3D");

            migrationBuilder.DropIndex(
                name: "IX_Client_Models3D_OrderID",
                table: "Client_Models3D");

            migrationBuilder.DropColumn(
                name: "ClientID",
                table: "Client_Models3D");

            migrationBuilder.AddColumn<string>(
                name: "ClientID",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Client_Models3D",
                table: "Client_Models3D",
                columns: new[] { "OrderID", "Model3DID" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientID",
                table: "Orders",
                column: "ClientID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_ClientID",
                table: "Orders",
                column: "ClientID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_ClientID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ClientID",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Client_Models3D",
                table: "Client_Models3D");

            migrationBuilder.DropColumn(
                name: "ClientID",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "ClientID",
                table: "Client_Models3D",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Client_Models3D",
                table: "Client_Models3D",
                columns: new[] { "ClientID", "Model3DID" });

            migrationBuilder.CreateIndex(
                name: "IX_Client_Models3D_OrderID",
                table: "Client_Models3D",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_Models3D_AspNetUsers_ClientID",
                table: "Client_Models3D",
                column: "ClientID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
