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
                HomePicture = new HomePicture()
                {

                },
                TeamBattlePuzzle = new TeamBattlePuzzle()
                {

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
                CookingSummary = new CookingSummary()
                {
                    Uid = 0,
                    MaxProficiencyRecipeQuantity = 0,
                },
                MemberLikabilitylevelFlag = new MemberLikabilitylevelFlag()
                {

                },
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
            data.Advertising.AddRange(user.Advertising.ToProtoList());
            data.Stage.AddRange(user.Stages.ToProtoList());
            data.PhotoStamp.AddRange(user.PhotoStamps.ToProtoList());
            data.HomeMemberClothes.AddRange(user.HomeMemberClothes.ToProtoList());
            data.HomeMemberTapMotion.AddRange(user.HomeMemberTapMotions.ToProtoList());
            data.WorkLineup.AddRange(user.WorkLineups.ToProtoList());
            data.AppIcon.AddRange(user.AppIcons.ToProtoList());

            return data;
        }
    }
}
