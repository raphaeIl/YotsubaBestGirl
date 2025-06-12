using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
