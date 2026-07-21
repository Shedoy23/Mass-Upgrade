using System;
using System.IO;
using HarmonyLib;
using TaleWorlds.MountAndBlade;

namespace MassUpgrade
{
    public sealed class SubModule : MBSubModuleBase
    {
        private static readonly string LogPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Personal),
            "Mount and Blade II Bannerlord", "Configs", "MassUpgrade.log");

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(LogPath));
                new Harmony("nasulski.massupgrade").PatchAll(typeof(SubModule).Assembly);

                int patched = 0;
                foreach (var _ in Harmony.GetAllPatchedMethods()) patched++;
                Log($"PatchAll OK; total patched methods now: {patched}");
            }
            catch (Exception ex)
            {
                Log($"PatchAll FAILED: {ex}");
            }
        }

        private static void Log(string s)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(LogPath));
                File.AppendAllText(LogPath,
                    DateTime.Now.ToString("HH:mm:ss") + " " + s + Environment.NewLine);
            }
            catch { }
        }
    }
}
