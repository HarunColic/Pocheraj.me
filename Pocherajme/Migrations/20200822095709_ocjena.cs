using Microsoft.EntityFrameworkCore.Migrations;

namespace Pocherajme.Migrations
{
    public partial class ocjena : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostID",
                table: "Ratings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_PostID",
                table: "Ratings",
                column: "PostID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Posts_PostID",
                table: "Ratings",
                column: "PostID",
                principalTable: "Posts",
                principalColumn: "PostID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Posts_PostID",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_PostID",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "PostID",
                table: "Ratings");
        }
    }
}
