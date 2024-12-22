using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starsector_QuickPack
{
    public static class Variables
    {
        public static string modName = "";
        public static string modVer = "";
        public static string modVerMajor = "";
        public static string modVerMinor = "";
        public static string modVerPatch = "";
        public static string modPath = "";
        public static string modPackZipPath = "";
        public static List<string> blacklist = ["\\.git\\" ];
    }
}
