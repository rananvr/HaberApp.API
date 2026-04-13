using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HaberApp.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryAndComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsApproved",
                table: "Comments",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "CommentBody",
                table: "Comments",
                newName: "Content");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Comments",
                newName: "IsApproved");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Comments",
                newName: "CommentBody");
        }
    }
}
