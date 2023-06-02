using GorillaNetworking;
using HarmonyLib;

namespace AntiSnowballSpam
{
    [HarmonyPatch]
    internal class Patches
    {
        [HarmonyPatch(typeof(GorillaGameManager), "LaunchSlingshotProjectile"), HarmonyPrefix]
        private static void Hook_SnowBallThrown()
        {
            if (Main.Instance.RoomModded && Main.Instance.InForest)
                return;
        }

        [HarmonyPatch(typeof(GorillaNetworkJoinTrigger), "OnBoxTriggered"), HarmonyPostfix]
        private static void Hook_GeoOnTriggered(GorillaNetworkJoinTrigger __instance)
        {
            Main.Instance.InForest = __instance.gameModeName == "forest";
        }
    }
}
