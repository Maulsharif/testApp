using Microsoft.EntityFrameworkCore.Migrations;

namespace wallet.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Currency = table.Column<string>(nullable: true),
                    Sum = table.Column<decimal>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                column: "UserId",
                value: 1);

            migrationBuilder.InsertData(
                table: "Users",
                column: "UserId",
                value: 2);

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Currency", "Sum", "UserId" },
                values: new object[] { 1, "USD", 2000m, 1 });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Currency", "Sum", "UserId" },
                values: new object[] { 2, "EUR", 5000m, 1 });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Currency", "Sum", "UserId" },
                values: new object[] { 3, "GBP", 50000m, 1 });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Currency", "Sum", "UserId" },
                values: new object[] { 4, "USD", 0m, 2 });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Currency", "Sum", "UserId" },
                values: new object[] { 5, "EUR", 20000m, 2 });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Currency", "Sum", "UserId" },
                values: new object[] { 6, "GBP", 56000m, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UserId",
                table: "Wallets",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
