using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Example.Simple.Migrations
{
    /// <inheritdoc />
    public partial class BatchColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BatchOperation",
                table: "Revisions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BatchTables",
                table: "Revisions",
                type: "character varying(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReleaseDate",
                table: "Books",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 20, 12, 9, 58, 481, DateTimeKind.Utc).AddTicks(2074),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 6, 21, 19, 41, 18, 469, DateTimeKind.Utc).AddTicks(991));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatchOperation",
                table: "Revisions");

            migrationBuilder.DropColumn(
                name: "BatchTables",
                table: "Revisions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReleaseDate",
                table: "Books",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 21, 19, 41, 18, 469, DateTimeKind.Utc).AddTicks(991),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 7, 20, 12, 9, 58, 481, DateTimeKind.Utc).AddTicks(2074));
        }
    }
}
