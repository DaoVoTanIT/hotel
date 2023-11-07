using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hotel_api.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhysicalFacility",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsHighLight = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalFacility", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomFacility",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoomId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhysicalFacilityId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomFacility", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomFacility_PhysicalFacility_PhysicalFacilityId",
                        column: x => x.PhysicalFacilityId,
                        principalTable: "PhysicalFacility",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomFacility_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomFacility_PhysicalFacilityId",
                table: "RoomFacility",
                column: "PhysicalFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomFacility_RoomId",
                table: "RoomFacility",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomFacility");

            migrationBuilder.DropTable(
                name: "PhysicalFacility");
        }
    }
}
