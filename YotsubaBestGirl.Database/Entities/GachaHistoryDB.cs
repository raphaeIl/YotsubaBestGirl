using Grpc.Core;
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
    [Table("t_user_gacha_history")]
    public class GachaHistoryDB : UserOwnedEntity, IProtoConvertible<Proto.Pmisc.GachaHistory>
    {
        public int GachaId { get; set; }
        public int Step { get; set; }
        public int CurrentBoxId { get; set; }
        public int RankupBoxId { get; set; }
        public int SingleCnt { get; set; }
        public int TicketCnt { get; set; }
        public int LumpCnt { get; set; }
        public int TotalCnt { get; set; }
        public int LimitCnt { get; set; }
        public int GroupWeightCnt { get; set; }
        public int IsPaid { get; set; }
        public uint PaidDate { get; set; }
        public uint LastGachaDate { get; set; }
        public int PaidSingleResetCount { get; set; }
        public uint PaidSingleResetDate { get; set; }
        public int PaidLumpResetCount { get; set; }
        public uint PaidLumpResetDate { get; set; }

        public GachaHistoryDB(int gachaId)
        {
            GachaId = gachaId;
            Step = 0;
            CurrentBoxId = 0;
            RankupBoxId = 0;
            SingleCnt = 0;
            TicketCnt = 0;
            LumpCnt = 1;
            TotalCnt = 1;
            LimitCnt = 1;
            GroupWeightCnt = 0;
            IsPaid = 0;
            PaidDate = 1749572351;
            LastGachaDate = 1749572351;
            PaidSingleResetCount = 0;
            PaidSingleResetDate = 1749572351;
            PaidLumpResetCount = 0;
            PaidLumpResetDate = 1749572351;
        }

        public Proto.Pmisc.GachaHistory ToProto()
        {
            string jsonStr = JsonConvert.SerializeObject(this);

            return JsonConvert.DeserializeObject<Proto.Pmisc.GachaHistory>(jsonStr);
        }
    }
}
