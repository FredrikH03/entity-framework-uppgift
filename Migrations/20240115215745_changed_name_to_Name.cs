using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace entity_framework_uppgift.Migrations
{
    /// <inheritdoc />
    public partial class changed_name_to_Name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Blogs",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Blogs",
                newName: "name");
        }
    }
}
