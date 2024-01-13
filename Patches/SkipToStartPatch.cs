using HarmonyLib;
using UnityEngine.SceneManagement;

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
        [HarmonyPatch(typeof(PreInitSceneScript), "SkipToFinalSetting")]
        [HarmonyPostfix]
        internal static void SetLaunchModePatch(PreInitSceneScript __instance, ref bool ___choseLaunchOption)
        {
            if (ConfigSettings.LaunchOption.Value == "normal")
            {
                return;
            }

            Plugin.Log("Setting chosen quick-launch option.");

            foreach (var panel in __instance.LaunchSettingsPanels)
            {
                panel.gameObject.SetActive(false);
            }

            __instance.currentLaunchSettingPanel = 0;
            __instance.headerText.text = "";
            __instance.continueButton.gameObject.SetActive(false);
            ___choseLaunchOption = true;

            var launchMode = ConfigSettings.LaunchOption.Value == "online";

            Plugin.Log($"Launching into {ConfigSettings.LaunchOption.Value} mode.");

            SceneManager.LoadScene(launchMode ? "InitScene" : "InitSceneLANMode");
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