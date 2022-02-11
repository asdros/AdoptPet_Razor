using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdoptPet.Data.Migrations
{
    public partial class updateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animal",
                columns: table => new
                {
                    AnimalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Species = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal", x => x.AnimalId);
                });

            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    ProvinceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.ProvinceId);
                });

            migrationBuilder.CreateTable(
                name: "Breed",
                columns: table => new
                {
                    BreedId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnimalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breed", x => x.BreedId);
                    table.ForeignKey(
                        name: "FK_Breed_Animal_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animal",
                        principalColumn: "AnimalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    DistrictId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProvinceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.DistrictId);
                    table.ForeignKey(
                        name: "FK_District_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Province",
                        principalColumn: "ProvinceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    PlaceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistrictId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.PlaceId);
                    table.ForeignKey(
                        name: "FK_Places_District_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "District",
                        principalColumn: "DistrictId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ad",
                columns: table => new
                {
                    AdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NormalizedLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Sterilization = table.Column<bool>(type: "bit", nullable: false),
                    Deworming = table.Column<bool>(type: "bit", nullable: false),
                    AvailableFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BreedId = table.Column<int>(type: "int", nullable: false),
                    PlaceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ad", x => x.AdId);
                    table.ForeignKey(
                        name: "FK_Ad_Breed_BreedId",
                        column: x => x.BreedId,
                        principalTable: "Breed",
                        principalColumn: "BreedId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ad_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "PlaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    isPoster = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Image_Ad_AdId",
                        column: x => x.AdId,
                        principalTable: "Ad",
                        principalColumn: "AdId");
                });

            migrationBuilder.CreateTable(
                name: "WatchedItem",
                columns: table => new
                {
                    WatchedItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchedItem", x => x.WatchedItemId);
                    table.ForeignKey(
                        name: "FK_WatchedItem_Ad_AdId",
                        column: x => x.AdId,
                        principalTable: "Ad",
                        principalColumn: "AdId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Animal",
                columns: new[] { "AnimalId", "Species" },
                values: new object[,]
                {
                    { 1, "kot" },
                    { 2, "pies" }
                });

            migrationBuilder.InsertData(
                table: "Province",
                columns: new[] { "ProvinceId", "Name" },
                values: new object[,]
                {
                    { 1, "wielkopolskie" },
                    { 2, "mazowiecki" }
                });

            migrationBuilder.InsertData(
                table: "Breed",
                columns: new[] { "BreedId", "AnimalId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Maine coon" },
                    { 2, 1, "Syjamski" },
                    { 3, 2, "Owczarek" }
                });

            migrationBuilder.InsertData(
                table: "District",
                columns: new[] { "DistrictId", "Name", "ProvinceId" },
                values: new object[,]
                {
                    { 1, "złotowski", 1 },
                    { 2, "pilski", 1 },
                    { 3, "płocki", 2 }
                });

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "PlaceId", "DistrictId", "Name" },
                values: new object[] { 1, 1, "Złotów" });

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "PlaceId", "DistrictId", "Name" },
                values: new object[] { 2, 1, "Piła" });

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "PlaceId", "DistrictId", "Name" },
                values: new object[] { 3, 2, "Płock" });

            migrationBuilder.InsertData(
                table: "Ad",
                columns: new[] { "AdId", "AvailableFrom", "BreedId", "Description", "Deworming", "LastModified", "NormalizedLink", "PlaceId", "Sterilization", "Title" },
                values: new object[] { new Guid("8e23fb77-0a52-4e65-8d52-753ba1a6be4f"), new DateTime(2022, 2, 11, 22, 46, 14, 625, DateTimeKind.Local).AddTicks(2298), 2, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.", true, null, "slicz-Qe2GV", 1, true, "Śliczny kotek" });

            migrationBuilder.InsertData(
                table: "Ad",
                columns: new[] { "AdId", "AvailableFrom", "BreedId", "Description", "Deworming", "LastModified", "NormalizedLink", "PlaceId", "Sterilization", "Title" },
                values: new object[] { new Guid("25c3424d-79cd-40ca-a4e3-d71a14635141"), new DateTime(2022, 2, 11, 22, 46, 14, 625, DateTimeKind.Local).AddTicks(3819), 1, "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source.", true, null, "kot-b-u2ONx", 3, false, "kot Bajtek" });

            migrationBuilder.InsertData(
                table: "Ad",
                columns: new[] { "AdId", "AvailableFrom", "BreedId", "Description", "Deworming", "LastModified", "NormalizedLink", "PlaceId", "Sterilization", "Title" },
                values: new object[] { new Guid("dde9be6e-6ff8-4c9e-b844-387b7f4ec5a9"), new DateTime(2022, 2, 11, 22, 46, 14, 625, DateTimeKind.Local).AddTicks(3845), 3, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. ", true, null, "azore-3xkTE", 3, true, "Azorek" });

            migrationBuilder.InsertData(
                table: "Image",
                columns: new[] { "ImageId", "AdId", "Name", "Path", "isPoster" },
                values: new object[,]
                {
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new Guid("8e23fb77-0a52-4e65-8d52-753ba1a6be4f"), "cat.jpg", "${currentdir}\\..\\App_Data\\images", true },
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991872"), new Guid("25c3424d-79cd-40ca-a4e3-d71a14635141"), "dog.jpg", "${currentdir}\\..\\App_Data\\images", true },
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991871"), new Guid("dde9be6e-6ff8-4c9e-b844-387b7f4ec5a9"), "catNOP.jpg", "${currentdir}\\..\\App_Data\\images", true }
                });

            migrationBuilder.InsertData(
                table: "WatchedItem",
                columns: new[] { "WatchedItemId", "AdId", "OwnerId" },
                values: new object[,]
                {
                    { new Guid("17457cfd-63f5-4f6e-9ff1-b5ada7a4ea2a"), new Guid("8e23fb77-0a52-4e65-8d52-753ba1a6be4f"), null },
                    { new Guid("6a183cf8-7b8e-4bec-828c-7fd506880dde"), new Guid("25c3424d-79cd-40ca-a4e3-d71a14635141"), null },
                    { new Guid("57050acd-c26b-46fd-a0f7-0718b6c84eb4"), new Guid("25c3424d-79cd-40ca-a4e3-d71a14635141"), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ad_BreedId",
                table: "Ad",
                column: "BreedId");

            migrationBuilder.CreateIndex(
                name: "IX_Ad_PlaceId",
                table: "Ad",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Breed_AnimalId",
                table: "Breed",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_District_ProvinceId",
                table: "District",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_AdId",
                table: "Image",
                column: "AdId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_DistrictId",
                table: "Places",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchedItem_AdId",
                table: "WatchedItem",
                column: "AdId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "WatchedItem");

            migrationBuilder.DropTable(
                name: "Ad");

            migrationBuilder.DropTable(
                name: "Breed");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "Animal");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "Province");
        }
    }
}
