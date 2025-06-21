using YotsubaBestGirl.Core;

namespace YotsubaBestGirl.GameServer.Controllers.Api.ProtocolHandlers
{
    public class Gacha : ProtocolHandlerBase
    {
        public Gacha(IProtocolHandlerFactory protocolHandlerFactory) : base(protocolHandlerFactory)
        {

        }

        [ProtocolHandler(Protocol.gacha_purchase)]
        public Proto.Proto.GachaResult GachaPurchaseHandler(IQueryCollection? reqParams)
        {
            var resp = new Proto.Proto.GachaResult()
            {
                StoredData = Account.GetUserData(),
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
