using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using YotsubaBestGirl.Common.Utils;
using YotsubaBestGirl.Proto.Puser;

namespace YotsubaBestGirl.Database.Entities
{
    [Table("t_user_home_member_clothes")]
    public class HomeMemberClothesDB : UserOwnedEntity, IProtoConvertible<HomeMemberClothes>
    {
        public int ClothesId { get; set; }

        public HomeMemberClothesDB(int clothesId)
        {
            ClothesId = clothesId;
        }

        public HomeMemberClothes ToProto()
        {
            string jsonStr = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<HomeMemberClothes>(jsonStr);
        }
    }
}
