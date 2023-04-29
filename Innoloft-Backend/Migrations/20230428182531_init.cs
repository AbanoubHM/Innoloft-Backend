using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Innoloft_Backend.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Address_Street = table.Column<string>(type: "TEXT", nullable: false),
                    Address_Suite = table.Column<string>(type: "TEXT", nullable: false),
                    Address_City = table.Column<string>(type: "TEXT", nullable: false),
                    Address_Zipcode = table.Column<string>(type: "TEXT", nullable: true),
                    Address_Geo_Lat = table.Column<string>(type: "TEXT", nullable: true),
                    Address_Geo_Lng = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Website = table.Column<string>(type: "TEXT", nullable: false),
                    Company_Name = table.Column<string>(type: "TEXT", nullable: false),
                    Company_CatchPhrase = table.Column<string>(type: "TEXT", nullable: false),
                    Company_Bs = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    StartTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Address_Street = table.Column<string>(type: "TEXT", nullable: false),
                    Address_Suite = table.Column<string>(type: "TEXT", nullable: false),
                    Address_City = table.Column<string>(type: "TEXT", nullable: false),
                    Address_Zipcode = table.Column<string>(type: "TEXT", nullable: true),
                    Address_Geo_Lat = table.Column<string>(type: "TEXT", nullable: true),
                    Address_Geo_Lng = table.Column<string>(type: "TEXT", nullable: true),
                    IsOnline = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParticipatingEventUser",
                columns: table => new
                {
                    ParticipatingEventsId = table.Column<int>(type: "INTEGER", nullable: false),
                    ParticipatingUsersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventUser", x => new { x.ParticipatingEventsId, x.ParticipatingUsersId });
                    table.ForeignKey(
                        name: "FK_EventUser_Events_ParticipatingEventsId",
                        column: x => x.ParticipatingEventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventUser_Users_ParticipatingUsersId",
                        column: x => x.ParticipatingUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvitedEventUser",
                columns: table => new
                {
                    InvitedEventsId = table.Column<int>(type: "INTEGER", nullable: false),
                    InvitedUsersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventUser1", x => new { x.InvitedEventsId, x.InvitedUsersId });
                    table.ForeignKey(
                        name: "FK_EventUser1_Events_InvitedEventsId",
                        column: x => x.InvitedEventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventUser1_Users_InvitedUsersId",
                        column: x => x.InvitedUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_UserId",
                table: "Events",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EventUser_ParticipatingUsersId",
                table: "ParticipatingEventUser",
                column: "ParticipatingUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_EventUser1_InvitedUsersId",
                table: "InvitedEventUser",
                column: "InvitedUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParticipatingEventUser");

            migrationBuilder.DropTable(
                name: "InvitedEventUser");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
