using Google.Protobuf;
using Serilog;
using YotsubaBestGirl.Common.Utils;
using YotsubaBestGirl.Core;
using YotsubaBestGirl.GameServer.Services;
using YotsubaBestGirl.Proto.Pcommon;
using YotsubaBestGirl.Proto.Pmisc;
using YotsubaBestGirl.Proto.Proto;
using YotsubaBestGirl.Proto.Puser;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace YotsubaBestGirl.GameServer.Controllers.Api.ProtocolHandlers
{
    public class Account : ProtocolHandlerBase
    {
        private readonly ResourceService resourceService;

        public Account(IProtocolHandlerFactory protocolHandlerFactory, ResourceService _resourceService) : base(protocolHandlerFactory)
        {
            resourceService = _resourceService;
        }

        [ProtocolHandler(Protocol.account_authorize)]
        public AccountAuthorize AccountAuthorizeHandler(IQueryCollection? reqParams)
        {
            return new AccountAuthorize()
            {
                Session = "seggs",
                OpeningShows =
                {
                    new Proto.Pmaster.OpeningShow()
                    {
                        Id = 95,
                        BackgroundResourceId = 10400739,
                        VoiceId = "V000_0001",
                        Weight = 50,
                        OpenDate = 1748916000,
                        CloseDate = 1750125599
                    }
                },
                TermRevision = 2,
            };
        }

        [ProtocolHandler(Protocol.account_certificate)]
        public AccountCertificate AccountCertificateHandler(IQueryCollection? reqParams)
        {
            return new AccountCertificate()
            {

                Version = new Proto.Pmaster.Version()
                {
                    Platform = "android",
                    Application = "1.43.440",
                    Resource = Config.ResourceVersion,
                    Master = Config.MasterVersion,
                    Term = 2,
                    Revision = 2
                }
            };
        }

        [ProtocolHandler(Protocol.resource_list_Android)]
        public Resources ResourceListAndroidHandler(IQueryCollection? reqParams)
        {
            return resourceService.GetResource<Resources>(Protocol.resource_list_Android);
        }

        [ProtocolHandler(Protocol.master_all)]
        public Proto.Pmaster.All MasterAllHandler(IQueryCollection? reqParams)
        {
            return resourceService.GetResource<Proto.Pmaster.All>(Protocol.master_all);
        }

        [ProtocolHandler(Protocol.user_load)]
        public Proto.Proto.Nocontent UserLoadHandler(IQueryCollection? reqParams)
        {
            var resp = new Proto.Proto.Nocontent()
            {
                StoredData = Account.GetUserData()
            };

            return resp;
        }

        [ProtocolHandler(Protocol.fcm_token)]
        public Proto.Proto.Nocontent FcmTokenHandler(IQueryCollection? reqParams)
        {
            return new Proto.Proto.Nocontent()
            {
                StoredData = new StoredData()
                {
                    Clear =
                    {
                          "unlock_story_ids",
                          "feature_team_create",
                          "member_likabilitypoint",
                          "news_unread_count",
                          "friend_approval_count",
                          "login_bonus_flag",
                          "feature_team_member",
                          "last_bonds_season_ranking",
                          "bonds_season_ranking",
                          "news_latest_id",
                          "chat_unread_categories"
                    },
                    NewsLatestId = 3286,
                    NewsUnreadCount = 6,
                    BondsSeasonRanking = new BondsSeasonRanking()
                    {
                        SeasonId = 56,
                        Score = 2001
                    },
                    LastBondsSeasonRanking = new BondsSeasonRanking()
                    {
                        SeasonId = 55,
                    },
                }
            };
        }

        [ProtocolHandler(Protocol.shop_products)]
        public Proto.Proto.IAPProductList ShopProductsHandler(IQueryCollection? reqParams) // one case of reqParams being used, there are two it can be all=1 or all=1, resp prob different depending on that
        {
            // this is not the full list at all, the full shit is like 16k lines no way im coding that, so shop prob broken for now
            var resp = new IAPProductList()
            {
                List =
                {
                    new IAPProduct()
                    {
                        Id = 2,
                        ProductId = "leo30000",
                        Name = "aaa",
                        ConsumeType = "consumable",
                        SellingType = "normal",
                        Price = 120,
                        Coin = 100,
                        Enabled = true,
                        StartTime = 0,
                        EndTime = 0,
                        PurchaseCount = 0,
                        PurchaseCountMax = 0
                    }
                }
            };

            return resp;
        }

        // hardcoded for now, db later
        public static StoredData GetUserData()
        {
            // no db yet, so everything hardcoded ugly
            var data = new StoredData
            {
                Clear =
                {
                    "unlock_story_ids",
                    "options",
                    "retryable_puzzle",
                    "card_special_property",
                    "bgm",
                    "friend_approval_count",
                    "unlock_dearlevels",
                    "advertising",
                    "card_story",
                    "retryable_stage_group",
                    "home_actor",
                    "shop_exchange_lock",
                    "home_background",
                    "home_messgae",
                    "feature_coupon",
                    "story",
                    "shop_challenge",
                    "login_bonus_flag",
                    "special_puzzle",
                    "bonds_season_ranking",
                    "app_icon",
                    "challenge",
                    "gacha_omake_history",
                    "appointment_surprise",
                    "review",
                    "team_battle_selection_ids",
                    "still_picture",
                    "daily_unseen_lottery_result_ids",
                    "retryable_quest",
                    "feature_effects",
                    "feature_item",
                    "mileage",
                    "team_battle_stage",
                    "cooking_summary",
                    "chapter_expire",
                    "shop_dearpoint_stock",
                    "unit_skill",
                    "card_special",
                    "team_battle_puzzle",
                    "member",
                    "present_count",
                    "member_dearpoint",
                    "season_oshimen_ids",
                    "work",
                    "chat_unread_categories",
                    "news_unread_count",
                    "stage_fail_lock",
                    "feature_revival",
                    "item",
                    "collection_reward_flags",
                    "currency",
                    "team_battle_unread_lottery_result_ids",
                    "player_title_summary",
                    "vr_content",
                    "card_special_property_slot",
                    "stage_skip",
                    "free_gacha_count",
                    "photo_stamp",
                    "member_likabilitylevel_flag",
                    "card",
                    "member_picture",
                    "shop_fragment_lock",
                    "encore_puzzle",
                    "chapter",
                    "challenge_done",
                    "encore",
                    "home_picture",
                    "cooking_recipe",
                    "shop_stock",
                    "feature_selection_ids",
                    "retryable_chapter",
                    "member_likabilitypoint",
                    "feature_treasure_hunting_stage",
                    "group_photo",
                    "book",
                    "member_dearpoint_rank",
                    "work_member_summary",
                    "bonds_season_unlock",
                    "gacha_history",
                    "challenge_group",
                    "photo_booth",
                    "stage",
                    "home_member_clothes",
                    "friend_can_greeting_count",
                    "last_bonds_season_ranking",
                    "vip",
                    "gacha_box",
                    "cooking_tray",
                    "news_latest_id",
                    "unlock_likabilitylevels",
                    "feature_unread_lottery_result_ids",
                    "feature_team_member",
                    "appointment",
                    "member_dearlevel_flag",
                    "unit",
                    "work_lineup",
                    "special_stage",
                    "puzzle",
                    "special_content",
                    "func_tutorial_ids",
                    "card_special_slot_status",
                    "feature_team_create",
                    "card_special_slot",
                    "feature_appointment",
                    "home_member_tap_motion",
                    "user",
                    "retryable_stage",
                    "gacha_bonus_choice",
                    "bonds_season"
                },
                User = new User
                {
                    Uid = 50443193,
                    Muid = "IiaqKNMkWhlC",
                    WalletId = "MDxSD96XfmGZm",
                    Status = "open",
                    SessionId = "seggs",
                    Lastname = "上杉",
                    Firstname = "風太郎",
                    LastnameKana = "ウエスギ",
                    FirstnameKana = "フータロー",
                    Nickname = "Raphael",
                    Comment = "よろしくお願いします",
                    Birthday = 102,
                    BirthdayLastDate = 1749495563,
                    HomeBackgroundId = 10028,
                    Tutorial = 140,
                    ActiveUnit = 1,
                    PlayerTitleId = 505001,
                    Leaf = int.MaxValue, // some currency? gacha?
                    Exp = 2001,
                    Level = 9,
                    Ap = 28,
                    LastApDate = 1749497296,
                    SpecialAp = 3,
                    LastSpecialApDate = 1749495563,
                    ContinueLoginNum = 2,
                    LoginNum = 2,
                    TotalLoginNum = 6,
                    LastLoginDate = 1749514629,
                    LastChallengeLoginDate = 1749409163,
                    ChallengeUpdateStep = 6,
                    LastSeasonId = 55,
                    OpenedAt = 1749497296,
                    CreatedAt = 1749495563,
                },
                Currency = new Currency
                {
                    FreeCoin = int.MaxValue,
                    TotalCoin = int.MaxValue,
                },
                Puzzle = new Puzzle
                {
                    Uid = 50443193,
                    Puid = "3991b072-8273-4c25-a1b4-96d0ee9c8b73",
                    Status = 2,
                    RetryCount = 0,
                    ContinueCount = 0,
                    StageId = 101102,
                    RoundId = 101102,
                    Score = 0,
                    RemainCount = 0,
                    RemainSecond = 0,
                    HelperUid = 49966922,
                    HelperUse = 0,
                    UnitSkillUse = 0,
                    UsedAp = 1,
                    StartedAt = 1749497296,
                },
                Options = new Options
                {
                    Uid = 50443193,
                    Bgm = 50,
                    Se = 50,
                    Voice = 50,
                    PushSystem = 1,
                    PushAppointment = 1,
                    PushAp = 1,
                    PushWork = 1,
                    PushChat = 1,
                    PushCooking = 1,
                    ProtectCardR6 = 1,
                    ProtectCardR5 = 1,
                    ProtectCardFirst = 1,
                    Gyro = 1,
                },
                MemberDearlevelFlag = new MemberDearlevelFlag()
                {

                },
                Member =
                {
                    new Member
                    {
                        Uid = 50443193,
                        MemberId = 1,
                        MemberPictureId = 1,
                    },
                    new Member
                    {
                        Uid = 50443193,
                        MemberId = 5,
                        MemberPictureId = 5,
                    },
                },
                Unit =
                {
                    new Unit()
                    {
                        Uid = 50443924,
                        Idx = 1,
                        UnitName = "",
                        MemberId1 = 1,
                        MemberId2 = 2,
                        MemberId3 = 3,
                        MemberId4 = 4,
                        MemberId5 = 5,
                        CardId1 = 1924497643,
                        CardId2 = 1924497636,
                        CardId3 = 1924497639,
                        CardId4 = 1924497574,
                        CardId5 = 1924497635,
                    }
                },
                Chapter =
                {
                    new Chapter
                    {
                        Uid = 50443193,
                        ChapterId = 1011,
                        Status = 1,
                    },
                    new Chapter
                    {
                        Uid = 50443193,
                        ChapterId = 4001,
                        Status = 1,
                    },
                },
                Stage =
                {
                    new Stage
                    {
                        Uid = 50443924,
                        StageId = 40001,
                        Status = 2,
                        Score = 5040,
                        Rank = 5,
                        UnitIdx = 1,
                        UpdatedAt = 1749572641,
                    },
                    new Stage
                    {
                        Uid = 50443924,
                        StageId = 101102,
                        Status = 2,
                        Score = 17124,
                        Rank = 5,
                        UnitIdx = 1,
                        UpdatedAt = 1749572724,
                    },
                    new Stage
                    {
                        Uid = 50443924,
                        StageId = 101102,
                        Status = 2,
                        Score = 17124,
                        Rank = 5,
                        UnitIdx = 1,
                        UpdatedAt = 1749572833,
                    },
                },
                Card =
                {
                    new Card
                    {
                        Id = 1924314499,
                        Uid = 50443193,
                        CardId = 10001,
                        CardUniqueId = "1d30a269-1bd5-4f26-bed9-0892448c4171",
                        CardPropertyId = 100011,
                        Level = 1,
                        PassiveSkillLevel1 = 1,
                        AwakePriority = 1,
                        Protect = 1,
                        ResourceIdx = 1,
                    },
                },
                Item =
                {
                    new Item
                    {
                        Uid = 50443924,
                        ItemId = 3008,
                        Quantity = 1,
                        Duration = 0,
                        ExpireDate = 1749573375
                    },
                    new Item
                    {
                        Uid = 50443924,
                        ItemId = 5003,
                        Quantity = 1,
                        Duration = 0,
                        ExpireDate = 1749694376
                    },
                    new Item
                    {
                        Uid = 50443924,
                        ItemId = 7001,
                        Quantity = 5,
                        Duration = 0,
                        ExpireDate = 1749573053
                    },
                    new Item
                    {
                        Uid = 50443924,
                        ItemId = 7004,
                        Quantity = 1,
                        Duration = 0,
                        ExpireDate = 1749573053
                    },
                    new Item
                    {
                        Uid = 50443924,
                        ItemId = 9210,
                        Quantity = 10,
                        Duration = 0,
                        ExpireDate = 1749573300
                    },
                    new Item
                    {
                        Uid = 50443924,
                        ItemId = 17123,
                        Quantity = 55,
                        Duration = 0,
                        ExpireDate = 1749573053
                    },
                    new Item
                    {
                        Uid = 50443924,
                        ItemId = 17143,
                        Quantity = 55,
                        Duration = 0,
                        ExpireDate = 1750379894
                    },
                    new Item
                    {
                        Uid = 50443924,
                        ItemId = 20823,
                        Quantity = 9,
                        Duration = 0,
                        ExpireDate = 1749573053
                    },
                    new Item
                    {
                        Uid = 50443924,
                        ItemId = 20824,
                        Quantity = 3,
                        Duration = 0,
                        ExpireDate = 1750380763
                    },
                    new Item
                    {
                        Uid = 50443924,
                        ItemId = 55016,
                        Quantity = 1,
                        Duration = 0,
                        ExpireDate = 1749684691
                    },
                    new Item
                    {
                        Uid = 50443924,
                        ItemId = 190202,
                        Quantity = 1,
                        Duration = 0,
                        ExpireDate = 1749573375
                    }
                },
                MemberPicture =
                {
                    new MemberPicture { Uid = 50443924, MemberPictureId = 10001, ReleasedEmotionResourceIds = "" },
                    new MemberPicture { Uid = 50443924, MemberPictureId = 10002, ReleasedEmotionResourceIds = "" },
                    new MemberPicture { Uid = 50443924, MemberPictureId = 10003, ReleasedEmotionResourceIds = "" },
                    new MemberPicture { Uid = 50443924, MemberPictureId = 10004, ReleasedEmotionResourceIds = "" },
                    new MemberPicture { Uid = 50443924, MemberPictureId = 10005, ReleasedEmotionResourceIds = "" },
                    new MemberPicture { Uid = 50443924, MemberPictureId = 10015, ReleasedEmotionResourceIds = "" },
                    new MemberPicture { Uid = 50443924, MemberPictureId = 10017, ReleasedEmotionResourceIds = "" },
                    new MemberPicture { Uid = 50443924, MemberPictureId = 10023, ReleasedEmotionResourceIds = "" },
                    new MemberPicture { Uid = 50443924, MemberPictureId = 10028, ReleasedEmotionResourceIds = "" },
                    new MemberPicture { Uid = 50443924, MemberPictureId = 10029, ReleasedEmotionResourceIds = "" },
                    new MemberPicture { Uid = 50443924, MemberPictureId = 10034, ReleasedEmotionResourceIds = "" },
                    new MemberPicture { Uid = 50443924, MemberPictureId = 10035, ReleasedEmotionResourceIds = "" },
                    new MemberPicture { Uid = 50443924, MemberPictureId = 10036, ReleasedEmotionResourceIds = "" },
                    new MemberPicture { Uid = 50443924, MemberPictureId = 10046, ReleasedEmotionResourceIds = "" },
                    new MemberPicture { Uid = 50443924, MemberPictureId = 10067, ReleasedEmotionResourceIds = "" },
                    new MemberPicture { Uid = 50443924, MemberPictureId = 10071, ReleasedEmotionResourceIds = "" },
                    new MemberPicture { Uid = 50443924, MemberPictureId = 10072, ReleasedEmotionResourceIds = "" },
                    new MemberPicture { Uid = 50443924, MemberPictureId = 10073, ReleasedEmotionResourceIds = "" },
                    new MemberPicture { Uid = 50443924, MemberPictureId = 10074, ReleasedEmotionResourceIds = "" },
                    new MemberPicture { Uid = 50443924, MemberPictureId = 10075, ReleasedEmotionResourceIds = "" },
                    new MemberPicture { Uid = 50443924, MemberPictureId = 10128, ReleasedEmotionResourceIds = "" },
                    new MemberPicture { Uid = 50443924, MemberPictureId = 1, ReleasedEmotionResourceIds = "" },
                    new MemberPicture { Uid = 50443924, MemberPictureId = 2, ReleasedEmotionResourceIds = "" },
                    new MemberPicture { Uid = 50443924, MemberPictureId = 3, ReleasedEmotionResourceIds = "" },
                    new MemberPicture { Uid = 50443924, MemberPictureId = 4, ReleasedEmotionResourceIds = "" },
                    new MemberPicture { Uid = 50443924, MemberPictureId = 5, ReleasedEmotionResourceIds = "" }
                },
                HomeBackground =
                {
                    new HomeBackground
                    {
                        Uid = 50443924,
                        HomeBackgroundId = 10028,
                    },
                },
                Book =
                {
                    new Book
                    {
                        Id = 420358966,
                        Uid = 50443193,
                        CardId = 10001,
                        Level = 1,
                        PassiveSkillLevel1 = 1,
                        AllCardId = 1924314499,
                        MemberCardId = 1924314499,
                        CostumeCardId = 1924314499,
                        GroupCardId = "1:1924314499",
                    }
                },
                Story =
                {
                    new Story()
                    {
                        Uid = 50443924,
                        StoryId = 70010101,
                        Status = 2,
                    }
                },
                GachaHistory =
                {
                    new GachaHistory
                    {
                        Uid = 50443924,
                        GachaId = 8001,
                        Step = 0,
                        CurrentBoxId = 0,
                        RankupBoxId = 0,
                        SingleCnt = 0,
                        TicketCnt = 0,
                        LumpCnt = 1,
                        TotalCnt = 1,
                        LimitCnt = 1,
                        GroupWeightCnt = 0,
                        IsPaid = 0,
                        PaidDate = 1749572351,
                        LastGachaDate = 1749572351,
                        PaidSingleResetCount = 0,
                        PaidSingleResetDate = 1749572351,
                        PaidLumpResetCount = 0,
                        PaidLumpResetDate = 1749572351
                    }
                },
                Challenge =
                {
                    new Challenge()
                    {
                        Uid = 50443924,
                        ChallengeId = 2102,
                        Value = 2,
                        UpdatedAt = 1749572832,
                    }
                },
                ShopFragmentLock = new ShopFragmentLock()
                {
                    Uid = 0,
                    ShopIds = "",
                    ManualUpdatedAt = 946652400,
                    AutoUpdatedAt = 0
                },
                PresentCount = 1,
                Review = 0,
                LoginBonusFlag = 0,
                NewsLatestId = 3294,
                NewsUnreadCount = 4,
                ChallengeDone = new ChallengeDoneCount()
                {

                },
                ChallengeGroup =
                {
                    new ChallengeGroup
                    {
                        Uid = 50443924,
                        GroupId = 2001,
                        Status = 1,
                        UpdatedAt = 1750410465
                    }
                },
                RetryablePuzzle = new RetryablePuzzle()
                {

                },
                Work = new Work()
                {
                    Uid = 50443924,
                    DailyCount = 0,
                    TotalCount = 0,
                    DailyUpdateCount = 0,
                    DailyResetCount = 0,
                    ResultCount1 = 0,
                    ResultCount2 = 0,
                    ResultCount3 = 0,
                    LastWorkDate = 1749572315,
                    LastLineupUpdateDate = 1749572315,
                    LastResetDate = 1749572315
                },
                WorkLineup =
                {
                    new WorkLineup
                    {
                        Id = 371496484,
                        Uid = 50443924,
                        WorkId = 1010028,
                        Status = 0,
                        ExpectValue = 0,
                        Result = 0,
                        LottedConditionId1 = 0,
                        LottedConditionId2 = 0,
                        LottedConditionId3 = 0,
                        CardIds = "",
                        CardResults = "",
                        MvpCardId = 0,
                        LottedRewardSeqIds1 = "1",
                        LottedRewardSeqIds2 = "1",
                        LottedRewardSeqIds3 = "1",
                        StartDate = 1749572317,
                        EndDate = 1749572317,
                        ShortMin = 0,
                        AddCoin = 0
                    },
                    new WorkLineup
                    {
                        Id = 371496485,
                        Uid = 50443924,
                        WorkId = 1030016,
                        Status = 0,
                        ExpectValue = 0,
                        Result = 0,
                        LottedConditionId1 = 0,
                        LottedConditionId2 = 0,
                        LottedConditionId3 = 0,
                        CardIds = "",
                        CardResults = "",
                        MvpCardId = 0,
                        LottedRewardSeqIds1 = "1",
                        LottedRewardSeqIds2 = "1",
                        LottedRewardSeqIds3 = "1",
                        StartDate = 1749572317,
                        EndDate = 1749572317,
                        ShortMin = 0,
                        AddCoin = 0
                    }
                },
                HomePicture = new HomePicture()
                {

                },
                TeamBattlePuzzle = new TeamBattlePuzzle()
                {

                },
                AppIcon =
                {
                    new AppIcon { Uid = 50443924, AppIconId = 1 },
                    new AppIcon { Uid = 50443924, AppIconId = 2 },
                    new AppIcon { Uid = 50443924, AppIconId = 3 },
                    new AppIcon { Uid = 50443924, AppIconId = 4 },
                    new AppIcon { Uid = 50443924, AppIconId = 5 },
                    new AppIcon { Uid = 50443924, AppIconId = 6 },
                    new AppIcon { Uid = 50443924, AppIconId = 7 },
                    new AppIcon { Uid = 50443924, AppIconId = 8 },
                    new AppIcon { Uid = 50443924, AppIconId = 9 },
                    new AppIcon { Uid = 50443924, AppIconId = 10 },
                    new AppIcon { Uid = 50443924, AppIconId = 11 },
                    new AppIcon { Uid = 50443924, AppIconId = 12 },
                    new AppIcon { Uid = 50443924, AppIconId = 13 },
                    new AppIcon { Uid = 50443924, AppIconId = 14 },
                    new AppIcon { Uid = 50443924, AppIconId = 15 }
                },
                HomeActor = {
                    new HomeActor
                    {
                        Uid = 0,
                        CharacterId = 1,
                        ModelKindId = 1,
                        ClothesId = 11,
                        FacialPresetId = 0,
                        Position = 0,
                        BodyTapReaction = 1,
                        BodyTapMotion1 = 111,
                        BodyTapMotion2 = 0,
                        BodyTapMotion3 = 0,
                        BodyTapMotion4 = 0,
                        BodyTapMotion5 = 0,
                        FaceTapReaction = 1,
                        FaceTapMotion1 = 110,
                        FaceTapMotion2 = 0,
                        FaceTapMotion3 = 0,
                        FaceTapMotion4 = 0,
                        FaceTapMotion5 = 0
                    }
                },
                PhotoBooth =
                {
                    new PhotoBooth { Uid = 50443924, PhotoBoothId = 20001 },
                    new PhotoBooth { Uid = 50443924, PhotoBoothId = 30001 },
                    new PhotoBooth { Uid = 50443924, PhotoBoothId = 30002 },
                    new PhotoBooth { Uid = 50443924, PhotoBoothId = 30003 },
                    new PhotoBooth { Uid = 50443924, PhotoBoothId = 30004 },
                    new PhotoBooth { Uid = 50443924, PhotoBoothId = 49001 },
                    new PhotoBooth { Uid = 50443924, PhotoBoothId = 49002 },
                    new PhotoBooth { Uid = 50443924, PhotoBoothId = 49003 },
                    new PhotoBooth { Uid = 50443924, PhotoBoothId = 49004 },
                    new PhotoBooth { Uid = 50443924, PhotoBoothId = 49005 }
                },
                BondsSeason =
                {
                    new BondsSeason
                    {
                        Uid = 50443924,
                        SeasonId = 56,
                        MemberId = 0,
                        Score = 44501,
                        Ranking = 0,
                        PremiumFlag = 0,
                        PremiumLoveFlag = 0,
                        StandardConfessionStep = 0,
                        PremiumConfessionStep = 0,
                        StandardLoveStep = 0,
                        PremiumLoveStep = 0,
                        LastSeenStep = 0,
                        LastSeenLoveStep = 0
                    }
                },
                Mileage = new Mileage()
                {
                    Uid = 50443924,
                    Point = 0,
                    Grade = 0,
                    DailyRewardReceivedAt = 1749485876,
                    PuzzleSkipCount = 0,
                    PuzzleSkippedAt = 1749485876
                },
                HomeMemberClothes =
                {
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 92 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 123 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 161 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 163 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 164 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 165 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 202 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 215 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 224 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 235 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 303 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 335 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 402 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 463 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 472 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 482 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 484 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 554 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 571 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 584 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 664 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 691 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 783 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 785 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 801 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 822 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 841 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 843 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 844 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 882 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 884 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 885 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 903 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 934 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 942 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 951 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 953 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 1011 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 1013 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 1022 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 1094 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 1112 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 1144 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 1155 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 1162 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 1183 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 1185 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 1241 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 1292 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 1305 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 1333 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 1353 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 11 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 142 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 144 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 145 },
                    new HomeMemberClothes { Uid = 50443924, ClothesId = 153 }
                },
                HomeMemberTapMotion =
                {
                    new HomeMemberTapMotion { Uid = 50443924, TapMotionId = 110 },
                    new HomeMemberTapMotion { Uid = 50443924, TapMotionId = 111 },
                    new HomeMemberTapMotion { Uid = 50443924, TapMotionId = 120 },
                    new HomeMemberTapMotion { Uid = 50443924, TapMotionId = 121 },
                    new HomeMemberTapMotion { Uid = 50443924, TapMotionId = 130 },
                    new HomeMemberTapMotion { Uid = 50443924, TapMotionId = 131 },
                    new HomeMemberTapMotion { Uid = 50443924, TapMotionId = 140 },
                    new HomeMemberTapMotion { Uid = 50443924, TapMotionId = 141 },
                    new HomeMemberTapMotion { Uid = 50443924, TapMotionId = 150 },
                    new HomeMemberTapMotion { Uid = 50443924, TapMotionId = 151 }
                },
                BondsSeasonRanking = new BondsSeasonRanking()
                {
                    SeasonId = 56,
                    Score = 44501,
                    Rank = 8696
                },
                FriendApprovalCount = 0,
                FriendCanGreetingCount = 0,
                LastBondsSeasonRanking = new BondsSeasonRanking()
                {
                    SeasonId = 55,
                    Score = 0,
                    Rank = 0
                },
                PhotoStamp =
                {
                    new PhotoStamp { Uid = 50443924, PhotoStampId = 50001 },
                    new PhotoStamp { Uid = 50443924, PhotoStampId = 50002 },
                    new PhotoStamp { Uid = 50443924, PhotoStampId = 50003 },
                    new PhotoStamp { Uid = 50443924, PhotoStampId = 50004 },
                    new PhotoStamp { Uid = 50443924, PhotoStampId = 50005 }
                },
                Advertising =
                {
                    new Advertising
                    {
                        Uid = 50443924,
                        AdvertisingId = 1,
                        ViewCount = 0,
                        RewardCountRemaining = 0,
                        LastResetAt = 1750378118,
                        LastViewedAt = 0
                    },
                    new Advertising
                    {
                        Uid = 50443924,
                        AdvertisingId = 2,
                        ViewCount = 0,
                        RewardCountRemaining = 0,
                        LastResetAt = 1750410319,
                        LastViewedAt = 0
                    },
                    new Advertising
                    {
                        Uid = 50443924,
                        AdvertisingId = 4,
                        ViewCount = 0,
                        RewardCountRemaining = 0,
                        LastResetAt = 1750378118,
                        LastViewedAt = 0
                    },
                    new Advertising
                    {
                        Uid = 50443924,
                        AdvertisingId = 5,
                        ViewCount = 0,
                        RewardCountRemaining = 0,
                        LastResetAt = 1750378118,
                        LastViewedAt = 0
                    },
                    new Advertising
                    {
                        Uid = 50443924,
                        AdvertisingId = 6,
                        ViewCount = 0,
                        RewardCountRemaining = 0,
                        LastResetAt = 1750378118,
                        LastViewedAt = 0
                    },
                    new Advertising
                    {
                        Uid = 50443924,
                        AdvertisingId = 8,
                        ViewCount = 0,
                        RewardCountRemaining = 0,
                        LastResetAt = 1750378118,
                        LastViewedAt = 0
                    },
                    new Advertising
                    {
                        Uid = 50443924,
                        AdvertisingId = 9,
                        ViewCount = 0,
                        RewardCountRemaining = 0,
                        LastResetAt = 1750378118,
                        LastViewedAt = 0
                    },
                    new Advertising
                    {
                        Uid = 50443924,
                        AdvertisingId = 10,
                        ViewCount = 0,
                        RewardCountRemaining = 0,
                        LastResetAt = 1750410319,
                        LastViewedAt = 0
                    },
                    new Advertising
                    {
                        Uid = 50443924,
                        AdvertisingId = 11,
                        ViewCount = 0,
                        RewardCountRemaining = 0,
                        LastResetAt = 1750378118,
                        LastViewedAt = 0
                    },
                    new Advertising
                    {
                        Uid = 50443924,
                        AdvertisingId = 10074,
                        ViewCount = 0,
                        RewardCountRemaining = 0,
                        LastResetAt = 1750378118,
                        LastViewedAt = 0
                    },
                    new Advertising
                    {
                        Uid = 50443924,
                        AdvertisingId = 10081,
                        ViewCount = 0,
                        RewardCountRemaining = 0,
                        LastResetAt = 1750378118,
                        LastViewedAt = 0
                    },
                    new Advertising
                    {
                        Uid = 50443924,
                        AdvertisingId = 10082,
                        ViewCount = 0,
                        RewardCountRemaining = 0,
                        LastResetAt = 1750378118,
                        LastViewedAt = 0
                    },
                    new Advertising
                    {
                        Uid = 50443924,
                        AdvertisingId = 11823,
                        ViewCount = 0,
                        RewardCountRemaining = 0,
                        LastResetAt = 1750378118,
                        LastViewedAt = 0
                    },
                    new Advertising
                    {
                        Uid = 50443924,
                        AdvertisingId = 11824,
                        ViewCount = 0,
                        RewardCountRemaining = 0,
                        LastResetAt = 1750378118,
                        LastViewedAt = 0
                    },
                    new Advertising
                    {
                        Uid = 50443924,
                        AdvertisingId = 11831,
                        ViewCount = 0,
                        RewardCountRemaining = 0,
                        LastResetAt = 1750378118,
                        LastViewedAt = 0
                    }
                },
                CookingSummary = new CookingSummary()
                {
                    Uid = 0,
                    MaxProficiencyRecipeQuantity = 0,
                },
                MemberLikabilitylevelFlag = new MemberLikabilitylevelFlag()
                {

                }
            };

            return data;
        }
    }
}