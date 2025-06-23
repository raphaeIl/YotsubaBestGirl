using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace YotsubaBestGirl.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddMostUserDataDBs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cards_users_Uid",
                table: "cards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_player_account",
                table: "player_account");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cards",
                table: "cards");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "t_user");

            migrationBuilder.RenameTable(
                name: "player_account",
                newName: "t_player_account");

            migrationBuilder.RenameTable(
                name: "cards",
                newName: "t_user_card");

            migrationBuilder.RenameIndex(
                name: "IX_users_Uid",
                table: "t_user",
                newName: "IX_t_user_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_cards_Uid",
                table: "t_user_card",
                newName: "IX_t_user_card_Uid");

            migrationBuilder.AddColumn<List<string>>(
                name: "Clear",
                table: "t_user",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_user",
                table: "t_user",
                column: "Uid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_player_account",
                table: "t_player_account",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_user_card",
                table: "t_user_card",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "t_user_book",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CardId = table.Column<int>(type: "integer", nullable: false),
                    CardPremiumId = table.Column<int>(type: "integer", nullable: false),
                    IsPairCard = table.Column<int>(type: "integer", nullable: false),
                    IsSignedCard = table.Column<int>(type: "integer", nullable: false),
                    SerialNumber = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    ActiveSkillLevel = table.Column<int>(type: "integer", nullable: false),
                    PassiveSkillLevel1 = table.Column<int>(type: "integer", nullable: false),
                    PassiveSkillLevel2 = table.Column<int>(type: "integer", nullable: false),
                    PassiveSkillLevel3 = table.Column<int>(type: "integer", nullable: false),
                    LimitbreakRank = table.Column<int>(type: "integer", nullable: false),
                    Kirameki = table.Column<int>(type: "integer", nullable: false),
                    Tokimeki = table.Column<int>(type: "integer", nullable: false),
                    Kiratoki = table.Column<int>(type: "integer", nullable: false),
                    LimitbreakRankSigned = table.Column<int>(type: "integer", nullable: false),
                    KiramekiSigned = table.Column<int>(type: "integer", nullable: false),
                    TokimekiSigned = table.Column<int>(type: "integer", nullable: false),
                    KiratokiSigned = table.Column<int>(type: "integer", nullable: false),
                    PictureResourceIdx1 = table.Column<int>(type: "integer", nullable: false),
                    PictureResourceIdx6 = table.Column<int>(type: "integer", nullable: false),
                    PictureResourceIdx7 = table.Column<int>(type: "integer", nullable: false),
                    PictureResourceIdx8 = table.Column<int>(type: "integer", nullable: false),
                    InterludeVoice1 = table.Column<int>(type: "integer", nullable: false),
                    InterludeVoice2 = table.Column<int>(type: "integer", nullable: false),
                    InterludeVoice3 = table.Column<int>(type: "integer", nullable: false),
                    InterludeVoice4 = table.Column<int>(type: "integer", nullable: false),
                    InterludeVoice5 = table.Column<int>(type: "integer", nullable: false),
                    InterludeVoice6 = table.Column<int>(type: "integer", nullable: false),
                    InterludeVoice7 = table.Column<int>(type: "integer", nullable: false),
                    Sell = table.Column<int>(type: "integer", nullable: false),
                    BuyBack = table.Column<int>(type: "integer", nullable: false),
                    AllCardId = table.Column<long>(type: "bigint", nullable: false),
                    MemberCardId = table.Column<long>(type: "bigint", nullable: false),
                    CostumeCardId = table.Column<long>(type: "bigint", nullable: false),
                    GroupCardId = table.Column<string>(type: "text", nullable: true),
                    AcquiredGrowthRewardSeqIds = table.Column<string>(type: "text", nullable: true),
                    Uid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_user_book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_user_book_t_user_Uid",
                        column: x => x.Uid,
                        principalTable: "t_user",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_user_chapter",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChapterId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Uid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_user_chapter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_user_chapter_t_user_Uid",
                        column: x => x.Uid,
                        principalTable: "t_user",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_user_gacha_history",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GachaId = table.Column<int>(type: "integer", nullable: false),
                    Step = table.Column<int>(type: "integer", nullable: false),
                    CurrentBoxId = table.Column<int>(type: "integer", nullable: false),
                    RankupBoxId = table.Column<int>(type: "integer", nullable: false),
                    SingleCnt = table.Column<int>(type: "integer", nullable: false),
                    TicketCnt = table.Column<int>(type: "integer", nullable: false),
                    LumpCnt = table.Column<int>(type: "integer", nullable: false),
                    TotalCnt = table.Column<int>(type: "integer", nullable: false),
                    LimitCnt = table.Column<int>(type: "integer", nullable: false),
                    GroupWeightCnt = table.Column<int>(type: "integer", nullable: false),
                    IsPaid = table.Column<int>(type: "integer", nullable: false),
                    PaidDate = table.Column<long>(type: "bigint", nullable: false),
                    LastGachaDate = table.Column<long>(type: "bigint", nullable: false),
                    PaidSingleResetCount = table.Column<int>(type: "integer", nullable: false),
                    PaidSingleResetDate = table.Column<long>(type: "bigint", nullable: false),
                    PaidLumpResetCount = table.Column<int>(type: "integer", nullable: false),
                    PaidLumpResetDate = table.Column<long>(type: "bigint", nullable: false),
                    Uid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_user_gacha_history", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_user_gacha_history_t_user_Uid",
                        column: x => x.Uid,
                        principalTable: "t_user",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_user_item",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    ExpireDate = table.Column<long>(type: "bigint", nullable: false),
                    Uid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_user_item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_user_item_t_user_Uid",
                        column: x => x.Uid,
                        principalTable: "t_user",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_user_member",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MemberId = table.Column<int>(type: "integer", nullable: false),
                    MemberPictureId = table.Column<int>(type: "integer", nullable: false),
                    ExceptHomeMessageIds = table.Column<string>(type: "text", nullable: true),
                    Dearlevel = table.Column<int>(type: "integer", nullable: false),
                    Dearpoint = table.Column<int>(type: "integer", nullable: false),
                    NameRank = table.Column<int>(type: "integer", nullable: false),
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    ItemIdQueue = table.Column<string>(type: "text", nullable: true),
                    AddCount = table.Column<int>(type: "integer", nullable: false),
                    ExpireDate = table.Column<long>(type: "bigint", nullable: false),
                    Uid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_user_member", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_user_member_t_user_Uid",
                        column: x => x.Uid,
                        principalTable: "t_user",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_user_member_picture",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MemberPictureId = table.Column<int>(type: "integer", nullable: false),
                    ReleasedEmotionResourceIds = table.Column<string>(type: "text", nullable: false),
                    Uid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_user_member_picture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_user_member_picture_t_user_Uid",
                        column: x => x.Uid,
                        principalTable: "t_user",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_user_options",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Bgm = table.Column<int>(type: "integer", nullable: false),
                    Se = table.Column<int>(type: "integer", nullable: false),
                    Voice = table.Column<int>(type: "integer", nullable: false),
                    PushSystem = table.Column<int>(type: "integer", nullable: false),
                    PushAppointment = table.Column<int>(type: "integer", nullable: false),
                    PushAp = table.Column<int>(type: "integer", nullable: false),
                    PushGuerrilla = table.Column<int>(type: "integer", nullable: false),
                    PushWork = table.Column<int>(type: "integer", nullable: false),
                    PushChat = table.Column<int>(type: "integer", nullable: false),
                    PushCooking = table.Column<int>(type: "integer", nullable: false),
                    ProtectCardR6 = table.Column<int>(type: "integer", nullable: false),
                    ProtectCardR5 = table.Column<int>(type: "integer", nullable: false),
                    ProtectCardR4 = table.Column<int>(type: "integer", nullable: false),
                    ProtectCardFirst = table.Column<int>(type: "integer", nullable: false),
                    Gyro = table.Column<int>(type: "integer", nullable: false),
                    PowerSaving = table.Column<int>(type: "integer", nullable: false),
                    Uid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_user_options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_user_options_t_user_Uid",
                        column: x => x.Uid,
                        principalTable: "t_user",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_user_story",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StoryId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Choice1 = table.Column<int>(type: "integer", nullable: false),
                    Choice2 = table.Column<int>(type: "integer", nullable: false),
                    Choice3 = table.Column<int>(type: "integer", nullable: false),
                    Uid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_user_story", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_user_story_t_user_Uid",
                        column: x => x.Uid,
                        principalTable: "t_user",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_user_unit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Idx = table.Column<int>(type: "integer", nullable: false),
                    UnitName = table.Column<string>(type: "text", nullable: false),
                    MemberId1 = table.Column<int>(type: "integer", nullable: false),
                    MemberId2 = table.Column<int>(type: "integer", nullable: false),
                    MemberId3 = table.Column<int>(type: "integer", nullable: false),
                    MemberId4 = table.Column<int>(type: "integer", nullable: false),
                    MemberId5 = table.Column<int>(type: "integer", nullable: false),
                    CardId1 = table.Column<long>(type: "bigint", nullable: false),
                    CardId2 = table.Column<long>(type: "bigint", nullable: false),
                    CardId3 = table.Column<long>(type: "bigint", nullable: false),
                    CardId4 = table.Column<long>(type: "bigint", nullable: false),
                    CardId5 = table.Column<long>(type: "bigint", nullable: false),
                    Sub1CardId1 = table.Column<long>(type: "bigint", nullable: false),
                    Sub1CardId2 = table.Column<long>(type: "bigint", nullable: false),
                    Sub1CardId3 = table.Column<long>(type: "bigint", nullable: false),
                    Sub1CardId4 = table.Column<long>(type: "bigint", nullable: false),
                    Sub1CardId5 = table.Column<long>(type: "bigint", nullable: false),
                    Sub2CardId1 = table.Column<long>(type: "bigint", nullable: false),
                    Sub2CardId2 = table.Column<long>(type: "bigint", nullable: false),
                    Sub2CardId3 = table.Column<long>(type: "bigint", nullable: false),
                    Sub2CardId4 = table.Column<long>(type: "bigint", nullable: false),
                    Sub2CardId5 = table.Column<long>(type: "bigint", nullable: false),
                    Sub3CardId1 = table.Column<long>(type: "bigint", nullable: false),
                    Sub3CardId2 = table.Column<long>(type: "bigint", nullable: false),
                    Sub3CardId3 = table.Column<long>(type: "bigint", nullable: false),
                    Sub3CardId4 = table.Column<long>(type: "bigint", nullable: false),
                    Sub3CardId5 = table.Column<long>(type: "bigint", nullable: false),
                    Sub4CardId1 = table.Column<long>(type: "bigint", nullable: false),
                    Sub4CardId2 = table.Column<long>(type: "bigint", nullable: false),
                    Sub4CardId3 = table.Column<long>(type: "bigint", nullable: false),
                    Sub4CardId4 = table.Column<long>(type: "bigint", nullable: false),
                    Sub4CardId5 = table.Column<long>(type: "bigint", nullable: false),
                    UnitSkillId = table.Column<int>(type: "integer", nullable: false),
                    Uid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_user_unit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_user_unit_t_user_Uid",
                        column: x => x.Uid,
                        principalTable: "t_user",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_user_book_Uid",
                table: "t_user_book",
                column: "Uid");

            migrationBuilder.CreateIndex(
                name: "IX_t_user_chapter_Uid",
                table: "t_user_chapter",
                column: "Uid");

            migrationBuilder.CreateIndex(
                name: "IX_t_user_gacha_history_Uid",
                table: "t_user_gacha_history",
                column: "Uid");

            migrationBuilder.CreateIndex(
                name: "IX_t_user_item_Uid",
                table: "t_user_item",
                column: "Uid");

            migrationBuilder.CreateIndex(
                name: "IX_t_user_member_Uid",
                table: "t_user_member",
                column: "Uid");

            migrationBuilder.CreateIndex(
                name: "IX_t_user_member_picture_Uid",
                table: "t_user_member_picture",
                column: "Uid");

            migrationBuilder.CreateIndex(
                name: "IX_t_user_options_Uid",
                table: "t_user_options",
                column: "Uid");

            migrationBuilder.CreateIndex(
                name: "IX_t_user_story_Uid",
                table: "t_user_story",
                column: "Uid");

            migrationBuilder.CreateIndex(
                name: "IX_t_user_unit_Uid",
                table: "t_user_unit",
                column: "Uid");

            migrationBuilder.AddForeignKey(
                name: "FK_t_user_card_t_user_Uid",
                table: "t_user_card",
                column: "Uid",
                principalTable: "t_user",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_user_card_t_user_Uid",
                table: "t_user_card");

            migrationBuilder.DropTable(
                name: "t_user_book");

            migrationBuilder.DropTable(
                name: "t_user_chapter");

            migrationBuilder.DropTable(
                name: "t_user_gacha_history");

            migrationBuilder.DropTable(
                name: "t_user_item");

            migrationBuilder.DropTable(
                name: "t_user_member");

            migrationBuilder.DropTable(
                name: "t_user_member_picture");

            migrationBuilder.DropTable(
                name: "t_user_options");

            migrationBuilder.DropTable(
                name: "t_user_story");

            migrationBuilder.DropTable(
                name: "t_user_unit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_user_card",
                table: "t_user_card");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_user",
                table: "t_user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_player_account",
                table: "t_player_account");

            migrationBuilder.DropColumn(
                name: "Clear",
                table: "t_user");

            migrationBuilder.RenameTable(
                name: "t_user_card",
                newName: "cards");

            migrationBuilder.RenameTable(
                name: "t_user",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "t_player_account",
                newName: "player_account");

            migrationBuilder.RenameIndex(
                name: "IX_t_user_card_Uid",
                table: "cards",
                newName: "IX_cards_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_t_user_Uid",
                table: "users",
                newName: "IX_users_Uid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cards",
                table: "cards",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "Uid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_player_account",
                table: "player_account",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_cards_users_Uid",
                table: "cards",
                column: "Uid",
                principalTable: "users",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
