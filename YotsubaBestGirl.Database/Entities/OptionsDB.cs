using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YotsubaBestGirl.Common.Utils;

namespace YotsubaBestGirl.Database.Entities
{
    [Table("t_user_options")]
    public class OptionsDB : UserOwnedEntity, IProtoConvertible<Proto.Puser.Options>
    {
        public int Bgm { get; set; }
        public int Se { get; set; }
        public int Voice { get; set; }
        public int PushSystem { get; set; }
        public int PushAppointment { get; set; }
        public int PushAp { get; set; }
        public int PushGuerrilla { get; set; }
        public int PushWork { get; set; }
        public int PushChat { get; set; }
        public int PushCooking { get; set; }
        public int ProtectCardR6 { get; set; }
        public int ProtectCardR5 { get; set; }
        public int ProtectCardR4 { get; set; }
        public int ProtectCardFirst { get; set; }
        public int Gyro { get; set; }
        public int PowerSaving { get; set; }

        public OptionsDB()
        {
            Bgm = 50;
            Se = 50;
            Voice = 50;
            PushSystem = 1;
            PushAppointment = 1;
            PushAp = 1;
            PushWork = 1;
            PushChat = 1;
            PushCooking = 1;
            ProtectCardR6 = 1;
            ProtectCardR5 = 1;
            ProtectCardFirst = 1;
            Gyro = 1;
        }

        public Proto.Puser.Options ToProto()
        {
            string jsonStr = JsonConvert.SerializeObject(this);

            return JsonConvert.DeserializeObject<Proto.Puser.Options>(jsonStr);
        }

    }
}
