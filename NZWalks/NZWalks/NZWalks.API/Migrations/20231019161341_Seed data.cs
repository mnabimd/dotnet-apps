using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class Seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("f10e38c5-32a2-4f13-b115-71214fe6af21"), "Easy" },
                    { new Guid("f10e38c5-32a2-4f13-b115-71214fe6af22"), "Medium" },
                    { new Guid("f10e38c5-32a2-4f13-b115-71214fe6af23"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("f10e38c5-32a2-4f13-b115-71214fe6af24"), "BOP", "Bay of Plenty", null },
                    { new Guid("f10e38c5-32a2-4f13-b115-71214fe6af25"), "WLG", "Wellington", null },
                    { new Guid("f10e38c5-32a2-4f13-b115-71214fe6af26"), "AKL", "Auckland", "akl.jpg" },
                    { new Guid("f10e38c5-32a2-4f13-b115-71214fe6af27"), "NSL", "Nelson", "nelson.jpg" },
                    { new Guid("f10e38c5-32a2-4f13-b115-71214fe6af28"), "KDR", "Kandahar", "kdr.jpg" },
                    { new Guid("f10e38c5-32a2-4f13-b115-71214fe6af29"), "KBL", "Kabul", "kbl.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("f10e38c5-32a2-4f13-b115-71214fe6af21"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("f10e38c5-32a2-4f13-b115-71214fe6af22"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("f10e38c5-32a2-4f13-b115-71214fe6af23"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f10e38c5-32a2-4f13-b115-71214fe6af24"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f10e38c5-32a2-4f13-b115-71214fe6af25"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f10e38c5-32a2-4f13-b115-71214fe6af26"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f10e38c5-32a2-4f13-b115-71214fe6af27"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f10e38c5-32a2-4f13-b115-71214fe6af28"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f10e38c5-32a2-4f13-b115-71214fe6af29"));
        }
    }
}
