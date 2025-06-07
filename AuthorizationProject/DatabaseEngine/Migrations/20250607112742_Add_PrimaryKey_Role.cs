using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseEngine.Migrations
{
    /// <inheritdoc />
    public partial class Add_PrimaryKey_Role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_RoleCode",
                schema: "dbo",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_RoleCode",
                schema: "dbo",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                schema: "dict",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "RoleCode",
                schema: "dbo",
                table: "UserRole");

            migrationBuilder.AlterColumn<string>(
                name: "RoleCode",
                schema: "dict",
                table: "Role",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "dict",
                table: "Role",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                schema: "dict",
                table: "Role",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                schema: "dbo",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_RoleId",
                schema: "dbo",
                table: "UserRole",
                column: "RoleId",
                principalSchema: "dict",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_RoleId",
                schema: "dbo",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_RoleId",
                schema: "dbo",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                schema: "dict",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "dict",
                table: "Role");

            migrationBuilder.AddColumn<string>(
                name: "RoleCode",
                schema: "dbo",
                table: "UserRole",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "RoleCode",
                schema: "dict",
                table: "Role",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                schema: "dict",
                table: "Role",
                column: "RoleCode");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleCode",
                schema: "dbo",
                table: "UserRole",
                column: "RoleCode");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_RoleCode",
                schema: "dbo",
                table: "UserRole",
                column: "RoleCode",
                principalSchema: "dict",
                principalTable: "Role",
                principalColumn: "RoleCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
