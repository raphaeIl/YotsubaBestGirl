using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YotsubaBestGirl.Common.Utils;
using YotsubaBestGirl.Proto.Pmisc;

namespace YotsubaBestGirl.Database.Entities
{
    [Table("t_user_work_lineup")]
    public class WorkLineupDB : UserOwnedEntity, IProtoConvertible<WorkLineup>
    {
        public int WorkId { get; set; }
        public int Status { get; set; }
        public int ExpectValue { get; set; }
        public int Result { get; set; }
        public int LottedConditionId1 { get; set; }
        public int LottedConditionId2 { get; set; }
        public int LottedConditionId3 { get; set; }
        public string CardIds { get; set; }
        public string CardResults { get; set; }
        public long MvpCardId { get; set; }
        public string LottedRewardSeqIds1 { get; set; }
        public string LottedRewardSeqIds2 { get; set; }
        public string LottedRewardSeqIds3 { get; set; }
        public uint StartDate { get; set; }
        public uint EndDate { get; set; }
        public int ShortMin { get; set; }
        public int AddCoin { get; set; }

        public WorkLineupDB(int workId)
        {
            WorkId = workId;
            Status = 0;
            ExpectValue = 0;
            Result = 0;
            LottedConditionId1 = 0;
            LottedConditionId2 = 0;
            LottedConditionId3 = 0;
            CardIds = "";
            CardResults = "";
            MvpCardId = 0;
            LottedRewardSeqIds1 = "";
            LottedRewardSeqIds2 = "";
            LottedRewardSeqIds3 = "";
            StartDate = 0;
            EndDate = 0;
            ShortMin = 0;
            AddCoin = 0;
        }

        public WorkLineup ToProto()
        {
            string jsonStr = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<WorkLineup>(jsonStr);
        }
    }
}
