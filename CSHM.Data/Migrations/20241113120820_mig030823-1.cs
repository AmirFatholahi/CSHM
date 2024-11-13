using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSHM.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig0308231 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IDNUsers_PersonType_PersonTypeID",
                table: "IDNUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonType",
                table: "PersonType");

            migrationBuilder.RenameTable(
                name: "PersonType",
                newName: "PersonTypes");

            migrationBuilder.AddColumn<string>(
                name: "ProductTypeCode",
                table: "ProductTypes",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ProductCode",
                table: "Products",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "Products",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MaxPrice",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "MinPrice",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "PersonTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModificationDateTime",
                table: "PersonTypes",
                type: "DateTime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDateTime",
                table: "PersonTypes",
                type: "DateTime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonTypes",
                table: "PersonTypes",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_IDNUsers_PersonTypes_PersonTypeID",
                table: "IDNUsers",
                column: "PersonTypeID",
                principalTable: "PersonTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IDNUsers_PersonTypes_PersonTypeID",
                table: "IDNUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonTypes",
                table: "PersonTypes");

            migrationBuilder.DropColumn(
                name: "ProductTypeCode",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MaxPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MinPrice",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "PersonTypes",
                newName: "PersonType");

            migrationBuilder.AlterColumn<string>(
                name: "ProductCode",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "PersonType",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModificationDateTime",
                table: "PersonType",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "DateTime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDateTime",
                table: "PersonType",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DateTime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonType",
                table: "PersonType",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_IDNUsers_PersonType_PersonTypeID",
                table: "IDNUsers",
                column: "PersonTypeID",
                principalTable: "PersonType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
