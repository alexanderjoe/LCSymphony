using BepInEx;
using HarmonyLib;
using LCSymphony.Patches;
using UnityEngine;

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

        public static PingManager PingManager;

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            ConfigSettings.Init();

            _harmony.PatchAll(typeof(SkipToStartPatch));

            if (ConfigSettings.PingEnabled.Value)
                _harmony.PatchAll(typeof(HudManagerPatch));


            InitPingManager();

            Logger.LogInfo($"Plugin {ModName}-{ModVersion} loaded!");
        }

        private void InitPingManager()
        {
            var target = new GameObject("PingManager");
            DontDestroyOnLoad(target);
            target.hideFlags = HideFlags.HideAndDontSave;
            target.AddComponent<PingManager>();
            PingManager = target.GetComponent<PingManager>();
        }

        public static void Log(string message) => Instance.Logger.LogInfo(message);
    }
}