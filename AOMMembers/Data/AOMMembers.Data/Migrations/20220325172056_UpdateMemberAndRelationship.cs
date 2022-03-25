using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AOMMembers.Data.Migrations
{
    public partial class UpdateMemberAndRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relationships_Members_MemberId",
                table: "Relationships");

            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                table: "Relationships",
                type: "nvarchar(40)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Members",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Relationships_Members_MemberId",
                table: "Relationships",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relationships_Members_MemberId",
                table: "Relationships");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "Members");

            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                table: "Relationships",
                type: "nvarchar(40)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)");

            migrationBuilder.AddForeignKey(
                name: "FK_Relationships_Members_MemberId",
                table: "Relationships",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");
        }
    }
}
