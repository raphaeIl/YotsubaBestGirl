using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace YotsubaBestGirl.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateAccountAndUserDBs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "player_account",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uuid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_player_account", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Uid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Muid = table.Column<string>(type: "text", nullable: true),
                    WalletId = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    SessionId = table.Column<string>(type: "text", nullable: true),
                    Lastname = table.Column<string>(type: "text", nullable: true),
                    Firstname = table.Column<string>(type: "text", nullable: true),
                    LastnameKana = table.Column<string>(type: "text", nullable: true),
                    FirstnameKana = table.Column<string>(type: "text", nullable: true),
                    Nickname = table.Column<string>(type: "text", nullable: true),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    Birthday = table.Column<int>(type: "integer", nullable: false),
                    BirthdayCount = table.Column<int>(type: "integer", nullable: false),
                    BirthdayLastDate = table.Column<long>(type: "bigint", nullable: false),
                    HomeBackgroundId = table.Column<int>(type: "integer", nullable: false),
                    Tutorial = table.Column<int>(type: "integer", nullable: false),
                    IsTutorialFinish = table.Column<int>(type: "integer", nullable: false),
                    IsClearBeginner = table.Column<int>(type: "integer", nullable: false),
                    ActiveUnit = table.Column<int>(type: "integer", nullable: false),
                    FavoriteCharacters = table.Column<int>(type: "integer", nullable: false),
                    PlayerTitleId = table.Column<int>(type: "integer", nullable: false),
                    PlayerTitleTargetId = table.Column<int>(type: "integer", nullable: false),
                    ProfileCardId = table.Column<long>(type: "bigint", nullable: false),
                    ProfileBackgroundId = table.Column<int>(type: "integer", nullable: false),
                    HomeBgm = table.Column<int>(type: "integer", nullable: false),
                    Leaf = table.Column<long>(type: "bigint", nullable: false),
                    Ruby = table.Column<int>(type: "integer", nullable: false),
                    Fragment = table.Column<int>(type: "integer", nullable: false),
                    FriendPoint = table.Column<int>(type: "integer", nullable: false),
                    Exp = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Ap = table.Column<int>(type: "integer", nullable: false),
                    LastApDate = table.Column<long>(type: "bigint", nullable: false),
                    SpecialAp = table.Column<int>(type: "integer", nullable: false),
                    LastSpecialApDate = table.Column<long>(type: "bigint", nullable: false),
                    ContinueLoginNum = table.Column<int>(type: "integer", nullable: false),
                    LoginNum = table.Column<int>(type: "integer", nullable: false),
                    TotalLoginNum = table.Column<int>(type: "integer", nullable: false),
                    LastLoginDate = table.Column<long>(type: "bigint", nullable: false),
                    LastChallengeLoginDate = table.Column<long>(type: "bigint", nullable: false),
                    ChallengeUpdateStep = table.Column<int>(type: "integer", nullable: false),
                    LastSeasonId = table.Column<int>(type: "integer", nullable: false),
                    EventPlayingPolicy = table.Column<int>(type: "integer", nullable: false),
                    OpenedAt = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "cards",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Uid = table.Column<int>(type: "integer", nullable: false),
                    CardId = table.Column<int>(type: "integer", nullable: false),
                    CardUniqueId = table.Column<string>(type: "text", nullable: true),
                    CardPropertyId = table.Column<int>(type: "integer", nullable: false),
                    CardPropertyId2 = table.Column<int>(type: "integer", nullable: false),
                    CardPremiumId = table.Column<int>(type: "integer", nullable: false),
                    IsPairCard = table.Column<int>(type: "integer", nullable: false),
                    IsSignedCard = table.Column<int>(type: "integer", nullable: false),
                    SerialNumber = table.Column<int>(type: "integer", nullable: false),
                    Exp = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    LevelAwake = table.Column<int>(type: "integer", nullable: false),
                    ActiveSkillExp = table.Column<int>(type: "integer", nullable: false),
                    ActiveSkillLevel = table.Column<int>(type: "integer", nullable: false),
                    PassiveSkillExp1 = table.Column<int>(type: "integer", nullable: false),
                    PassiveSkillLevel1 = table.Column<int>(type: "integer", nullable: false),
                    PassiveSkillExp2 = table.Column<int>(type: "integer", nullable: false),
                    PassiveSkillLevel2 = table.Column<int>(type: "integer", nullable: false),
                    PassiveSkillExp3 = table.Column<int>(type: "integer", nullable: false),
                    PassiveSkillLevel3 = table.Column<int>(type: "integer", nullable: false),
                    LimitbreakRank = table.Column<int>(type: "integer", nullable: false),
                    AwakePriority = table.Column<int>(type: "integer", nullable: false),
                    Kirameki = table.Column<int>(type: "integer", nullable: false),
                    Tokimeki = table.Column<int>(type: "integer", nullable: false),
                    InterludeVoice1 = table.Column<int>(type: "integer", nullable: false),
                    InterludeVoice2 = table.Column<int>(type: "integer", nullable: false),
                    InterludeVoice3 = table.Column<int>(type: "integer", nullable: false),
                    InterludeVoice4 = table.Column<int>(type: "integer", nullable: false),
                    InterludeVoice5 = table.Column<int>(type: "integer", nullable: false),
                    AcquiredGrowthRewardSeqIds = table.Column<string>(type: "text", nullable: true),
                    Protect = table.Column<int>(type: "integer", nullable: false),
                    Bgm = table.Column<int>(type: "integer", nullable: false),
                    ResourceIdx = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cards_users_Uid",
                        column: x => x.Uid,
                        principalTable: "users",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cards_Uid",
                table: "cards",
                column: "Uid");

            migrationBuilder.CreateIndex(
                name: "IX_users_Uid",
                table: "users",
                column: "Uid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cards");

            migrationBuilder.DropTable(
                name: "player_account");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
