using Microsoft.EntityFrameworkCore.Migrations;

namespace AdoptPet.Data.Migrations
{
    public partial class chatEntitiesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "Message",
                newName: "SendByUserId");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Chat",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Chat");

            migrationBuilder.RenameColumn(
                name: "SendByUserId",
                table: "Message",
                newName: "ReceiverId");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Message",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
