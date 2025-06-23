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
    [Table("t_user_app_icon")]
    public class AppIconDB : UserOwnedEntity, IProtoConvertible<AppIcon>
    {
        public int AppIconId { get; set; }

        public AppIconDB(int appIconId)
        {
            AppIconId = appIconId;
        }

        public AppIcon ToProto()
        {
            string jsonStr = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<AppIcon>(jsonStr);
        }
    }
}
