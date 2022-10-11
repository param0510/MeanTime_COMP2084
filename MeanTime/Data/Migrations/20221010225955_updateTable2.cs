using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeanTime.Data.Migrations
{
    public partial class updateTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Genres",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Genres");
        }
    }
}
