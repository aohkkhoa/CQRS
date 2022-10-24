using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class addColumnBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userName",
                table: "UserTable",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "UserTable",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "UserTable",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "UserTable",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "UserTable",
                newName: "UserId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Book",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Book");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "UserTable",
                newName: "userName");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "UserTable",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "UserTable",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "UserTable",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserTable",
                newName: "userId");
        }
    }
}
