using HarmonyLib;

namespace LCSymphony.Patches
{
    internal class PreInitSceneScriptPatch
    {
        [HarmonyPatch(typeof(PreInitSceneScript), "Start")]
        [HarmonyPostfix]
        private static void chooseOnlineDefaultPatch(ref PreInitSceneScript __instance)
        {
            Plugin.Log("Skipping online mode selection.");

            __instance.ChooseLaunchOption(true);
        }

        [HarmonyPatch(typeof(InitializeGame), "Awake")]
        [HarmonyPostfix]
        private static void Test(ref InitializeGame __instance)
        {
            Plugin.Log("Skipping boot-up screen.");

            __instance.runBootUpScreen = false;
        }
    }
}