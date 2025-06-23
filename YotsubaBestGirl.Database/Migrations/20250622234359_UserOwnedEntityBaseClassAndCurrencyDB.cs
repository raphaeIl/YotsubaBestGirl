using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YotsubaBestGirl.Database.Migrations
{
    /// <inheritdoc />
    public partial class UserOwnedEntityBaseClassAndCurrencyDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FreeCoin",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PayCoin",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FreeCoin",
                table: "users");

            migrationBuilder.DropColumn(
                name: "PayCoin",
                table: "users");
        }
    }
}
