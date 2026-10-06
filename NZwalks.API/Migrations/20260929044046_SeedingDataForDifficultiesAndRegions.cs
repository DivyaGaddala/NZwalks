using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZwalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataForDifficultiesAndRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "DifficultyId", "Name" },
                values: new object[,]
                {
                    { new Guid("2b91db42-8de6-4fa7-8dd4-3ca6a33b169d"), "Easy" },
                    { new Guid("9b6b8135-fe9f-4e78-9e87-fe4ff654dd5c"), "Difficult" },
                    { new Guid("eed3ae4a-5e16-4d2d-a11b-a98f33d6430c"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageurl" },
                values: new object[,]
                {
                    { new Guid("5557a8df-6337-46df-848b-0064d8198282"), "QTW", "QueensTown", "https://images.pexels.com/photos/33981213/pexels-photo-33981213.jpeg" },
                    { new Guid("c04b8d78-ccc8-4f78-8f99-13fff48dadda"), "WLG", "Willingston", "https://images.pexels.com/photos/17824133/pexels-photo-17824133.jpeg" },
                    { new Guid("e1648d68-c6fb-4342-a0b1-2d46fe23ee30"), "AKL", "Auckland", "https://www.pexels.com/video/stunning-aerial-view-of-lake-wolfgang-austria-39144011/" },
                    { new Guid("ea1013fe-678d-4eec-86ac-991a77066dc5"), "NLS", "Nelson", "https://images.pexels.com/photos/32971907/pexels-photo-32971907.jpeg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "DifficultyId",
                keyValue: new Guid("2b91db42-8de6-4fa7-8dd4-3ca6a33b169d"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "DifficultyId",
                keyValue: new Guid("9b6b8135-fe9f-4e78-9e87-fe4ff654dd5c"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "DifficultyId",
                keyValue: new Guid("eed3ae4a-5e16-4d2d-a11b-a98f33d6430c"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("5557a8df-6337-46df-848b-0064d8198282"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("c04b8d78-ccc8-4f78-8f99-13fff48dadda"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("e1648d68-c6fb-4342-a0b1-2d46fe23ee30"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("ea1013fe-678d-4eec-86ac-991a77066dc5"));
        }
    }
}
