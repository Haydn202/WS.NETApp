using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkSafeWebSite.Data.Migrations
{
    public partial class updateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Business");

            migrationBuilder.AlterColumn<DateTime>(
                name: "resolved_date",
                table: "WSNotice",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "current_notice_status",
                table: "WSNotice",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "current_resolve_status",
                table: "WSNotice",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "improvement_notice",
                table: "WSNotice",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "infringements",
                table: "WSNotice",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_inspection",
                table: "WSNotice",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "current_notice_status",
                table: "WSNotice");

            migrationBuilder.DropColumn(
                name: "current_resolve_status",
                table: "WSNotice");

            migrationBuilder.DropColumn(
                name: "improvement_notice",
                table: "WSNotice");

            migrationBuilder.DropColumn(
                name: "infringements",
                table: "WSNotice");

            migrationBuilder.DropColumn(
                name: "last_inspection",
                table: "WSNotice");

            migrationBuilder.AlterColumn<DateTime>(
                name: "resolved_date",
                table: "WSNotice",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Business",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    business_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    business_Number = table.Column<int>(type: "int", nullable: false),
                    industry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business", x => x.Id);
                });
        }
    }
}
