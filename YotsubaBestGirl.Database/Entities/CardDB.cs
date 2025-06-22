using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YotsubaBestGirl.Proto.Pmaster;

namespace YotsubaBestGirl.Database.Entities
{
    // all models: Uid is the unique playerId, Id is the global unique id for this item, CardId, is the card's id, identical across db
    [Table("cards")]
    public class CardDB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public int Uid { get; set; }

        [JsonIgnore]
        [ForeignKey("Uid")]
        public UserDB User { get; set; }

        // above 3 are all required for all entities for the database to function

        [Required]
        public int CardId { get; set; }

        public string? CardUniqueId { get; set; } // uuid as guid str?
        public int CardPropertyId { get; set; }
        public int CardPropertyId2 { get; set; }
        public int CardPremiumId { get; set; }
        public int IsPairCard { get; set; }
        public int IsSignedCard { get; set; }
        public int SerialNumber { get; set; }
        public int Exp { get; set; }
        public int Level { get; set; }
        public int LevelAwake { get; set; }
        public int ActiveSkillExp { get; set; }
        public int ActiveSkillLevel { get; set; }
        public int PassiveSkillExp1 { get; set; }
        public int PassiveSkillLevel1 { get; set; }
        public int PassiveSkillExp2 { get; set; }
        public int PassiveSkillLevel2 { get; set; }
        public int PassiveSkillExp3 { get; set; }
        public int PassiveSkillLevel3 { get; set; }
        public int LimitbreakRank { get; set; }
        public int AwakePriority { get; set; }
        public int Kirameki { get; set; }
        public int Tokimeki { get; set; }
        public int InterludeVoice1 { get; set; }
        public int InterludeVoice2 { get; set; }
        public int InterludeVoice3 { get; set; }
        public int InterludeVoice4 { get; set; }
        public int InterludeVoice5 { get; set; }
        public string? AcquiredGrowthRewardSeqIds { get; set; }
        public int Protect { get; set; }
        public int Bgm { get; set; }
        public int ResourceIdx { get; set; }

        public CardDB()
        {
            CardId = 10001;
            CardUniqueId = "1d30a269-1bd5-4f26-bed9-0892448c4171";
            CardPropertyId = 100011;
            Level = 1;
            PassiveSkillLevel1 = 1;
            AwakePriority = 1;
            Protect = 1;
            ResourceIdx = 1;
            AcquiredGrowthRewardSeqIds = "";
        }

        public Proto.Puser.Card ToProto()
        {
            string jsonStr = JsonConvert.SerializeObject(this);

            return JsonConvert.DeserializeObject<Proto.Puser.Card>(jsonStr);
        }
    }
}
