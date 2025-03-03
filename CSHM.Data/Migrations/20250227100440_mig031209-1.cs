using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSHM.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig0312091 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IDNPages_ChannelTypes_ChannelTypeID",
                table: "IDNPages");

            migrationBuilder.DropTable(
                name: "ChannelTypes");

            migrationBuilder.RenameColumn(
                name: "ChannelTypeID",
                table: "IDNPages",
                newName: "PageTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_IDNPages_ChannelTypeID",
                table: "IDNPages",
                newName: "IX_IDNPages_PageTypeID");

            migrationBuilder.AddColumn<bool>(
                name: "IsNew",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRecommended",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSoon",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPin",
                table: "Blogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PageTypes",
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
                    table.PrimaryKey("PK_PageTypes", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_IDNPages_PageTypes_PageTypeID",
                table: "IDNPages",
                column: "PageTypeID",
                principalTable: "PageTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IDNPages_PageTypes_PageTypeID",
                table: "IDNPages");

            migrationBuilder.DropTable(
                name: "PageTypes");

            migrationBuilder.DropColumn(
                name: "IsNew",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsRecommended",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsSelected",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsSoon",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsPin",
                table: "Blogs");

            migrationBuilder.RenameColumn(
                name: "PageTypeID",
                table: "IDNPages",
                newName: "ChannelTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_IDNPages_PageTypeID",
                table: "IDNPages",
                newName: "IX_IDNPages_ChannelTypeID");

            migrationBuilder.CreateTable(
                name: "ChannelTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    TicketumSectionTypeID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelTypes", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_IDNPages_ChannelTypes_ChannelTypeID",
                table: "IDNPages",
                column: "ChannelTypeID",
                principalTable: "ChannelTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
