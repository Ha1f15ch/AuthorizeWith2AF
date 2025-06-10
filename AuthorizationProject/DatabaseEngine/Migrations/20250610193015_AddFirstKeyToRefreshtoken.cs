using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseEngine.Migrations
{
    /// <inheritdoc />
    public partial class AddFirstKeyToRefreshtoken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                schema: "dbo",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "Guid",
                schema: "dbo",
                table: "RefreshToken");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "dbo",
                table: "RefreshToken",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                schema: "dbo",
                table: "RefreshToken",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                schema: "dbo",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "dbo",
                table: "RefreshToken");

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                schema: "dbo",
                table: "RefreshToken",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                schema: "dbo",
                table: "RefreshToken",
                column: "Guid");
        }
    }
}
