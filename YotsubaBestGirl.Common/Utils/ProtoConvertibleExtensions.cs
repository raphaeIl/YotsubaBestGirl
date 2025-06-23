using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YotsubaBestGirl.Common.Utils
{
    public static class ProtoConvertibleExtensions
    {
        public static List<TProto> ToProtoList<TProto>(this IEnumerable<IProtoConvertible<TProto>> source)
        {
            return source.Select(x => x.ToProto()).ToList();
        }
    }
}
