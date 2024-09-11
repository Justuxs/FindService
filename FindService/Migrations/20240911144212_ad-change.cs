using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindService.Migrations
{
    /// <inheritdoc />
    public partial class adchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrice",
                table: "Advertisements");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Advertisements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Advertisements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Advertisements");

            migrationBuilder.AddColumn<bool>(
                name: "IsPrice",
                table: "Advertisements",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
