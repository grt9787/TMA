using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMA.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    ActionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ActionName = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.ActionId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    RoleDescription = table.Column<string>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaskStatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    UserStatusId = table.Column<int>(type: "INTEGER", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "RoleActions",
                columns: table => new
                {
                    RoleActionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    ActionId = table.Column<int>(type: "INTEGER", nullable: false),
                    HasFullAccess = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasReadOnly = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleActions", x => x.RoleActionId);
                    table.ForeignKey(
                        name: "FK_RoleActions_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "ActionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleActions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UserRoleId);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Actions",
                columns: new[] { "ActionId", "ActionName", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" },
                values: new object[] { 1, "Create", "System", new DateTime(2024, 11, 6, 15, 28, 46, 494, DateTimeKind.Local).AddTicks(5814), null, null });

            migrationBuilder.InsertData(
                table: "Actions",
                columns: new[] { "ActionId", "ActionName", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" },
                values: new object[] { 2, "Edit", "System", new DateTime(2024, 11, 6, 15, 28, 46, 494, DateTimeKind.Local).AddTicks(5816), null, null });

            migrationBuilder.InsertData(
                table: "Actions",
                columns: new[] { "ActionId", "ActionName", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" },
                values: new object[] { 3, "Delete", "System", new DateTime(2024, 11, 6, 15, 28, 46, 494, DateTimeKind.Local).AddTicks(5817), null, null });

            migrationBuilder.InsertData(
                table: "Actions",
                columns: new[] { "ActionId", "ActionName", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" },
                values: new object[] { 4, "View", "System", new DateTime(2024, 11, 6, 15, 28, 46, 494, DateTimeKind.Local).AddTicks(5818), null, null });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "CreatedBy", "CreatedDate", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name", "RoleDescription" },
                values: new object[] { 1, "System", new DateTime(2024, 11, 6, 15, 28, 46, 494, DateTimeKind.Local).AddTicks(5775), false, null, null, "Admin", "Administrator with full access" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "CreatedBy", "CreatedDate", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name", "RoleDescription" },
                values: new object[] { 2, "System", new DateTime(2024, 11, 6, 15, 28, 46, 494, DateTimeKind.Local).AddTicks(5789), false, null, null, "Manager", "Manager with limited management rights" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "CreatedBy", "CreatedDate", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name", "RoleDescription" },
                values: new object[] { 3, "System", new DateTime(2024, 11, 6, 15, 28, 46, 494, DateTimeKind.Local).AddTicks(5790), false, null, null, "User", "Standard user with read-only access" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "City", "CreatedBy", "CreatedDate", "Email", "FirstName", "IsDeleted", "LastName", "ModifiedBy", "ModifiedDate", "Password", "PhoneNumber", "UserStatusId" },
                values: new object[] { 1, "123 Admin St", "Admin City", "system", new DateTime(2024, 11, 6, 9, 58, 46, 494, DateTimeKind.Utc).AddTicks(5484), "admin@example.com", "Admin", false, "User", null, null, "Admin@123", "123-456-7890", 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "City", "CreatedBy", "CreatedDate", "Email", "FirstName", "IsDeleted", "LastName", "ModifiedBy", "ModifiedDate", "Password", "PhoneNumber", "UserStatusId" },
                values: new object[] { 2, "456 Manager Ave", "Manager City", "system", new DateTime(2024, 11, 6, 9, 58, 46, 494, DateTimeKind.Utc).AddTicks(5490), "manager@example.com", "Manager", false, "User", null, null, "Manager@123", "987-654-3210", 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "City", "CreatedBy", "CreatedDate", "Email", "FirstName", "IsDeleted", "LastName", "ModifiedBy", "ModifiedDate", "Password", "PhoneNumber", "UserStatusId" },
                values: new object[] { 3, "789 User Blvd", "User City", "system", new DateTime(2024, 11, 6, 9, 58, 46, 494, DateTimeKind.Utc).AddTicks(5492), "user@example.com", "Regular", false, "User", null, null, "User@123", "555-555-5555", 1 });

            migrationBuilder.InsertData(
                table: "RoleActions",
                columns: new[] { "RoleActionId", "ActionId", "CreatedBy", "CreatedDate", "HasFullAccess", "HasReadOnly", "ModifiedBy", "ModifiedDate", "RoleId" },
                values: new object[] { 1, 1, "System", new DateTime(2024, 11, 6, 15, 28, 46, 494, DateTimeKind.Local).AddTicks(5840), true, false, null, null, 1 });

            migrationBuilder.InsertData(
                table: "RoleActions",
                columns: new[] { "RoleActionId", "ActionId", "CreatedBy", "CreatedDate", "HasFullAccess", "HasReadOnly", "ModifiedBy", "ModifiedDate", "RoleId" },
                values: new object[] { 2, 2, "System", new DateTime(2024, 11, 6, 15, 28, 46, 494, DateTimeKind.Local).AddTicks(5843), true, false, null, null, 1 });

            migrationBuilder.InsertData(
                table: "RoleActions",
                columns: new[] { "RoleActionId", "ActionId", "CreatedBy", "CreatedDate", "HasFullAccess", "HasReadOnly", "ModifiedBy", "ModifiedDate", "RoleId" },
                values: new object[] { 3, 3, "System", new DateTime(2024, 11, 6, 15, 28, 46, 494, DateTimeKind.Local).AddTicks(5846), true, false, null, null, 1 });

            migrationBuilder.InsertData(
                table: "RoleActions",
                columns: new[] { "RoleActionId", "ActionId", "CreatedBy", "CreatedDate", "HasFullAccess", "HasReadOnly", "ModifiedBy", "ModifiedDate", "RoleId" },
                values: new object[] { 4, 4, "System", new DateTime(2024, 11, 6, 15, 28, 46, 494, DateTimeKind.Local).AddTicks(5847), true, false, null, null, 1 });

            migrationBuilder.InsertData(
                table: "RoleActions",
                columns: new[] { "RoleActionId", "ActionId", "CreatedBy", "CreatedDate", "HasFullAccess", "HasReadOnly", "ModifiedBy", "ModifiedDate", "RoleId" },
                values: new object[] { 5, 1, "System", new DateTime(2024, 11, 6, 15, 28, 46, 494, DateTimeKind.Local).AddTicks(5849), false, false, null, null, 2 });

            migrationBuilder.InsertData(
                table: "RoleActions",
                columns: new[] { "RoleActionId", "ActionId", "CreatedBy", "CreatedDate", "HasFullAccess", "HasReadOnly", "ModifiedBy", "ModifiedDate", "RoleId" },
                values: new object[] { 6, 2, "System", new DateTime(2024, 11, 6, 15, 28, 46, 494, DateTimeKind.Local).AddTicks(5909), false, false, null, null, 2 });

            migrationBuilder.InsertData(
                table: "RoleActions",
                columns: new[] { "RoleActionId", "ActionId", "CreatedBy", "CreatedDate", "HasFullAccess", "HasReadOnly", "ModifiedBy", "ModifiedDate", "RoleId" },
                values: new object[] { 7, 4, "System", new DateTime(2024, 11, 6, 15, 28, 46, 494, DateTimeKind.Local).AddTicks(5911), false, true, null, null, 2 });

            migrationBuilder.InsertData(
                table: "RoleActions",
                columns: new[] { "RoleActionId", "ActionId", "CreatedBy", "CreatedDate", "HasFullAccess", "HasReadOnly", "ModifiedBy", "ModifiedDate", "RoleId" },
                values: new object[] { 8, 4, "System", new DateTime(2024, 11, 6, 15, 28, 46, 494, DateTimeKind.Local).AddTicks(5913), false, true, null, null, 3 });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserRoleId", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "RoleId", "UserId" },
                values: new object[] { 1, "system", new DateTime(2024, 11, 6, 9, 58, 46, 494, DateTimeKind.Utc).AddTicks(5741), null, null, 1, 1 });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserRoleId", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "RoleId", "UserId" },
                values: new object[] { 2, "system", new DateTime(2024, 11, 6, 9, 58, 46, 494, DateTimeKind.Utc).AddTicks(5743), null, null, 2, 2 });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserRoleId", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "RoleId", "UserId" },
                values: new object[] { 3, "system", new DateTime(2024, 11, 6, 9, 58, 46, 494, DateTimeKind.Utc).AddTicks(5744), null, null, 3, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_RoleActions_ActionId",
                table: "RoleActions",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleActions_RoleId",
                table: "RoleActions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleActions");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
