using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    File = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "Not approved"),
                    KodRole = table.Column<int>(type: "int", nullable: false),
                    RoleUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_RoleUsers_RoleUserId",
                        column: x => x.RoleUserId,
                        principalTable: "RoleUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TypeDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KodUser = table.Column<int>(type: "int", nullable: false),
                    KodDocumentFile = table.Column<int>(type: "int", nullable: false),
                    DocumentFileId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypeDocuments_DocumentFiles_DocumentFileId",
                        column: x => x.DocumentFileId,
                        principalTable: "DocumentFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TypeDocuments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "RoleUsers",
                columns: new[] { "Id", "Role" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "TypeDocuments",
                columns: new[] { "Id", "DocumentFileId", "KodDocumentFile", "KodUser", "Type", "UserId" },
                values: new object[] { 1, null, 1, 2, "Passport", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "KodRole", "Password", "RoleUserId", "UserName" },
                values: new object[] { 1, "qwerty@gmail.com", 1, "4A7D1ED414474E4033AC29CCB8653D9B", null, "Иван" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "KodRole", "Password", "RoleUserId", "Status", "UserName" },
                values: new object[] { 2, "1234@gmail.com", 2, "B59C67BF196A4758191E42F76670CEBA", null, "Approved", "Николай" });

            migrationBuilder.CreateIndex(
                name: "IX_TypeDocuments_DocumentFileId",
                table: "TypeDocuments",
                column: "DocumentFileId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeDocuments_UserId",
                table: "TypeDocuments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleUserId",
                table: "Users",
                column: "RoleUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TypeDocuments");

            migrationBuilder.DropTable(
                name: "DocumentFiles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "RoleUsers");
        }
    }
}
