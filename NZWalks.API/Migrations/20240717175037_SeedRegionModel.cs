using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedRegionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("009d38c0-38ee-4a61-9c25-f5e6b2b76fd6"), "ER", "Eastern Region", "https://example.com/eastern-region.jpg" },
                    { new Guid("3a01809d-2db1-40fc-b0b7-924e00535f3b"), "WR", "Western Region", "https://example.com/western-region.jpg" },
                    { new Guid("58074fd8-c684-4354-b0d9-56989d73d713"), "NR", "Northern Region", "https://example.com/northern-region.jpg" },
                    { new Guid("d6bc01a1-c658-4c6f-8606-f8a7dc2048b0"), "SR", "Southern Region", "https://example.com/southern-region.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("009d38c0-38ee-4a61-9c25-f5e6b2b76fd6"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("3a01809d-2db1-40fc-b0b7-924e00535f3b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("58074fd8-c684-4354-b0d9-56989d73d713"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d6bc01a1-c658-4c6f-8606-f8a7dc2048b0"));
        }
    }
}
