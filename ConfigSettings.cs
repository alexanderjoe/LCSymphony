using BepInEx.Configuration;

namespace LCSymphony
{
    public static class ConfigSettings
    {
        public static ConfigEntry<bool> PingEnabled { get; set; }
        public static ConfigEntry<string> LaunchOption { get; set; }
        public static ConfigEntry<bool> SkipTerminalBoot { get; set; }


        public static void Init()
        {
            PingEnabled = Plugin.Instance.Config.Bind("Ping Management", "PingEnabled", true,
                "Enable or disable the ping display in the top right corner.");
            LaunchOption = Plugin.Instance.Config.Bind("Launch Options", "LaunchOption", "online",
                "Choose between launching into online mode, lan mode, or normal startup. Online will launch the game directly to online mode, lan will launch the game directly to lan mode, and normal will launch the game normally. Valid options are: online, lan, normal.");
            SkipTerminalBoot = Plugin.Instance.Config.Bind("Launch Options", "SkipTerminalBoot", true,
                "Skip the terminal boot screen.");
        }
    }
}