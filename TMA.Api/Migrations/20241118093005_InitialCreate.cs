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
                    ActionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.ActionId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskTypeId = table.Column<int>(type: "int", nullable: false),
                    TaskStatusId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserStatusId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "RoleActions",
                columns: table => new
                {
                    RoleActionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ActionId = table.Column<int>(type: "int", nullable: false),
                    HasFullAccess = table.Column<bool>(type: "bit", nullable: false),
                    HasReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                values: new object[,]
                {
                    { 1, "Create", "System", new DateTime(2024, 11, 18, 15, 0, 4, 858, DateTimeKind.Local).AddTicks(4699), null, null },
                    { 2, "Edit", "System", new DateTime(2024, 11, 18, 15, 0, 4, 858, DateTimeKind.Local).AddTicks(4702), null, null },
                    { 3, "Delete", "System", new DateTime(2024, 11, 18, 15, 0, 4, 858, DateTimeKind.Local).AddTicks(4704), null, null },
                    { 4, "View", "System", new DateTime(2024, 11, 18, 15, 0, 4, 858, DateTimeKind.Local).AddTicks(4706), null, null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "CreatedBy", "CreatedDate", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name", "RoleDescription" },
                values: new object[,]
                {
                    { 1, "System", new DateTime(2024, 11, 18, 15, 0, 4, 858, DateTimeKind.Local).AddTicks(4651), false, null, null, "Admin", "Administrator with full access" },
                    { 2, "System", new DateTime(2024, 11, 18, 15, 0, 4, 858, DateTimeKind.Local).AddTicks(4667), false, null, null, "Manager", "Manager with limited management rights" },
                    { 3, "System", new DateTime(2024, 11, 18, 15, 0, 4, 858, DateTimeKind.Local).AddTicks(4669), false, null, null, "User", "Standard user with read-only access" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "City", "CreatedBy", "CreatedDate", "Email", "FirstName", "IsDeleted", "LastName", "ModifiedBy", "ModifiedDate", "Password", "PhoneNumber", "UserStatusId" },
                values: new object[,]
                {
                    { 1, "123 Admin St", "Admin City", "system", new DateTime(2024, 11, 18, 9, 30, 4, 858, DateTimeKind.Utc).AddTicks(4287), "admin@example.com", "Admin", false, "User", null, null, "Admin@123", "123-456-7890", 1 },
                    { 2, "456 Manager Ave", "Manager City", "system", new DateTime(2024, 11, 18, 9, 30, 4, 858, DateTimeKind.Utc).AddTicks(4294), "manager@example.com", "Manager", false, "User", null, null, "Manager@123", "987-654-3210", 1 },
                    { 3, "789 User Blvd", "User City", "system", new DateTime(2024, 11, 18, 9, 30, 4, 858, DateTimeKind.Utc).AddTicks(4297), "user@example.com", "Regular", false, "User", null, null, "User@123", "555-555-5555", 1 }
                });

            migrationBuilder.InsertData(
                table: "RoleActions",
                columns: new[] { "RoleActionId", "ActionId", "CreatedBy", "CreatedDate", "HasFullAccess", "HasReadOnly", "ModifiedBy", "ModifiedDate", "RoleId" },
                values: new object[,]
                {
                    { 1, 1, "System", new DateTime(2024, 11, 18, 15, 0, 4, 858, DateTimeKind.Local).AddTicks(4733), true, false, null, null, 1 },
                    { 2, 2, "System", new DateTime(2024, 11, 18, 15, 0, 4, 858, DateTimeKind.Local).AddTicks(4736), true, false, null, null, 1 },
                    { 3, 3, "System", new DateTime(2024, 11, 18, 15, 0, 4, 858, DateTimeKind.Local).AddTicks(4738), true, false, null, null, 1 },
                    { 4, 4, "System", new DateTime(2024, 11, 18, 15, 0, 4, 858, DateTimeKind.Local).AddTicks(4740), true, false, null, null, 1 },
                    { 5, 1, "System", new DateTime(2024, 11, 18, 15, 0, 4, 858, DateTimeKind.Local).AddTicks(4742), false, false, null, null, 2 },
                    { 6, 2, "System", new DateTime(2024, 11, 18, 15, 0, 4, 858, DateTimeKind.Local).AddTicks(4744), false, false, null, null, 2 },
                    { 7, 4, "System", new DateTime(2024, 11, 18, 15, 0, 4, 858, DateTimeKind.Local).AddTicks(4746), false, true, null, null, 2 },
                    { 8, 4, "System", new DateTime(2024, 11, 18, 15, 0, 4, 858, DateTimeKind.Local).AddTicks(4748), false, true, null, null, 3 }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserRoleId", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, "system", new DateTime(2024, 11, 18, 9, 30, 4, 858, DateTimeKind.Utc).AddTicks(4616), null, null, 1, 1 },
                    { 2, "system", new DateTime(2024, 11, 18, 9, 30, 4, 858, DateTimeKind.Utc).AddTicks(4619), null, null, 2, 2 },
                    { 3, "system", new DateTime(2024, 11, 18, 9, 30, 4, 858, DateTimeKind.Utc).AddTicks(4620), null, null, 3, 3 }
                });

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
