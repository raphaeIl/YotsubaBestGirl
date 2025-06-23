using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YotsubaBestGirl.Common.Utils;

namespace YotsubaBestGirl.Database.Entities
{
    [Table("t_user_member_picture")]
    public class MemberPictureDB : UserOwnedEntity, IProtoConvertible<Proto.Puser.MemberPicture>
    {
        public int MemberPictureId { get; set; }
        public string ReleasedEmotionResourceIds { get; set; }

        public MemberPictureDB(int memberPictureId)
        {
            MemberPictureId = memberPictureId;
            ReleasedEmotionResourceIds = "";
        }

        public Proto.Puser.MemberPicture ToProto()
        {
            return new Proto.Puser.MemberPicture()
            {
                MemberPictureId = MemberPictureId,
                ReleasedEmotionResourceIds = ReleasedEmotionResourceIds
            };
        }
    }
}
