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
    [Table("t_user_home_member_tap_motion")]
    public class HomeMemberTapMotionDB : UserOwnedEntity, IProtoConvertible<HomeMemberTapMotion>
    {
        public int TapMotionId { get; set; }

        public HomeMemberTapMotionDB(int tapMotionId)
        {
            TapMotionId = tapMotionId;
        }

        public HomeMemberTapMotion ToProto()
        {
            string jsonStr = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<HomeMemberTapMotion>(jsonStr);
        }
    }
}
