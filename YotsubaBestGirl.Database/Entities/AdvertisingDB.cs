using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using YotsubaBestGirl.Common.Utils;
using YotsubaBestGirl.Proto.Pmisc;

namespace YotsubaBestGirl.Database.Entities
{
    [Table("t_user_advertising")]
    public class AdvertisingDB : UserOwnedEntity, IProtoConvertible<Proto.Pmisc.Advertising>
    {
        public int AdvertisingId { get; set; }
        public int ViewCount { get; set; }
        public int RewardCountRemaining { get; set; }
        public uint LastResetAt { get; set; }
        public uint LastViewedAt { get; set; }

        public AdvertisingDB(int advertisingId)
        {
            AdvertisingId = advertisingId;
            ViewCount = 0;
            RewardCountRemaining = 0;
            LastResetAt = 0;
            LastViewedAt = 0;
        }

        public Advertising ToProto()
        {
            string jsonStr = JsonConvert.SerializeObject(this);

            return JsonConvert.DeserializeObject<Advertising>(jsonStr);
        }
    }
}
