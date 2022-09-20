using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Migration_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "UploadLog",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "UploadLog",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "File",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "File",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Batch",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Batch",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "UploadLog",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getutcdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "UploadLog",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getutcdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "File",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getutcdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "File",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getutcdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Batch",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getutcdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Batch",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getutcdate())");
        }
    }
}
