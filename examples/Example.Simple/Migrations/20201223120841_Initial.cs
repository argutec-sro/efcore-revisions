using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Example.Simple.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Revisions",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BatchID = table.Column<Guid>(nullable: false),
                    RecordID = table.Column<string>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Table = table.Column<string>(maxLength: 128, nullable: false),
                    Column = table.Column<string>(maxLength: 128, nullable: false),
                    Original = table.Column<string>(nullable: true),
                    Current = table.Column<string>(nullable: true),
                    User = table.Column<string>(maxLength: 128, nullable: true),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Revisions", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Revisions");
        }
    }
}
