using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSHM.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig0312041 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublisherID",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GenreTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PropertyTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductGenreTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    GenreTypeID = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGenreTypes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductGenreTypes_GenreTypes_GenreTypeID",
                        column: x => x.GenreTypeID,
                        principalTable: "GenreTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductGenreTypes_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductPropertyTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    PropertyTypeID = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPropertyTypes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductPropertyTypes_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductPropertyTypes_PropertyTypes_PropertyTypeID",
                        column: x => x.PropertyTypeID,
                        principalTable: "PropertyTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_PublisherID",
                table: "Blogs",
                column: "PublisherID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGenreTypes_GenreTypeID",
                table: "ProductGenreTypes",
                column: "GenreTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGenreTypes_ProductID",
                table: "ProductGenreTypes",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPropertyTypes_ProductID",
                table: "ProductPropertyTypes",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPropertyTypes_PropertyTypeID",
                table: "ProductPropertyTypes",
                column: "PropertyTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Publishers_PublisherID",
                table: "Blogs",
                column: "PublisherID",
                principalTable: "Publishers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Publishers_PublisherID",
                table: "Blogs");

            migrationBuilder.DropTable(
                name: "ProductGenreTypes");

            migrationBuilder.DropTable(
                name: "ProductPropertyTypes");

            migrationBuilder.DropTable(
                name: "GenreTypes");

            migrationBuilder.DropTable(
                name: "PropertyTypes");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_PublisherID",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "PublisherID",
                table: "Blogs");
        }
    }
}
