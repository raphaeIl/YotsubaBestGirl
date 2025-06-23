using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YotsubaBestGirl.Common.Utils;

namespace YotsubaBestGirl.Database.Entities
{
    [Table("t_user_stage")]
    public class StageDB : UserOwnedEntity, IProtoConvertible<Proto.Puser.Stage>
    {
        public int StageId { get; set; }
        public int Status { get; set; }
        public int Score { get; set; }
        public int Rank { get; set; }
        public int UnitIdx { get; set; }
        public uint UpdatedAt { get; set; }

        public StageDB(int stageId)
        {
            StageId = stageId;
            Status = 2;
            Score = 0;
            Rank = 1;
            UnitIdx = 1;
            UpdatedAt = 0;
        }

        public Proto.Puser.Stage ToProto()
        {
            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Proto.Puser.Stage>(jsonStr);
        }
    }
}
