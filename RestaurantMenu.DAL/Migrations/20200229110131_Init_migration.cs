using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantMenu.DAL.Migrations
{
    public partial class Init_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dishes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddingDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Composition = table.Column<string>(type: "varchar(max)", nullable: true),
                    Description = table.Column<string>(type: "varchar(500)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    Mass = table.Column<int>(nullable: false),
                    CalorieContent = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    CookingTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dishes", x => x.Id);
                    table.UniqueConstraint("AK_Dishes_Name", x => x.Name);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dishes");
        }
    }
}
