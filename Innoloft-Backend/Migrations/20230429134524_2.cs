using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Innoloft_Backend.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvitedEventUser");

            migrationBuilder.DropTable(
                name: "ParticipatingEventUser");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateTable(
                name: "EventUser",
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
                name: "EventUser1",
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
                name: "IX_EventUser_ParticipatingUsersId",
                table: "EventUser",
                column: "ParticipatingUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_EventUser1_InvitedUsersId",
                table: "EventUser1",
                column: "InvitedUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventUser");

            migrationBuilder.DropTable(
                name: "EventUser1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateTable(
                name: "InvitedEventUser",
                columns: table => new
                {
                    InvitedEventsId = table.Column<int>(type: "INTEGER", nullable: false),
                    InvitedUsersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvitedEventUser", x => new { x.InvitedEventsId, x.InvitedUsersId });
                    table.ForeignKey(
                        name: "FK_InvitedEventUser_Events_InvitedEventsId",
                        column: x => x.InvitedEventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvitedEventUser_Users_InvitedUsersId",
                        column: x => x.InvitedUsersId,
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
                    table.PrimaryKey("PK_ParticipatingEventUser", x => new { x.ParticipatingEventsId, x.ParticipatingUsersId });
                    table.ForeignKey(
                        name: "FK_ParticipatingEventUser_Events_ParticipatingEventsId",
                        column: x => x.ParticipatingEventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipatingEventUser_Users_ParticipatingUsersId",
                        column: x => x.ParticipatingUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvitedEventUser_InvitedUsersId",
                table: "InvitedEventUser",
                column: "InvitedUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipatingEventUser_ParticipatingUsersId",
                table: "ParticipatingEventUser",
                column: "ParticipatingUsersId");
        }
    }
}
