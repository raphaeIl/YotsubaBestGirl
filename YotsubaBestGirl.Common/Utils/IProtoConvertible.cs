using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YotsubaBestGirl.Common.Utils
{
    //public class ProtoConvertible<TProto> : IProtoConvertible<TProto>
    //{
    //    public virtual TProto ToProto()
    //    {
    //        string jsonStr = JsonConvert.SerializeObject(this);

    //        return JsonConvert.DeserializeObject<TProto>(jsonStr);
    //    }
    //}

    public interface IProtoConvertible<TProto>
    {
        TProto ToProto();
    }
}
