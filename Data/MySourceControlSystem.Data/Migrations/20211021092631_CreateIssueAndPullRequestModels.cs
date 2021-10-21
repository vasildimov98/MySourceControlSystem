using Microsoft.EntityFrameworkCore.Migrations;

namespace MySourceControlSystem.Data.Migrations
{
    public partial class CreateIssueAndPullRequestModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PullRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Issues",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PullRequests_UserId",
                table: "PullRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_UserId",
                table: "Issues",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_AspNetUsers_UserId",
                table: "Issues",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PullRequests_AspNetUsers_UserId",
                table: "PullRequests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_AspNetUsers_UserId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_PullRequests_AspNetUsers_UserId",
                table: "PullRequests");

            migrationBuilder.DropIndex(
                name: "IX_PullRequests_UserId",
                table: "PullRequests");

            migrationBuilder.DropIndex(
                name: "IX_Issues_UserId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PullRequests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Issues");
        }
    }
}
