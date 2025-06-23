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
    [Table("t_user_member")]
    public class MemberDB : UserOwnedEntity, IProtoConvertible<Proto.Puser.Member>
    {
        public int MemberId { get; set; }
        public int MemberPictureId { get; set; }
        public string? ExceptHomeMessageIds { get; set; }
        public int Dearlevel { get; set; }
        public int Dearpoint { get; set; }
        public int NameRank { get; set; }
        public int ItemId { get; set; }
        public string? ItemIdQueue { get; set; }
        public int AddCount { get; set; }
        public uint ExpireDate { get; set; }

        public MemberDB(int memberId)
        {
            MemberId = memberId;
            MemberPictureId = memberId; // u r suppose to retrieve this from tables ig
            ExceptHomeMessageIds = "";
            ItemIdQueue = "";
        }

        public Proto.Puser.Member ToProto()
        {
            string jsonStr = JsonConvert.SerializeObject(this);

            return JsonConvert.DeserializeObject<Proto.Puser.Member>(jsonStr);
        }
    }
}
