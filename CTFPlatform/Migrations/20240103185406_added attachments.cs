using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTFPlatform.Migrations
{
    /// <inheritdoc />
    public partial class addedattachments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "attachments",
                table: "tasks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "deployed_env",
                table: "tasks",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "attachments",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "deployed_env",
                table: "tasks");
        }
    }
}
