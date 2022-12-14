using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiddenVillaServer.Migrations
{
    public partial class Fk_Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HotelRoomClient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Occupancy = table.Column<int>(type: "int", nullable: false),
                    RegularRate = table.Column<double>(type: "float", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SqFt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalDays = table.Column<double>(type: "float", nullable: false),
                    TotalAmount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelRoomClient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HotelClientImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelClientImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelClientImage_HotelRoomClient_RoomId",
                        column: x => x.RoomId,
                        principalTable: "HotelRoomClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageURI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HotelRoomClientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageURI", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageURI_HotelRoomClient_HotelRoomClientId",
                        column: x => x.HotelRoomClientId,
                        principalTable: "HotelRoomClient",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoomOrderingDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StripeSessionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualCheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualCheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalCost = table.Column<double>(type: "float", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    isPaymentSuccessful = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hotelRoomDTOId = table.Column<int>(type: "int", nullable: true),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomOrderingDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomOrderingDetails_HotelRoomClient_hotelRoomDTOId",
                        column: x => x.hotelRoomDTOId,
                        principalTable: "HotelRoomClient",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelClientImage_RoomId",
                table: "HotelClientImage",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageURI_HotelRoomClientId",
                table: "ImageURI",
                column: "HotelRoomClientId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomOrderingDetails_hotelRoomDTOId",
                table: "RoomOrderingDetails",
                column: "hotelRoomDTOId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelClientImage");

            migrationBuilder.DropTable(
                name: "ImageURI");

            migrationBuilder.DropTable(
                name: "RoomOrderingDetails");

            migrationBuilder.DropTable(
                name: "HotelRoomClient");
        }
    }
}
