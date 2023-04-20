using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P2I.Migrations
{
    /// <inheritdoc />
    public partial class ModifARTICLEILLUSTRATION : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Illustration",
                table: "Articles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Illustration",
                table: "Articles");
        }
    }
}
