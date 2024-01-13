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
            while (StartOfRound.Instance == null)
            {
                yield return new WaitForSeconds(3f);
            }

            while (true)
            {
                if (SteamNetworkingUtils.LocalPingLocation != null && SteamNetworkingUtils.LocalPingLocation.HasValue)
                {
                    Ping = SteamNetworkingUtils.EstimatePingTo(SteamNetworkingUtils.LocalPingLocation.Value);
                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    Plugin.Log($"Could not update ping data. Retrying in 10 seconds.");
                    yield return new WaitForSeconds(10f);
                }
            }
        }
    }
}