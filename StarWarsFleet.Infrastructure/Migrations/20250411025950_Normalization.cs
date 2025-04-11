using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarWarsFleet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Normalization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Faction",
                table: "SpaceStations");

            migrationBuilder.RenameColumn(
                name: "CrewCapacity",
                table: "SpaceStations",
                newName: "Capacity");

            migrationBuilder.AddColumn<Guid>(
                name: "FactionId",
                table: "SpaceStations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Factions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Garages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SpaceStationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Garages_SpaceStations_SpaceStationId",
                        column: x => x.SpaceStationId,
                        principalTable: "SpaceStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DockingSlots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SlotNumber = table.Column<int>(type: "int", nullable: false),
                    GarageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShipId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DockingSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DockingSlots_Garages_GarageId",
                        column: x => x.GarageId,
                        principalTable: "Garages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DockingSlots_Ships_ShipId",
                        column: x => x.ShipId,
                        principalTable: "Ships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpaceStations_FactionId",
                table: "SpaceStations",
                column: "FactionId");

            migrationBuilder.CreateIndex(
                name: "IX_DockingSlots_GarageId",
                table: "DockingSlots",
                column: "GarageId");

            migrationBuilder.CreateIndex(
                name: "IX_DockingSlots_ShipId",
                table: "DockingSlots",
                column: "ShipId",
                unique: true,
                filter: "[ShipId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Garages_SpaceStationId",
                table: "Garages",
                column: "SpaceStationId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpaceStations_Factions_FactionId",
                table: "SpaceStations",
                column: "FactionId",
                principalTable: "Factions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpaceStations_Ships_Id",
                table: "SpaceStations",
                column: "Id",
                principalTable: "Ships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpaceStations_Factions_FactionId",
                table: "SpaceStations");

            migrationBuilder.DropForeignKey(
                name: "FK_SpaceStations_Ships_Id",
                table: "SpaceStations");

            migrationBuilder.DropTable(
                name: "DockingSlots");

            migrationBuilder.DropTable(
                name: "Factions");

            migrationBuilder.DropTable(
                name: "Garages");

            migrationBuilder.DropTable(
                name: "Ships");

            migrationBuilder.DropIndex(
                name: "IX_SpaceStations_FactionId",
                table: "SpaceStations");

            migrationBuilder.DropColumn(
                name: "FactionId",
                table: "SpaceStations");

            migrationBuilder.RenameColumn(
                name: "Capacity",
                table: "SpaceStations",
                newName: "CrewCapacity");

            migrationBuilder.AddColumn<string>(
                name: "Faction",
                table: "SpaceStations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
