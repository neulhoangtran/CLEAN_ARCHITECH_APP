using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateUserDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PermissionCategories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionCategories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PermissionCategoryID = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Permissions_PermissionCategories_PermissionCategoryID",
                        column: x => x.PermissionCategoryID,
                        principalTable: "PermissionCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Role_User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role_User", x => new { x.UserID, x.RoleID });
                    table.ForeignKey(
                        name: "FK_Role_User_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Role_User_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    TokenValue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TokenType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tokens_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Profile",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Profile", x => x.ID);
                    table.ForeignKey(
                        name: "FK_User_Profile_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Role_Permission",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    PermissionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role_Permission", x => new { x.RoleID, x.PermissionID });
                    table.ForeignKey(
                        name: "FK_Role_Permission_Permissions_PermissionID",
                        column: x => x.PermissionID,
                        principalTable: "Permissions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Role_Permission_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PermissionCategories",
                columns: new[] { "ID", "CreatedAt", "Description", "Name", "Order", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4556), "Manage user permissions", "User", 10, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4556) },
                    { 2, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4560), "Manage role permissions", "Role", 20, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4560) },
                    { 3, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4561), "Manage checklist permissions", "Checklist", 30, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4561) },
                    { 4, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4562), "Manage report permissions", "Report Virus", 40, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4563) },
                    { 5, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4564), "Manage meeting permissions", "Meeting", 50, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4564) },
                    { 6, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4565), "Manage license permissions", "License", 60, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4565) },
                    { 7, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4566), "Manage contract and bill permissions", "Contract & Bill", 70, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4567) },
                    { 8, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4568), "Manage settings permissions", "Settings Management", 80, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4568) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "ID", "CreatedAt", "RoleName", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4390), "Administrator", new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4393) },
                    { 2, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4395), "Employee", new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4395) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "CreatedAt", "Email", "EmployeeId", "PasswordHash", "Status", "UpdatedAt", "Username" },
                values: new object[] { 1, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4397), "admin@example.com", "ADMIN001", "3b612c75a7b5048a435fb6ec81e52ff92d6d795a8b5a9c17070f6a63c97a53b2", 1, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4397), "admin" });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "ID", "CreatedAt", "Description", "Name", "PermissionCategoryID", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4598), "View user list", "User_View", 1, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4599) },
                    { 2, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4601), "Add new user", "User_Add", 1, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4601) },
                    { 3, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4603), "Edit user information", "User_Edit", 1, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4603) },
                    { 4, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4605), "Delete user", "User_Delete", 1, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4605) },
                    { 5, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4607), "Add new role", "Role_Add", 2, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4607) },
                    { 6, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4609), "Edit role", "Role_Edit", 2, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4609) },
                    { 7, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4610), "Delete role", "Role_Delete", 2, new DateTime(2024, 10, 23, 17, 19, 42, 673, DateTimeKind.Utc).AddTicks(4611) }
                });

            migrationBuilder.InsertData(
                table: "Role_User",
                columns: new[] { "RoleID", "UserID" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Name",
                table: "Permissions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_PermissionCategoryID",
                table: "Permissions",
                column: "PermissionCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Permission_PermissionID",
                table: "Role_Permission",
                column: "PermissionID");

            migrationBuilder.CreateIndex(
                name: "IX_Role_User_RoleID",
                table: "Role_User",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Role_User_UserID",
                table: "Role_User",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleName",
                table: "Roles",
                column: "RoleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserID",
                table: "Tokens",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Profile_UserID",
                table: "User_Profile",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Role_Permission");

            migrationBuilder.DropTable(
                name: "Role_User");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "User_Profile");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "PermissionCategories");
        }
    }
}
