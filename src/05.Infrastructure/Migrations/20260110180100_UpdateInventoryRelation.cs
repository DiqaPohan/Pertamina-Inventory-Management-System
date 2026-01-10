using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pertamina.SolutionTemplate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInventoryRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RackSlots");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "Items",
                newSchema: "dbo");

            migrationBuilder.AlterColumn<string>(
                name: "RackId",
                schema: "dbo",
                table: "Items",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Racks",
                schema: "dbo",
                columns: table => new
                {
                    RackId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racks", x => x.RackId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_RackId",
                schema: "dbo",
                table: "Items",
                column: "RackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Racks_RackId",
                schema: "dbo",
                table: "Items",
                column: "RackId",
                principalSchema: "dbo",
                principalTable: "Racks",
                principalColumn: "RackId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Racks_RackId",
                schema: "dbo",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Racks",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Items_RackId",
                schema: "dbo",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "Items",
                schema: "dbo",
                newName: "Items");

            migrationBuilder.AlterColumn<string>(
                name: "RackId",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "RackSlots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PositionX = table.Column<int>(type: "int", nullable: false),
                    PositionY = table.Column<int>(type: "int", nullable: false),
                    RackCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RackSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RackSlots_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RackSlots_ItemId",
                table: "RackSlots",
                column: "ItemId");
        }
    }
}
