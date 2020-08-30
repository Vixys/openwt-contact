using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenWT.Contact.Data.Migrations
{
    public partial class UserManyContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Contact_ContactId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ContactId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ContactId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ContactId",
                table: "AspNetUsers",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Contact_ContactId",
                table: "AspNetUsers",
                column: "ContactId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
