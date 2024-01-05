using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTFPlatform.Migrations
{
    /// <inheritdoc />
    public partial class addedcountofsolves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "solves",
                table: "tasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "solves",
                table: "tasks");
        }
    }
}
