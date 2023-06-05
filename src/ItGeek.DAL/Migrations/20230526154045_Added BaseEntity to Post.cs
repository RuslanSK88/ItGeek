using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItGeek.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedBaseEntitytoPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PostCategories",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PostAuthors",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostCategories",
                table: "PostCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostAuthors",
                table: "PostAuthors",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PostCategories",
                table: "PostCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostAuthors",
                table: "PostAuthors");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PostCategories");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PostAuthors");
        }
    }
}
