using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChattingApp.Migrations
{
    public partial class addmigrationnewrole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
               migrationBuilder.InsertData(
             table: "AspNetRoles",
             columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
             values: new object[] { Guid.NewGuid().ToString(), "Moderator", "Moderator".ToUpper(), Guid.NewGuid().ToString() }
             );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
