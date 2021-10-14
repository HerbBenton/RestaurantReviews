using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantReviews.API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Hours = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AverageRating = table.Column<double>(type: "float", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "AverageRating", "Description", "Hours", "Name" },
                values: new object[] { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), "123 Main St. Anytown NY 61732", 0.0, "Americana", "M-F 8AM - 9PM", "Old Templeton" });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "AverageRating", "Description", "Hours", "Name" },
                values: new object[] { new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), "400 E. Salvadora Dr. Anywhere NM 52431", 0.0, "Mexicana", "Saturday & Sunday 11AM - 7PM", "Taco Crunchers" });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "Rank", "RestaurantId" },
                values: new object[,]
                {
                    { new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"), 1, new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35") },
                    { new Guid("d8663e5e-7494-4f81-8739-6e0de1bea7ee"), 5, new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35") },
                    { new Guid("40ff5488-fdab-45b5-bc3a-14302d59869a"), 2, new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96") },
                    { new Guid("d173e20d-159e-4127-9ce9-b0ac2564ad97"), 3, new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_RestaurantId",
                table: "Ratings",
                column: "RestaurantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Restaurants");
        }
    }
}
