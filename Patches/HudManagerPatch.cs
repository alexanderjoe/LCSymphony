using HarmonyLib;
using TMPro;
using UnityEngine;

namespace LCSymphony.Patches
{
    [HarmonyPatch(typeof(HUDManager))]
    internal class HudManagerPatch
    {
        private static TextMeshProUGUI _displayText;

        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        private static void PatchHudManagerStart(ref HUDManager __instance)
        {
            GameObject pingManagerDisplay = new GameObject("PingManagerDisplay");
            pingManagerDisplay.AddComponent<RectTransform>();
            TextMeshProUGUI component = pingManagerDisplay.AddComponent<TextMeshProUGUI>();

            // Configure the RectTransform
            RectTransform rectTransform = component.rectTransform;
            rectTransform.SetParent(__instance.debugText.transform.parent.parent.parent, false);
            rectTransform.parent = __instance.debugText.rectTransform.parent.parent.parent;
            rectTransform.anchorMin = new Vector2(1, 1);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(1, 1);
            rectTransform.sizeDelta = new Vector2(100, 100);
            rectTransform.anchoredPosition = new Vector2(50, -1);

            // Configure the TextMeshProUGUI
            component.font = __instance.controlTipLines[0].font;
            component.fontSize = 7f;
            component.text = $"Ping: {Plugin.PingManager.Ping}ms";
            component.overflowMode = TextOverflowModes.Overflow;
            component.enabled = true;

            _displayText = component;
            Plugin.Log("PingManagerDisplay component added to Canvas.");
        }

        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        private static void PatchHudManagerUpdate(ref HUDManager __instance)
        {
            if (__instance.NetworkManager.IsHost)
            {
                _displayText.text = "Ping: Host";
                return;
            }

            _displayText.text = $"Ping: {Plugin.PingManager.Ping}ms";
        }
    }
}