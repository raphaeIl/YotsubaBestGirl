using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YotsubaBestGirl.Common.Utils;

namespace YotsubaBestGirl.Database.Entities
{
    [Table("t_user_chapter")]
    public class ChapterDB : UserOwnedEntity, IProtoConvertible<Proto.Puser.Chapter>
    {
        public int ChapterId { get; set; }
        public int Status { get; set; }
        
        public ChapterDB(int chapterId, int status)
        {
            ChapterId = chapterId;
            Status = status;
        }

        public Proto.Puser.Chapter ToProto()
        {
            string jsonStr = JsonConvert.SerializeObject(this);

            return JsonConvert.DeserializeObject<Proto.Puser.Chapter>(jsonStr);
        }
    }
}
