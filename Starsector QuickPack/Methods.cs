using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starsector_QuickPack
{
    public static class Methods
    {
        public static bool isBlacklisted(string path)
        {
            foreach (string s in Variables.blacklist)
            {
                if (path.Contains(s))
                {
                    return true;
                }
            }
            foreach (string s in Variables.blacklistFromRoot)
            {
                string blacklistedPath = Path.GetFullPath(Variables.modPath + s);
                if (path.Contains(blacklistedPath))
                {
                    return true;
                }
            }
            return false;
        }

        public static void parseModInfo(string dir)
        {
            if (File.Exists(dir + "/mod_info.json"))
            {
                foreach (string line in File.ReadAllLines(dir + "/mod_info.json"))
                {
                    List<string> lineFragments = line.Split("\"").ToList();
                    int index;
                    if (lineFragments.Contains("version"))
                    {
                        index = lineFragments.IndexOf("version");
                        if (!lineFragments[index + 1].Contains("{"))
                        {
                            Variables.modVer = lineFragments[index + 2];
                        }
                    }
                    if (lineFragments.Contains("major"))
                    {
                        index = lineFragments.IndexOf("major");
                        Variables.modVerMajor = lineFragments[index + 2];
                    }
                    if (lineFragments.Contains("minor"))
                    {
                        index = lineFragments.IndexOf("minor");
                        Variables.modVerMinor = lineFragments[index + 2];
                    }
                    if (lineFragments.Contains("patch"))
                    {
                        index = lineFragments.IndexOf("patch");
                        Variables.modVerPatch = lineFragments[index + 2];
                    }
                }
                if (Variables.modVer == "")
                {
                    if (Variables.modVerMajor == "")
                    {
                        Variables.modVer += "0.";
                    }
                    else
                    {
                        Variables.modVer += Variables.modVerMajor + ".";
                    }
                    if (Variables.modVerMinor == "")
                    {
                        Variables.modVer += "0.";
                    }
                    else
                    {
                        Variables.modVer += Variables.modVerMinor + ".";
                    }
                    if (Variables.modVerPatch == "")
                    {
                        Variables.modVer += "0";
                    }
                    else
                    {
                        Variables.modVer += Variables.modVerPatch;
                    }
                }
                Variables.modPath = dir;
                string s = Directory.GetParent(dir).FullName;
                Variables.modPackZipPath = s += dir.Replace(s, "")+" "+Variables.modVer+".zip";
            }
            else
            {
                MessageBox.Show("mod_info.json not found.", "QuickPack");
            }
        }

        public static void packMod(string dir)
        {
            if (File.Exists(Variables.modPackZipPath))
            {
                if (MessageBox.Show("Zip file already exists at\n" + Variables.modPackZipPath+"\n\nDelete file?", "QuickPack", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    File.Delete(Variables.modPackZipPath);
                } else
                {
                    return;
                }
            }
            using (ZipArchive zip = ZipFile.Open(Variables.modPackZipPath, ZipArchiveMode.Create))
            {
                packDirectory(dir, zip);
            }
            Clipboard.SetText(Variables.modVer);
            MessageBox.Show("Successfully packed to:\n"+Variables.modPackZipPath, "QuickPack");

        }

        public static void packDirectory(string dir, ZipArchive zip)
        {
            foreach (string file in Directory.EnumerateFiles(dir))
            {
                if (isBlacklisted(file))
                {
                    continue;
                }
                string packPath = file.Replace(Directory.GetParent(Variables.modPath).FullName, "").Replace("\\", "/").Substring(1);
                Console.WriteLine("Packing " + file + " to " + packPath);
                zip.CreateEntryFromFile(@file, packPath);
            }
            foreach(string directory in Directory.EnumerateDirectories(dir))
            {
                if (isBlacklisted(directory))
                {
                    continue;
                }
                packDirectory(directory, zip);
            }
        }
    }
}
