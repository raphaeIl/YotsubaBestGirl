using Serilog;
using YotsubaBestGirl.Common.Core;
using YotsubaBestGirl.Common.Utils;
using YotsubaBestGirl.Core;
using YotsubaBestGirl.Database;
using YotsubaBestGirl.Database.Entities;
using YotsubaBestGirl.GameServer.Services;
using YotsubaBestGirl.Proto.Pmisc;
using YotsubaBestGirl.Proto.Proto;
using YotsubaBestGirl.Proto.Puser;

namespace YotsubaBestGirl.GameServer.Controllers.Api.ProtocolHandlers
{
    public class UserHandler : ProtocolHandlerBase
    {
        private readonly ISessionService sessionService;

        private YotsubaContext context; // prob a bad idea to have this here

        public UserHandler(IProtocolHandlerFactory protocolHandlerFactory, ISessionService _sessionService, YotsubaContext dbContext) : base(protocolHandlerFactory)
        {
            sessionService = _sessionService;
            context = dbContext;
        }

        [ProtocolHandler(Protocol.account_authorize)]
        public AccountAuthorize AccountAuthorizeHandler(RequestPacket req)
        {
            string uuid = req.Form["uuid"];

            // create session with new key every login! (already better security than official servers! fr tho not even joking)
            string sessionKey = sessionService.CreateSession(uuid);

            Log.Information("Created session for uuid[{uuid}] with sessionKey: {sessionKey}", uuid, sessionKey);
            int playerId = sessionService.GetPlayerIdBySession(sessionKey);

            if (playerId == -1)
            {
                Log.Error("Unable to get playerId for uuid: {uuid}.", uuid);
                throw new InvalidDataException("Unable to get playerId for uuid: " + uuid);
            }
            
            if (!sessionService.TryGetUser(sessionKey, out UserDB user))
            {
                // this means new account 
                user = sessionService.CreateUser(sessionKey);

                context.SaveChanges();
            }

            var resp = new AccountAuthorize()
            {
                Session = sessionKey,
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

            return resp;
        }

        [ProtocolHandler(Protocol.account_certificate)]
        public AccountCertificate AccountCertificateHandler(RequestPacket req)
        {
            //Log.Information($"account_certificate called with params: ");
            //Util.PrintDictionary(reqParams.);

            return new AccountCertificate()
            {
                Version = new Proto.Pmaster.Version()
                {
                    Platform = "android",
                    Application = Config.GameVersionNumber,
                    Resource = Config.ResourceVersion,
                    Master = Config.MasterVersion,
                    Term = 2,
                    Revision = 2
                }
            };
        }

        [ProtocolHandler(Protocol.user_load)]
        public HttpMessage UserLoadHandler(RequestPacket req)
        {
            int userId = sessionService.GetPlayerIdBySession(req.GetSessionId());

            var resp = new Proto.Proto.Nocontent()
            {
                StoredData = UserHandler.GetUserData(context, userId)
            };

            return HttpMessage.Create(resp, true);
        }

        // hardcoded for now, db later
        public static StoredData GetUserData(YotsubaContext context, int userId)
        {
            UserDB user = context.Users.Where(u => u.Uid == userId).FirstOrDefault();

            // no db yet, so everything hardcoded ugly
            var data = new StoredData
            {
                User = user.ToProto(),
                Currency = user.Currency.ToProto(),
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
                MemberDearlevelFlag = new MemberDearlevelFlag()
                {

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
                HomeBackground =
                {
                    new HomeBackground
                    {
                        Uid = 50443924,
                        HomeBackgroundId = 10028,
                    },
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

            data.Clear.AddRange(user.Clear);
            data.Options = user.Options.FirstOrDefault().ToProto(); // there should not be more than one of this, should prob add check

            data.Member.AddRange(user.Members.ToProtoList());
            data.Unit.AddRange(user.Units.ToProtoList());
            data.Chapter.AddRange(user.Chapters.ToProtoList());
            data.Item.AddRange(user.Items.ToProtoList());
            data.MemberPicture.AddRange(user.MemberPictures.ToProtoList());
            data.Story.AddRange(user.Stories.ToProtoList());
            data.GachaHistory.AddRange(user.GachaHistory.ToProtoList());
            data.Card.AddRange(user.Cards.ToProtoList());
            data.Book.AddRange(user.Books.ToProtoList());

            return data;
        }
    }
}
