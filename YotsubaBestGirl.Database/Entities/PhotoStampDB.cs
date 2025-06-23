using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using YotsubaBestGirl.Common.Utils;

namespace YotsubaBestGirl.Database.Entities
{
    [Table("t_user_photostamp")]
    public class PhotoStampDB : UserOwnedEntity, IProtoConvertible<Proto.Puser.PhotoStamp>
    {
        public int PhotoStampId { get; set; }

        public PhotoStampDB(int photoStampId)
        {
            PhotoStampId = photoStampId;
        }

        public Proto.Puser.PhotoStamp ToProto()
        {
            string jsonStr = JsonConvert.SerializeObject(this);

            return JsonConvert.DeserializeObject<Proto.Puser.PhotoStamp>(jsonStr);
        }
    }
}
