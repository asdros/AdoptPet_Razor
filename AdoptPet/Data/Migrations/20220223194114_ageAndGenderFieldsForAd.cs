using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdoptPet.Data.Migrations
{
    public partial class ageAndGenderFieldsForAd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Breed",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Species",
                table: "Animal",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<byte>(
                name: "AgeAnimal",
                table: "Ad",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<int>(
                name: "GenderAnimal",
                table: "Ad",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Ad",
                keyColumn: "AdId",
                keyValue: new Guid("25c3424d-79cd-40ca-a4e3-d71a14635141"),
                columns: new[] { "AvailableFrom", "NormalizedLink" },
                values: new object[] { new DateTime(2022, 2, 23, 20, 41, 13, 951, DateTimeKind.Local).AddTicks(9109), "kot-b-67ggq" });

            migrationBuilder.UpdateData(
                table: "Ad",
                keyColumn: "AdId",
                keyValue: new Guid("8e23fb77-0a52-4e65-8d52-753ba1a6be4f"),
                columns: new[] { "AvailableFrom", "NormalizedLink" },
                values: new object[] { new DateTime(2022, 2, 23, 20, 41, 13, 951, DateTimeKind.Local).AddTicks(7607), "slicz-M55AQ" });

            migrationBuilder.UpdateData(
                table: "Ad",
                keyColumn: "AdId",
                keyValue: new Guid("dde9be6e-6ff8-4c9e-b844-387b7f4ec5a9"),
                columns: new[] { "AvailableFrom", "NormalizedLink" },
                values: new object[] { new DateTime(2022, 2, 23, 20, 41, 13, 951, DateTimeKind.Local).AddTicks(9132), "azore-gzoCt" });

            migrationBuilder.UpdateData(
                table: "Image",
                keyColumn: "ImageId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                column: "Path",
                value: null);

            migrationBuilder.UpdateData(
                table: "Image",
                keyColumn: "ImageId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991871"),
                column: "Path",
                value: null);

            migrationBuilder.UpdateData(
                table: "Image",
                keyColumn: "ImageId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991872"),
                column: "Path",
                value: null);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgeAnimal",
                table: "Ad");

            migrationBuilder.DropColumn(
                name: "GenderAnimal",
                table: "Ad");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Breed",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Species",
                table: "Animal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Ad",
                keyColumn: "AdId",
                keyValue: new Guid("25c3424d-79cd-40ca-a4e3-d71a14635141"),
                columns: new[] { "AvailableFrom", "NormalizedLink" },
                values: new object[] { new DateTime(2022, 2, 11, 22, 46, 14, 625, DateTimeKind.Local).AddTicks(3819), "kot-b-u2ONx" });

            migrationBuilder.UpdateData(
                table: "Ad",
                keyColumn: "AdId",
                keyValue: new Guid("8e23fb77-0a52-4e65-8d52-753ba1a6be4f"),
                columns: new[] { "AvailableFrom", "NormalizedLink" },
                values: new object[] { new DateTime(2022, 2, 11, 22, 46, 14, 625, DateTimeKind.Local).AddTicks(2298), "slicz-Qe2GV" });

            migrationBuilder.UpdateData(
                table: "Ad",
                keyColumn: "AdId",
                keyValue: new Guid("dde9be6e-6ff8-4c9e-b844-387b7f4ec5a9"),
                columns: new[] { "AvailableFrom", "NormalizedLink" },
                values: new object[] { new DateTime(2022, 2, 11, 22, 46, 14, 625, DateTimeKind.Local).AddTicks(3845), "azore-3xkTE" });

            migrationBuilder.UpdateData(
                table: "Image",
                keyColumn: "ImageId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                column: "Path",
                value: "${currentdir}\\..\\App_Data\\images");

            migrationBuilder.UpdateData(
                table: "Image",
                keyColumn: "ImageId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991871"),
                column: "Path",
                value: "${currentdir}\\..\\App_Data\\images");

            migrationBuilder.UpdateData(
                table: "Image",
                keyColumn: "ImageId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991872"),
                column: "Path",
                value: "${currentdir}\\..\\App_Data\\images");
        }
    }
}
