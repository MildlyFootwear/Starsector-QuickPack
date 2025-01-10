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

        // Any path containing an element in this list will not be added.

        public static List<string> blacklist = [];

        /*
        This blacklist will be for paths from the mod folder that is being packed.
        For example, by default "Starsector/mods/myMod/.git/" would not be added, but "Starsector/mods/myMod/data/.git/" would be.
        */

        public static List<string> blacklistFromRoot = ["/.git/", "/out/"];
    }
}
