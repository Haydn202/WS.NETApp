using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkSafeWebSite.Data.Migrations
{
    public partial class Inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WSNotice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    business_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    business_Number = table.Column<int>(type: "int", nullable: false),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    industry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date_Issued = table.Column<DateTime>(type: "datetime2", nullable: false),
                    issued_To = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    notice_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    topic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pdf_URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    resolved_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    resolved_pdf_URL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WSNotice", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WSNotice");
        }
    }
}
