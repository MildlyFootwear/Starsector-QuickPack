using System.Runtime.InteropServices;

namespace Starsector_QuickPack
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            [DllImport("kernel32.dll")] static extern bool AllocConsole();
            AllocConsole();

            if (args.Length == 1 && Directory.Exists(args[0]))
            {
                Methods.parseModInfo(args[0]);
                if (Variables.modPackZipPath != "")
                {
                    Methods.packMod(args[0]);
                }
            }
            else
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.InitialDirectory = Environment.CurrentDirectory;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Methods.parseModInfo(dialog.SelectedPath);
                    if (Variables.modPackZipPath != "")
                    {
                        Methods.packMod(dialog.SelectedPath);
                    }
                }
            }
        }
    }
}