using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pokemon.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Rate",
                table: "Reviews",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Reviews");
        }
    }
}
