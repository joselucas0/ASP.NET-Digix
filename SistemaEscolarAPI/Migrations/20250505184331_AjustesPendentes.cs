using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaEscolarAPI.Migrations
{
    /// <inheritdoc />
    public partial class AjustesPendentes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DisciplinaAlunoCursos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Nota",
                table: "DisciplinaAlunoCursos",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "DisciplinaAlunoCursos");

            migrationBuilder.DropColumn(
                name: "Nota",
                table: "DisciplinaAlunoCursos");
        }
    }
}
