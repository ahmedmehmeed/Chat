using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChattingApp.Migrations
{
    public partial class addRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
      table: "AspNetRoles",
      columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
      values: new object[] { Guid.NewGuid().ToString(), "User", "user".ToUpper(), Guid.NewGuid().ToString() }
      );

            migrationBuilder.InsertData(
             table: "AspNetRoles",
             columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
             values: new object[] { Guid.NewGuid().ToString(), "Admin", "admin".ToUpper(), Guid.NewGuid().ToString() }
             );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from [AspNetRoles]");
            

        }
    }
}
