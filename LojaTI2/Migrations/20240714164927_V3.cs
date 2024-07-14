using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaTI2.Migrations
{
    /// <inheritdoc />
    public partial class V3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_CategoriaModel_CategoriaId",
                table: "Produto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriaModel",
                table: "CategoriaModel");

            migrationBuilder.RenameTable(
                name: "CategoriaModel",
                newName: "Categoria");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categoria",
                table: "Categoria",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Categoria_CategoriaId",
                table: "Produto",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Categoria_CategoriaId",
                table: "Produto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categoria",
                table: "Categoria");

            migrationBuilder.RenameTable(
                name: "Categoria",
                newName: "CategoriaModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriaModel",
                table: "CategoriaModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_CategoriaModel_CategoriaId",
                table: "Produto",
                column: "CategoriaId",
                principalTable: "CategoriaModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
