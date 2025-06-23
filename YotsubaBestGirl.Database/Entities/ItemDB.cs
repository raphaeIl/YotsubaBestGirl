using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YotsubaBestGirl.Common.Utils;
using YotsubaBestGirl.Proto.Puser;

namespace YotsubaBestGirl.Database.Entities
{
    [Table("t_user_item")]
    public class ItemDB : UserOwnedEntity, IProtoConvertible<Proto.Puser.Item>
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public int Duration { get; set; }
        public uint ExpireDate { get; set; }

        public ItemDB(int itemId, int quantity, int duration, uint expireDate)
        {
            ItemId = itemId;
            Quantity = quantity;
            Duration = duration;
            ExpireDate = expireDate;
        }

        public Item ToProto()
        {
            return new Proto.Puser.Item()
            {
                ItemId = ItemId,
                Quantity = Quantity,
                Duration = Duration,
                ExpireDate = ExpireDate
            };
        }
    }
}
