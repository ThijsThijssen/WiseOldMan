using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WiseOldMan.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ID = table
                        .Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ID);
                }
            );

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    ID = table
                        .Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscordID = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.ID);
                }
            );

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    ID = table
                        .Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.ID);
                }
            );

            migrationBuilder.CreateTable(
                name: "PlayerItem",
                columns: table => new
                {
                    PlayerItemID = table
                        .Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemID = table.Column<int>(type: "int", nullable: false),
                    PlayerID = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    PlayerID1 = table.Column<int>(type: "int", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerItem", x => x.PlayerItemID);
                    table.ForeignKey(
                        name: "FK_PlayerItem_Item_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Item",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_PlayerItem_Player_PlayerID1",
                        column: x => x.PlayerID1,
                        principalTable: "Player",
                        principalColumn: "ID"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "PlayerSkill",
                columns: table => new
                {
                    PlayerSkillID = table
                        .Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillID = table.Column<int>(type: "int", nullable: false),
                    PlayerID = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    PlayerID1 = table.Column<int>(type: "int", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerSkill", x => x.PlayerSkillID);
                    table.ForeignKey(
                        name: "FK_PlayerSkill_Player_PlayerID1",
                        column: x => x.PlayerID1,
                        principalTable: "Player",
                        principalColumn: "ID"
                    );
                    table.ForeignKey(
                        name: "FK_PlayerSkill_Skill_SkillID",
                        column: x => x.SkillID,
                        principalTable: "Skill",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_PlayerItem_ItemID",
                table: "PlayerItem",
                column: "ItemID"
            );

            migrationBuilder.CreateIndex(
                name: "IX_PlayerItem_PlayerID1",
                table: "PlayerItem",
                column: "PlayerID1"
            );

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSkill_PlayerID1",
                table: "PlayerSkill",
                column: "PlayerID1"
            );

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSkill_SkillID",
                table: "PlayerSkill",
                column: "SkillID"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "PlayerItem");

            migrationBuilder.DropTable(name: "PlayerSkill");

            migrationBuilder.DropTable(name: "Item");

            migrationBuilder.DropTable(name: "Player");

            migrationBuilder.DropTable(name: "Skill");
        }
    }
}
