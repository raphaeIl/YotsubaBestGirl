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
    [Table("t_user_story")]
    public class StoryDB : UserOwnedEntity, IProtoConvertible<Proto.Puser.Story>
    {
        public int StoryId { get; set; }
        public int Status { get; set; }
        public int Choice1 { get; set; }
        public int Choice2 { get; set; }
        public int Choice3 { get; set; }

        public StoryDB(int storyId, int status)
        {
            StoryId = storyId;
            Status = status;
            Choice1 = 0;
            Choice2 = 0;
            Choice3 = 0;
        }

        public Proto.Puser.Story ToProto()
        {
            string jsonStr = JsonConvert.SerializeObject(this);

            return JsonConvert.DeserializeObject<Proto.Puser.Story>(jsonStr);
        }
    }
}
