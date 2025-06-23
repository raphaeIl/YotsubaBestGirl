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
    [Table("t_user_unit")]
    public class UnitDB : UserOwnedEntity, IProtoConvertible<Proto.Puser.Unit>
    {
        public int Idx { get; set; }
        public string UnitName { get; set; }
        public int MemberId1 { get; set; }
        public int MemberId2 { get; set; }
        public int MemberId3 { get; set; }
        public int MemberId4 { get; set; }
        public int MemberId5 { get; set; }
        public long CardId1 { get; set; }
        public long CardId2 { get; set; }
        public long CardId3 { get; set; }
        public long CardId4 { get; set; }
        public long CardId5 { get; set; }
        public long Sub1CardId1 { get; set; }
        public long Sub1CardId2 { get; set; }
        public long Sub1CardId3 { get; set; }
        public long Sub1CardId4 { get; set; }
        public long Sub1CardId5 { get; set; }
        public long Sub2CardId1 { get; set; }
        public long Sub2CardId2 { get; set; }
        public long Sub2CardId3 { get; set; }
        public long Sub2CardId4 { get; set; }
        public long Sub2CardId5 { get; set; }
        public long Sub3CardId1 { get; set; }
        public long Sub3CardId2 { get; set; }
        public long Sub3CardId3 { get; set; }
        public long Sub3CardId4 { get; set; }
        public long Sub3CardId5 { get; set; }
        public long Sub4CardId1 { get; set; }
        public long Sub4CardId2 { get; set; }
        public long Sub4CardId3 { get; set; }
        public long Sub4CardId4 { get; set; }
        public long Sub4CardId5 { get; set; }
        public int UnitSkillId { get; set; }

        public UnitDB(int idx)
        {
            Idx = idx;
            UnitName = "";
            MemberId1 = 1;
            MemberId2 = 2;
            MemberId3 = 3;
            MemberId4 = 4;
            MemberId5 = 5;
            CardId1 = 1924497643;
            CardId2 = 1924497636;
            CardId3 = 1924497639;
            CardId4 = 1924497574;
            CardId5 = 1924497635;
        }

        public Proto.Puser.Unit ToProto()
        {
            string jsonStr = JsonConvert.SerializeObject(this);

            return JsonConvert.DeserializeObject<Proto.Puser.Unit>(jsonStr);
        }
    }
}
