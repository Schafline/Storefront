using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Storefront.Migrations
{
    /// <inheritdoc />
    public partial class AddShippingInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShippingInfoId",
                table: "Orders",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ShippingInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    AddressLine1 = table.Column<string>(type: "TEXT", nullable: false),
                    AddressLine2 = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Postcode = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingInfos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingInfoId",
                table: "Orders",
                column: "ShippingInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingInfos_ShippingInfoId",
                table: "Orders",
                column: "ShippingInfoId",
                principalTable: "ShippingInfos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingInfos_ShippingInfoId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "ShippingInfos");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShippingInfoId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingInfoId",
                table: "Orders");
        }
    }
}
