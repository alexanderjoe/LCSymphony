using HarmonyLib;

namespace LCSymphony.Patches
{
    [HarmonyPatch(typeof(PreInitSceneScript))]
    internal class PreInitSceneScriptPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        private static void chooseOnlineDefaultPatch(ref PreInitSceneScript __instance)
        {
            Plugin.Log("Skipping online mode selection.");

            __instance.ChooseLaunchOption(true);
        }
    }
}