using HarmonyLib;

namespace LCSymphony.Patches
{
    /*
     * This mod has been done a few times, but this is my edition.
     * I try to be safe and not forcibly skip the screens.
     * From my testing it seemed to break functionality
     * sometimes when I forcibly skipped the screens.
     */
    internal class SkipToStartPatch
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