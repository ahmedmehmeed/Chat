using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChattingApp.Migrations
{
    public partial class followeestable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Followees",
                columns: table => new
                {
                    SourceUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FolloweeUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Followees", x => new { x.SourceUserId, x.FolloweeUserId });
                    table.ForeignKey(
                        name: "FK_Followees_AspNetUsers_FolloweeUserId",
                        column: x => x.FolloweeUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Followees_AspNetUsers_SourceUserId",
                        column: x => x.SourceUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Followees_FolloweeUserId",
                table: "Followees",
                column: "FolloweeUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Followees");
        }
    }
}
