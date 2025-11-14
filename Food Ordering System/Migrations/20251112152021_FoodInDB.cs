using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Food_Ordering_System.Migrations
{
    /// <inheritdoc />
    public partial class FoodInDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyNames",
                columns: table => new
                {
                    ComId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ComName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyNames", x => x.ComId);
                });

            migrationBuilder.CreateTable(
                name: "FoodItems",
                columns: table => new
                {
                    FoodId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FoodName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoodQty = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FoodPic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItems", x => x.FoodId);
                    table.ForeignKey(
                        name: "FK_FoodItems_CompanyNames_ComId",
                        column: x => x.ComId,
                        principalTable: "CompanyNames",
                        principalColumn: "ComId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodItems_ComId",
                table: "FoodItems",
                column: "ComId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodItems");

            migrationBuilder.DropTable(
                name: "CompanyNames");
        }
    }
}
