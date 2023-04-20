using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P2I.Migrations
{
    /// <inheritdoc />
    public partial class NumeroPDF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Numero",
                table: "Journeaux",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Journeaux");
        }
    }
}
