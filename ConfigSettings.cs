using BepInEx.Configuration;

namespace LCSymphony
{
    public static class ConfigSettings
    {
        public static ConfigEntry<bool> PingEnabled { get; set; }

        public static void Init()
        {
            PingEnabled =
                Plugin.Instance.Config.Bind("Ping Management", "PingEnabled", true, "Enable or disable the ping display in the top right corner.");
        }
    }
}