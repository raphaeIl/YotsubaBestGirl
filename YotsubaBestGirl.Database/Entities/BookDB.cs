using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YotsubaBestGirl.Common.Utils;

namespace YotsubaBestGirl.Database.Entities
{
    [Table("t_user_book")]
    public class BookDB : UserOwnedEntity, IProtoConvertible<Proto.Puser.Book>
    {
        public int CardId { get; set; }
        public int CardPremiumId { get; set; }
        public int IsPairCard { get; set; }
        public int IsSignedCard { get; set; }
        public int SerialNumber { get; set; }
        public int Level { get; set; }
        public int ActiveSkillLevel { get; set; }
        public int PassiveSkillLevel1 { get; set; }
        public int PassiveSkillLevel2 { get; set; }
        public int PassiveSkillLevel3 { get; set; }
        public int LimitbreakRank { get; set; }
        public int Kirameki { get; set; }
        public int Tokimeki { get; set; }
        public int Kiratoki { get; set; }
        public int LimitbreakRankSigned { get; set; }
        public int KiramekiSigned { get; set; }
        public int TokimekiSigned { get; set; }
        public int KiratokiSigned { get; set; }
        public int PictureResourceIdx1 { get; set; }
        public int PictureResourceIdx6 { get; set; }
        public int PictureResourceIdx7 { get; set; }
        public int PictureResourceIdx8 { get; set; }
        public int InterludeVoice1 { get; set; }
        public int InterludeVoice2 { get; set; }
        public int InterludeVoice3 { get; set; }
        public int InterludeVoice4 { get; set; }
        public int InterludeVoice5 { get; set; }
        public int InterludeVoice6 { get; set; }
        public int InterludeVoice7 { get; set; }
        public int Sell { get; set; }
        public int BuyBack { get; set; }
        public long AllCardId { get; set; }
        public long MemberCardId { get; set; }
        public long CostumeCardId { get; set; }
        public string? GroupCardId { get; set; }
        public string? AcquiredGrowthRewardSeqIds { get; set; }

        public BookDB(int cardId)
        {
            CardId = cardId;
            Level = 1;
            PassiveSkillLevel1 = 1;
            AllCardId = 1924314499;
            MemberCardId = 1924314499;
            CostumeCardId = 1924314499;
            GroupCardId = "1:1924314499";
            GroupCardId = "";
            AcquiredGrowthRewardSeqIds = "";
        }

        public Proto.Puser.Book ToProto()
        {
            string jsonStr = JsonConvert.SerializeObject(this);

            return JsonConvert.DeserializeObject<Proto.Puser.Book>(jsonStr);
        }

    }
}
