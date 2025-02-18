using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSHM.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig140311301 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonOccupations_People_PersonID1",
                table: "PersonOccupations");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategoryTypes_Products_ProductID1",
                table: "ProductCategoryTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductComments_Products_ProductID1",
                table: "ProductComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductLables_Products_ProductID1",
                table: "ProductLables");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOccupations_Products_ProductID1",
                table: "ProductOccupations");

            migrationBuilder.DropIndex(
                name: "IX_ProductOccupations_ProductID1",
                table: "ProductOccupations");

            migrationBuilder.DropIndex(
                name: "IX_ProductLables_ProductID1",
                table: "ProductLables");

            migrationBuilder.DropIndex(
                name: "IX_ProductComments_ProductID1",
                table: "ProductComments");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategoryTypes_ProductID1",
                table: "ProductCategoryTypes");

            migrationBuilder.DropIndex(
                name: "IX_PersonOccupations_PersonID1",
                table: "PersonOccupations");

            migrationBuilder.DropColumn(
                name: "ProductID1",
                table: "ProductOccupations");

            migrationBuilder.DropColumn(
                name: "ProductID1",
                table: "ProductLables");

            migrationBuilder.DropColumn(
                name: "ProductID1",
                table: "ProductComments");

            migrationBuilder.DropColumn(
                name: "ProductID1",
                table: "ProductCategoryTypes");

            migrationBuilder.DropColumn(
                name: "PersonID1",
                table: "PersonOccupations");

            migrationBuilder.RenameColumn(
                name: "MetaDercreption",
                table: "Products",
                newName: "MetaDescription");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MetaDescription",
                table: "Products",
                newName: "MetaDercreption");

            migrationBuilder.AddColumn<int>(
                name: "ProductID1",
                table: "ProductOccupations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductID1",
                table: "ProductLables",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductID1",
                table: "ProductComments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductID1",
                table: "ProductCategoryTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonID1",
                table: "PersonOccupations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductOccupations_ProductID1",
                table: "ProductOccupations",
                column: "ProductID1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLables_ProductID1",
                table: "ProductLables",
                column: "ProductID1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_ProductID1",
                table: "ProductComments",
                column: "ProductID1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategoryTypes_ProductID1",
                table: "ProductCategoryTypes",
                column: "ProductID1");

            migrationBuilder.CreateIndex(
                name: "IX_PersonOccupations_PersonID1",
                table: "PersonOccupations",
                column: "PersonID1");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonOccupations_People_PersonID1",
                table: "PersonOccupations",
                column: "PersonID1",
                principalTable: "People",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategoryTypes_Products_ProductID1",
                table: "ProductCategoryTypes",
                column: "ProductID1",
                principalTable: "Products",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComments_Products_ProductID1",
                table: "ProductComments",
                column: "ProductID1",
                principalTable: "Products",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLables_Products_ProductID1",
                table: "ProductLables",
                column: "ProductID1",
                principalTable: "Products",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOccupations_Products_ProductID1",
                table: "ProductOccupations",
                column: "ProductID1",
                principalTable: "Products",
                principalColumn: "ID");
        }
    }
}
