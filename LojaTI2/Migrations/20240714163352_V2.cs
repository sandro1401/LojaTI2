using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaTI2.Migrations
{
    /// <inheritdoc />
    public partial class V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPedido_ProdutoModel_ProdutoId",
                table: "ItemPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoModel_CategoriaModel_CategoriaId",
                table: "ProdutoModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProdutoModel",
                table: "ProdutoModel");

            migrationBuilder.RenameTable(
                name: "ProdutoModel",
                newName: "Produto");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutoModel_CategoriaId",
                table: "Produto",
                newName: "IX_Produto_CategoriaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Produto",
                table: "Produto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPedido_Produto_ProdutoId",
                table: "ItemPedido",
                column: "ProdutoId",
                principalTable: "Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_CategoriaModel_CategoriaId",
                table: "Produto",
                column: "CategoriaId",
                principalTable: "CategoriaModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPedido_Produto_ProdutoId",
                table: "ItemPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_Produto_CategoriaModel_CategoriaId",
                table: "Produto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Produto",
                table: "Produto");

            migrationBuilder.RenameTable(
                name: "Produto",
                newName: "ProdutoModel");

            migrationBuilder.RenameIndex(
                name: "IX_Produto_CategoriaId",
                table: "ProdutoModel",
                newName: "IX_ProdutoModel_CategoriaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProdutoModel",
                table: "ProdutoModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPedido_ProdutoModel_ProdutoId",
                table: "ItemPedido",
                column: "ProdutoId",
                principalTable: "ProdutoModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoModel_CategoriaModel_CategoriaId",
                table: "ProdutoModel",
                column: "CategoriaId",
                principalTable: "CategoriaModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
