using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace YotsubaBestGirl.Common.Utils
{
    public static class Config
    {
        public static string ResourceDir = Path.Join(Path.GetDirectoryName(AppContext.BaseDirectory), "Resources");
        public static string PcapDir = Path.Join(ResourceDir, "Packets");

        public static string GameVersion = "v1_43_440";

        // these two are sent in account/certificate and in ALL response
        public static int ResourceVersion = 1906;
        public static int MasterVersion = 1497;
    }
}
