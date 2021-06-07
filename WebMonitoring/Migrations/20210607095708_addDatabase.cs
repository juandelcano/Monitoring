using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebMonitoring.Migrations
{
    public partial class addDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_M_KinerjaPerbankan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Periode = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SandiBank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KreditKol1 = table.Column<double>(type: "float", nullable: false),
                    KreditKol2 = table.Column<double>(type: "float", nullable: false),
                    KreditKol3 = table.Column<double>(type: "float", nullable: false),
                    KreditKol4 = table.Column<double>(type: "float", nullable: false),
                    KreditKol5 = table.Column<double>(type: "float", nullable: false),
                    Laba = table.Column<double>(type: "float", nullable: false),
                    Modal = table.Column<double>(type: "float", nullable: false),
                    TotalAset = table.Column<double>(type: "float", nullable: false),
                    ATMR = table.Column<double>(type: "float", nullable: false),
                    BebanOperasional = table.Column<double>(type: "float", nullable: false),
                    PendapatanOperasional = table.Column<double>(type: "float", nullable: false),
                    DanaPihakKetiga = table.Column<double>(type: "float", nullable: false),
                    NPL = table.Column<double>(type: "float", nullable: false),
                    ROE = table.Column<double>(type: "float", nullable: false),
                    ROA = table.Column<double>(type: "float", nullable: false),
                    LDR = table.Column<double>(type: "float", nullable: false),
                    BOPO = table.Column<double>(type: "float", nullable: false),
                    CAR = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_KinerjaPerbankan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsUpdatePassword = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_M_Employee_TB_M_User_UserID",
                        column: x => x.UserID,
                        principalTable: "TB_M_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_T_UserRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_T_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_T_UserRole_TB_M_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TB_M_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_T_UserRole_TB_M_User_UserId",
                        column: x => x.UserId,
                        principalTable: "TB_M_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Employee_UserID",
                table: "TB_M_Employee",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_UserRole_RoleId",
                table: "TB_T_UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_UserRole_UserId",
                table: "TB_T_UserRole",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_M_Employee");

            migrationBuilder.DropTable(
                name: "TB_M_KinerjaPerbankan");

            migrationBuilder.DropTable(
                name: "TB_T_UserRole");

            migrationBuilder.DropTable(
                name: "TB_M_Role");

            migrationBuilder.DropTable(
                name: "TB_M_User");
        }
    }
}
