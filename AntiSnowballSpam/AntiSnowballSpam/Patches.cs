using GorillaNetworking;
using HarmonyLib;
using UnityEngine;

namespace AntiSnowballSpam
{
    [HarmonyPatch]
    internal class Patches
    { 
        [HarmonyPatch(typeof(Slingshot), "LaunchNetworkedProjectile"), HarmonyPrefix]
        private static bool LaunchSnowball()
        {
            Debug.Log("Projectile Launched | Valid map " + Main.Instance.InValidMap);
            return !Main.Instance.RoomModded;
        }
        [HarmonyPatch(typeof(GorillaGameManager), "SpawnSlingshotPlayerImpactEffect"), HarmonyPrefix]
        private static bool ProjectileHit()
        {
            Debug.Log("Projectile hit | Valid map " + Main.Instance.InValidMap);
            return !Main.Instance.RoomModded;
        }

        [HarmonyPatch(typeof(GorillaNetworkJoinTrigger), "OnBoxTriggered"), HarmonyPostfix]
        private static void GeoOnTriggered(GorillaNetworkJoinTrigger __instance)
        {
            Main.Instance.InValidMap = !__instance.gameModeName.Contains("mountain");
        }
    }
}
