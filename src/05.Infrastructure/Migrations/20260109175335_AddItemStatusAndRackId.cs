using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pertamina.SolutionTemplate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddItemStatusAndRackId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Items",
                newName: "RackId");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "RackId",
                table: "Items",
                newName: "Description");
        }
    }
}
