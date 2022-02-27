using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdoptPet.Data.Migrations
{
    public partial class ownerId_AdModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Ad",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Ad",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Ad",
                keyColumn: "AdId",
                keyValue: new Guid("25c3424d-79cd-40ca-a4e3-d71a14635141"),
                columns: new[] { "AvailableFrom", "NormalizedLink" },
                values: new object[] { new DateTime(2022, 2, 27, 14, 32, 27, 498, DateTimeKind.Local).AddTicks(1170), "kot-b-KWMfu" });

            migrationBuilder.UpdateData(
                table: "Ad",
                keyColumn: "AdId",
                keyValue: new Guid("8e23fb77-0a52-4e65-8d52-753ba1a6be4f"),
                columns: new[] { "AvailableFrom", "NormalizedLink" },
                values: new object[] { new DateTime(2022, 2, 27, 14, 32, 27, 497, DateTimeKind.Local).AddTicks(9512), "slicz-6Ez4M" });

            migrationBuilder.UpdateData(
                table: "Ad",
                keyColumn: "AdId",
                keyValue: new Guid("dde9be6e-6ff8-4c9e-b844-387b7f4ec5a9"),
                columns: new[] { "AvailableFrom", "NormalizedLink" },
                values: new object[] { new DateTime(2022, 2, 27, 14, 32, 27, 498, DateTimeKind.Local).AddTicks(1259), "azore-ARV_S" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Ad");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Ad");

            migrationBuilder.UpdateData(
                table: "Ad",
                keyColumn: "AdId",
                keyValue: new Guid("25c3424d-79cd-40ca-a4e3-d71a14635141"),
                columns: new[] { "AvailableFrom", "NormalizedLink" },
                values: new object[] { new DateTime(2022, 2, 27, 14, 30, 2, 344, DateTimeKind.Local).AddTicks(4627), "kot-b-o9_Qq" });

            migrationBuilder.UpdateData(
                table: "Ad",
                keyColumn: "AdId",
                keyValue: new Guid("8e23fb77-0a52-4e65-8d52-753ba1a6be4f"),
                columns: new[] { "AvailableFrom", "NormalizedLink" },
                values: new object[] { new DateTime(2022, 2, 27, 14, 30, 2, 344, DateTimeKind.Local).AddTicks(3137), "slicz-LZ-ah" });

            migrationBuilder.UpdateData(
                table: "Ad",
                keyColumn: "AdId",
                keyValue: new Guid("dde9be6e-6ff8-4c9e-b844-387b7f4ec5a9"),
                columns: new[] { "AvailableFrom", "NormalizedLink" },
                values: new object[] { new DateTime(2022, 2, 27, 14, 30, 2, 344, DateTimeKind.Local).AddTicks(4650), "azore-yAvk-" });
        }
    }
}
