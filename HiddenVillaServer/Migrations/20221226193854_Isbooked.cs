﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiddenVillaServer.Migrations
{
    public partial class Isbooked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBooked",
                table: "HotelRoomClient",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBooked",
                table: "HotelRoomClient");
        }
    }
}
