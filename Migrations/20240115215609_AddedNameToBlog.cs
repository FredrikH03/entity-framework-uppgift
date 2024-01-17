using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace entity_framework_uppgift.Migrations
{
    /// <inheritdoc />
    public partial class AddedNameToBlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Blogs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "Blogs");
        }
    }
}
