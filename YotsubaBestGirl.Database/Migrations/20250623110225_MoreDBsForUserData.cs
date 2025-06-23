using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace YotsubaBestGirl.Database.Migrations
{
    /// <inheritdoc />
    public partial class MoreDBsForUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_user_advertising",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdvertisingId = table.Column<int>(type: "integer", nullable: false),
                    ViewCount = table.Column<int>(type: "integer", nullable: false),
                    RewardCountRemaining = table.Column<int>(type: "integer", nullable: false),
                    LastResetAt = table.Column<long>(type: "bigint", nullable: false),
                    LastViewedAt = table.Column<long>(type: "bigint", nullable: false),
                    Uid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_user_advertising", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_user_advertising_t_user_Uid",
                        column: x => x.Uid,
                        principalTable: "t_user",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_user_app_icon",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppIconId = table.Column<int>(type: "integer", nullable: false),
                    Uid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_user_app_icon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_user_app_icon_t_user_Uid",
                        column: x => x.Uid,
                        principalTable: "t_user",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_user_home_member_clothes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClothesId = table.Column<int>(type: "integer", nullable: false),
                    Uid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_user_home_member_clothes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_user_home_member_clothes_t_user_Uid",
                        column: x => x.Uid,
                        principalTable: "t_user",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_user_home_member_tap_motion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TapMotionId = table.Column<int>(type: "integer", nullable: false),
                    Uid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_user_home_member_tap_motion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_user_home_member_tap_motion_t_user_Uid",
                        column: x => x.Uid,
                        principalTable: "t_user",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_user_photostamp",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PhotoStampId = table.Column<int>(type: "integer", nullable: false),
                    Uid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_user_photostamp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_user_photostamp_t_user_Uid",
                        column: x => x.Uid,
                        principalTable: "t_user",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_user_stage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StageId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    Rank = table.Column<int>(type: "integer", nullable: false),
                    UnitIdx = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false),
                    Uid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_user_stage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_user_stage_t_user_Uid",
                        column: x => x.Uid,
                        principalTable: "t_user",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_user_work_lineup",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ExpectValue = table.Column<int>(type: "integer", nullable: false),
                    Result = table.Column<int>(type: "integer", nullable: false),
                    LottedConditionId1 = table.Column<int>(type: "integer", nullable: false),
                    LottedConditionId2 = table.Column<int>(type: "integer", nullable: false),
                    LottedConditionId3 = table.Column<int>(type: "integer", nullable: false),
                    CardIds = table.Column<string>(type: "text", nullable: false),
                    CardResults = table.Column<string>(type: "text", nullable: false),
                    MvpCardId = table.Column<long>(type: "bigint", nullable: false),
                    LottedRewardSeqIds1 = table.Column<string>(type: "text", nullable: false),
                    LottedRewardSeqIds2 = table.Column<string>(type: "text", nullable: false),
                    LottedRewardSeqIds3 = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<long>(type: "bigint", nullable: false),
                    EndDate = table.Column<long>(type: "bigint", nullable: false),
                    ShortMin = table.Column<int>(type: "integer", nullable: false),
                    AddCoin = table.Column<int>(type: "integer", nullable: false),
                    Uid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_user_work_lineup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_user_work_lineup_t_user_Uid",
                        column: x => x.Uid,
                        principalTable: "t_user",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_user_advertising_Uid",
                table: "t_user_advertising",
                column: "Uid");

            migrationBuilder.CreateIndex(
                name: "IX_t_user_app_icon_Uid",
                table: "t_user_app_icon",
                column: "Uid");

            migrationBuilder.CreateIndex(
                name: "IX_t_user_home_member_clothes_Uid",
                table: "t_user_home_member_clothes",
                column: "Uid");

            migrationBuilder.CreateIndex(
                name: "IX_t_user_home_member_tap_motion_Uid",
                table: "t_user_home_member_tap_motion",
                column: "Uid");

            migrationBuilder.CreateIndex(
                name: "IX_t_user_photostamp_Uid",
                table: "t_user_photostamp",
                column: "Uid");

            migrationBuilder.CreateIndex(
                name: "IX_t_user_stage_Uid",
                table: "t_user_stage",
                column: "Uid");

            migrationBuilder.CreateIndex(
                name: "IX_t_user_work_lineup_Uid",
                table: "t_user_work_lineup",
                column: "Uid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_user_advertising");

            migrationBuilder.DropTable(
                name: "t_user_app_icon");

            migrationBuilder.DropTable(
                name: "t_user_home_member_clothes");

            migrationBuilder.DropTable(
                name: "t_user_home_member_tap_motion");

            migrationBuilder.DropTable(
                name: "t_user_photostamp");

            migrationBuilder.DropTable(
                name: "t_user_stage");

            migrationBuilder.DropTable(
                name: "t_user_work_lineup");
        }
    }
}
