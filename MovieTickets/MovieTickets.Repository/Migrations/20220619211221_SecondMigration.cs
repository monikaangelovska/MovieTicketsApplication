using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieTickets.Repository.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieInshoppingCarts",
                table: "MovieInshoppingCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieInOrder",
                table: "MovieInOrder");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieInshoppingCarts",
                table: "MovieInshoppingCarts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieInOrder",
                table: "MovieInOrder",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MovieInshoppingCarts_MovieId",
                table: "MovieInshoppingCarts",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieInOrder_MovieId",
                table: "MovieInOrder",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieInshoppingCarts",
                table: "MovieInshoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_MovieInshoppingCarts_MovieId",
                table: "MovieInshoppingCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieInOrder",
                table: "MovieInOrder");

            migrationBuilder.DropIndex(
                name: "IX_MovieInOrder_MovieId",
                table: "MovieInOrder");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieInshoppingCarts",
                table: "MovieInshoppingCarts",
                columns: new[] { "MovieId", "ShoppingCartId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieInOrder",
                table: "MovieInOrder",
                columns: new[] { "MovieId", "OrderId" });
        }
    }
}
