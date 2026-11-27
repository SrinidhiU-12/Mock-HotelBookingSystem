using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBooking.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHotelManagementSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FloorNumber",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaximumGuests",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RoomStatus",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MaximumGuests",
                table: "RoomCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Amenities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HotelAmenities",
                columns: table => new
                {
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    AmenityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelAmenities", x => new { x.HotelId, x.AmenityId });
                    table.ForeignKey(
                        name: "FK_HotelAmenities_Amenities_AmenityId",
                        column: x => x.AmenityId,
                        principalTable: "Amenities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelAmenities_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelAmenities_AmenityId",
                table: "HotelAmenities",
                column: "AmenityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelAmenities");

            migrationBuilder.DropTable(
                name: "Amenities");

            migrationBuilder.DropColumn(
                name: "FloorNumber",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "MaximumGuests",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomStatus",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "MaximumGuests",
                table: "RoomCategories");
        }
    }
}
