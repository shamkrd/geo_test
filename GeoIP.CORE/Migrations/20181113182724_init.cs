using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GeoIP.CORE.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeoInfos",
                columns: table => new
                {
                    id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ip = table.Column<string>(type: "text", nullable: true),
                    latitude = table.Column<double>(type: "float8", nullable: false),
                    longitude = table.Column<double>(type: "float8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoInfos", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeoInfos");
        }
    }
}
