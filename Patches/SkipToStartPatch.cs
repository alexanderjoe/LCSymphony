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
        private static void SetLaunchModePatch(ref PreInitSceneScript __instance)
        {
            Plugin.Log("Setting chosen quick-launch option.");

            switch (ConfigSettings.LaunchOption.Value)
            {
                case "online":
                    Plugin.Log("Launching into online mode.");
                    __instance.ChooseLaunchOption(true);
                    break;
                case "lan":
                    Plugin.Log("Launching into LAN mode.");
                    __instance.ChooseLaunchOption(false);
                    break;
                case "normal":
                    Plugin.Log("Allowing user choice of launch mode.");
                    break;
            }
        }

        [HarmonyPatch(typeof(InitializeGame), "Awake")]
        [HarmonyPostfix]
        private static void SkipTerminalBootPatch(ref InitializeGame __instance)
        {
            Plugin.Log("Skipping boot-up screen.");

            if (ConfigSettings.SkipTerminalBoot.Value)
            {
                __instance.runBootUpScreen = false;
            }
        }
    }
}