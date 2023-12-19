using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "character varying", nullable: false),
                    Password = table.Column<string>(type: "character varying", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HostID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.id);
                    table.ForeignKey(
                        name: "HostID",
                        column: x => x.HostID,
                        principalTable: "Player",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.id);
                    table.ForeignKey(
                        name: "FK_Session_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "PlayerID",
                        column: x => x.UserID,
                        principalTable: "Player",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_HostID",
                table: "Game",
                column: "HostID");

            migrationBuilder.CreateIndex(
                name: "IX_Session_GameId",
                table: "Session",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Session_UserID",
                table: "Session",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Player");
        }
    }
}
