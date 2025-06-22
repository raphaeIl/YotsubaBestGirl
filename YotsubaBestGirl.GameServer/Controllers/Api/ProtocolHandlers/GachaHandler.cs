using Microsoft.EntityFrameworkCore;
using YotsubaBestGirl.Common.Core;
using YotsubaBestGirl.Core;
using YotsubaBestGirl.Database;
using YotsubaBestGirl.GameServer.Services;

namespace YotsubaBestGirl.GameServer.Controllers.Api.ProtocolHandlers
{
    public class GachaHandler : ProtocolHandlerBase
    {
        private readonly ISessionService sessionService;

        private YotsubaContext context;

        public GachaHandler(IProtocolHandlerFactory protocolHandlerFactory, ISessionService _sessionService, YotsubaContext dbContext) : base(protocolHandlerFactory)
        {
            sessionService = _sessionService;
            context = dbContext;
        }

        [ProtocolHandler(Protocol.gacha_purchase)]
        public Proto.Proto.GachaResult GachaPurchaseHandler(RequestPacket req)
        {
            int userId = sessionService.GetPlayerIdBySession(req.GetSessionId());

            var resp = new Proto.Proto.GachaResult()
            {
                StoredData = UserHandler.GetUserData(context, userId),
            };

            var testCard = new Proto.Proto.Goods()
            {
                Category = 2,
                TargetId = 10154,
                Quantity = 1,
                CardParam = new Proto.Proto.GoodsCardParam()
                {
                    Level = 1,
                    ActiveSkillLevel = 1,
                    PassiveSkillLevel1 = 1,
                    PassiveSkillLevel2 = 0,
                    PassiveSkillLevel3 = 0,
                    PropertyId = 101541,
                    PropertyId2 = 0,
                    CardPremiumId = 0,
                    IsPairCard = 0,
                    IsSignedCard = 0,
                    SerialNumber = 0,
                    LimibreakRank = 0,
                    InterludeVoice1 = 0,
                    InterludeVoice2 = 0,
                    InterludeVoice3 = 0,
                    InterludeVoice4 = 0,
                    InterludeVoice5 = 0,
                },
                Bonus =
                {

                },
                RewardId = 0,
                RewardSeqId = 0,
                HomeMemberPictureParam = null,
                CouponCode = "",
                UnitSkillUniqueId = "",
                FeatureItemChapterId = 0,
            };

            for (int i = 0; i < 10; i++)
            {
                resp.Cards.Add(testCard);
            }

            return resp;
        }
    }
}
