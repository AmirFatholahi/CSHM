using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSHM.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig0311162 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChannelTypeID",
                table: "IDNPages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ChannelTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketumSectionTypeID = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ChannelTypes", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IDNPages_ChannelTypeID",
                table: "IDNPages",
                column: "ChannelTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_IDNPages_ChannelTypes_ChannelTypeID",
                table: "IDNPages",
                column: "ChannelTypeID",
                principalTable: "ChannelTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IDNPages_ChannelTypes_ChannelTypeID",
                table: "IDNPages");

            migrationBuilder.DropTable(
                name: "ChannelTypes");

            migrationBuilder.DropIndex(
                name: "IX_IDNPages_ChannelTypeID",
                table: "IDNPages");

            migrationBuilder.DropColumn(
                name: "ChannelTypeID",
                table: "IDNPages");
        }
    }
}
