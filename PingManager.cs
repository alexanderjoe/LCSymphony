using System.Collections;
using Steamworks;
using UnityEngine;

namespace LCSymphony
{
    public class PingManager : MonoBehaviour
    {
        public int Ping { get; private set; }
        private Coroutine _coroutine;

        private void Start()
        {
            if (!ConfigSettings.PingEnabled.Value)
                return;
            _coroutine = StartCoroutine(UpdatePingData());
        }

        private void OnDestroy()
        {
            StopCoroutine(_coroutine);
        }

        private IEnumerator UpdatePingData()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);

                if (GameNetworkManager.Instance.currentLobby != null ||
                    !SteamNetworkingUtils.LocalPingLocation.HasValue)
                {
                    if (SteamNetworkingUtils.LocalPingLocation != null)
                        Ping = SteamNetworkingUtils.EstimatePingTo(SteamNetworkingUtils.LocalPingLocation.Value);
                }
                else
                {
                    Plugin.Log($"Could not update ping data. Retrying in 5 seconds.");
                    yield return new WaitForSeconds(5f);
                }
            }
        }
    }
}