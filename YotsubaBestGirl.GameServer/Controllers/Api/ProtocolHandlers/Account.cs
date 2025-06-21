using Google.Protobuf;
using Serilog;
using YotsubaBestGirl.Common.Utils;
using YotsubaBestGirl.Core;
using YotsubaBestGirl.Proto.Pcommon;
using YotsubaBestGirl.Proto.Proto;
using YotsubaBestGirl.Proto.Puser;

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
            var data = new StoredData
            {
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
                    Leaf = 2794,
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
                    FreeCoin = 60,
                    TotalCoin = 60,
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
                Puzzle = new Puzzle
                {
                    Uid = 50443193,
                    Puid = "3991b072-8273-4c25-a1b4-96d0ee9c8b73",
                    Status = 2,
                    StageId = 101102,
                    RoundId = 101102,
                    HelperUid = 49966922,
                    UsedAp = 1,
                    StartedAt = 1749497296,
                },
                PresentCount = 22,
                NewsLatestId = 3286,
                NewsUnreadCount = 6,
                Mileage = new Mileage
                {
                    Uid = 50443193,
                    DailyRewardReceivedAt = 1749409163,
                    PuzzleSkippedAt = 1749409163,
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
                    new Card
                    {
                        Id = 1924314502,
                        Uid = 50443193,
                        CardId = 10004,
                        CardUniqueId = "2d1565f5-7a7f-46e1-9582-2b22314372e4",
                        CardPropertyId = 100041,
                        Exp = 10,
                        Level = 4,
                        PassiveSkillLevel1 = 1,
                        AwakePriority = 1,
                        Protect = 1,
                        ResourceIdx = 1,
                    },
                    new Card
                    {
                        Id = 1924314517,
                        Uid = 50443193,
                        CardId = 10553,
                        CardUniqueId = "48a3b024-63d5-474d-a929-64590c5ee4f7",
                        CardPropertyId = 105531,
                        Exp = 10,
                        Level = 1,
                        ActiveSkillLevel = 1,
                        PassiveSkillLevel1 = 1,
                        AwakePriority = 1,
                        Protect = 1,
                        ResourceIdx = 1,
                    },
                },

                Stage =
                {
                    new Stage
                    {
                        Uid = 50443193,
                        StageId = 40001,
                        Status = 2,
                        Score = 5040,
                        Rank = 5,
                        UnitIdx = 1,
                        UpdatedAt = 1749497177,
                    },
                    new Stage
                    {
                        Uid = 50443193,
                        StageId = 101102,
                        Status = 2,
                        Score = 17124,
                        Rank = 5,
                        UnitIdx = 1,
                        UpdatedAt = 1749497353,
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
                    },
                    new Book
                    {
                        Id = 420358990,
                        Uid = 50443193,
                        CardId = 10553,
                        Level = 1,
                        ActiveSkillLevel = 1,
                        PassiveSkillLevel1 = 1,
                        AllCardId = 1924314517,
                        MemberCardId = 1924314517,
                        CostumeCardId = 1924314517,
                    },
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

                AppIcon =
                {
                    new AppIcon
                    {
                        Uid = 50443193,
                        AppIconId = 1,
                    },
                    new AppIcon
                    {
                        Uid = 50443193,
                        AppIconId = 15,
                    },
                },
            };

            //return new Proto.Proto.Nocontent()
            //{
            //    StoredData = data
            //};

            var pcapResp = (Proto.Proto.Nocontent)PcapParser.PcapParser.Instance.GetPcapPacket(Protocol.user_load);

            pcapResp.StoredData.Currency = new Currency
            {
                FreeCoin = int.MaxValue,
                TotalCoin = int.MaxValue,
            };

            pcapResp.StoredData.User.SessionId = "seggs";

            pcapResp.StoredData.User.Leaf = int.MaxValue;

            return pcapResp;
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

        [ProtocolHandler(Protocol.gacha_purchase)]
        public Proto.Proto.GachaResult GachaPurchaseHandler(IQueryCollection? reqParams)
        {
            var pcapResp = (Proto.Proto.GachaResult)PcapParser.PcapParser.Instance.GetPcapPacket(Protocol.gacha_purchase);

            var templateCard = pcapResp.Cards[2];

            pcapResp.Cards.Clear();

            for (int i = 0; i < 10; i++)
            {
                pcapResp.Cards.Add(templateCard);
            }

            return pcapResp;
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

    }
}