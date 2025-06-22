using Google.Protobuf;
using Serilog;
using YotsubaBestGirl.Common.Core;
using YotsubaBestGirl.Common.Utils;
using YotsubaBestGirl.Core;
using YotsubaBestGirl.Database;
using YotsubaBestGirl.Database.Entities;
using YotsubaBestGirl.GameServer.Services;
using YotsubaBestGirl.Proto.Pcommon;
using YotsubaBestGirl.Proto.Pmisc;
using YotsubaBestGirl.Proto.Proto;
using YotsubaBestGirl.Proto.Puser;

namespace YotsubaBestGirl.GameServer.Controllers.Api.ProtocolHandlers
{
    // handlers in here is responsible for packets sent during login till lobby, or just testing ones lol
    public class LoginHandler : ProtocolHandlerBase
    {
        private readonly ResourceService resourceService;


        public LoginHandler(IProtocolHandlerFactory protocolHandlerFactory, ResourceService _resourceService) : base(protocolHandlerFactory)
        {
            resourceService = _resourceService;

        }

        [ProtocolHandler(Protocol.resource_list_Android)]
        public HttpMessage ResourceListAndroidHandler()
        {
            var resp = resourceService.GetResource<Resources>(Protocol.resource_list_Android);

            var extraHeaders = new Dictionary<string, string>();

            extraHeaders["X-Enish-App-Resource-Cnt"] = "54055";

            return HttpMessage.Create(resp, doGzip: true, extraHeaders);
        }

        [ProtocolHandler(Protocol.master_all)]
        public HttpMessage MasterAllHandler()
        {
            var resp = resourceService.GetResource<Proto.Pmaster.All>(Protocol.master_all);

            return HttpMessage.Create(resp, true);
        }

        [ProtocolHandler(Protocol.fcm_token)]
        public Proto.Proto.Nocontent FcmTokenHandler()
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
        public Proto.Proto.IAPProductList ShopProductsHandler(RequestPacket req) // one case of reqParams being used, there are two it can be all=1 or all=1, resp prob different depending on that
        {
            //Log.Information($"ShopProductsHandler called with params: ");
            //Util.PrintDictionary(reqParams);

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