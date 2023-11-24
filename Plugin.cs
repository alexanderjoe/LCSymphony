using BepInEx;
using HarmonyLib;
using LCSymphony.Patches;

namespace LCSymphony
{
    [BepInPlugin(ModGuid, ModName, ModVersion)]
    public class Plugin : BaseUnityPlugin
    {
        private const string ModGuid = PluginInfo.PLUGIN_GUID;
        private const string ModName = PluginInfo.PLUGIN_NAME;
        private const string ModVersion = PluginInfo.PLUGIN_VERSION;

        private readonly Harmony _harmony = new Harmony(ModGuid);

        public static Plugin Instance;

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            _harmony.PatchAll(typeof(PreInitSceneScriptPatch));

            Logger.LogInfo($"Plugin {ModName}-{ModVersion} loaded!");
        }

        public static void Log(string message) => Instance.Logger.LogInfo(message);
    }
}