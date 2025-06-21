using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YotsubaBestGirl.Core;

namespace YotsubaBestGirl.Common.Utils
{
    public static class Util
    {
        public static string ConvertToPascalCase(string path)
        {
            return string.Concat(path
                .Split('/')
                .Select(word => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(word)));
        }

        public static Protocol GetProtocolFromRoute(string path)
        {
            if (Enum.TryParse<Protocol>(path.Replace("/", "_"), out Protocol protocol))
            {
                return protocol;
            } 
            
            else
            {
                return Protocol.Unknown;
            }
        }
    }
}
