using HarmonyLib;
using Il2CppSystem;
using MelonLoader;
using UnityEngine.InputSystem.Utilities;

[assembly: MelonInfo(typeof(YarnHooker.Core), "YarnHooker", "1.0.0", "IRUZZ", null)]
[assembly: MelonGame("Nino", "project_nonmp")]

namespace YarnHooker
{
    public class Core : MelonMod
    {
        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Initialized.");
            Config.Init();
        }
    }

    [HarmonyPatch(typeof(Il2CppYarn.Dialogue), "SetNode", typeof(string))]
    public class SetNodePatch
    {
        public static void Prefix(Il2CppYarn.Dialogue __instance, string __0)
        {
            if (!Il2CppSystem.IO.File.Exists(Config.FilePath))
            {
                MelonLogger.Error($"{Config.FilePath} not found");
                return;
            }

            Il2CppYarn.Program prog_bak = __instance.Program;
            try
            {
                __instance.LoadProgram(Config.FilePath);
                Il2CppSystem.Collections.Generic.IEnumerable<string> NodeNames = __instance.NodeNames;
                if (NodeNames.IndexOf(__0) < 0)
                {
                    MelonLogger.Warning($"{__0} not found");
                    __instance.SetProgram(prog_bak);
                }
            }
            catch (System.Exception e)
            {
                MelonLogger.Error(e.Message);
                __instance.SetProgram(prog_bak);
            }
        }
    }
}