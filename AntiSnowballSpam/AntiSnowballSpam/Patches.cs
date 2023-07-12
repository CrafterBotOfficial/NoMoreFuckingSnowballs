using GorillaNetworking;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine;

namespace AntiSnowballSpam
{
    internal static class Patches
    {
        /* Fuck snowballs */

        [HarmonyPatch(typeof(Slingshot), nameof(Slingshot.LaunchNetworkedProjectile)), HarmonyPrefix]
        private static bool Slingshot_LaunchNetworkedProjectile_Prefix()
        {
            Debug.Log("Projectile Launched | Valid map " + Main.Instance.InValidMap);
            return !Main.Instance.RoomModded;
        }

        [HarmonyPatch(typeof(GorillaGameManager), nameof(GorillaGameManager.SpawnSlingshotPlayerImpactEffect))]
        [HarmonyReversePatch(HarmonyReversePatchType.Original)] // Using this to prevent messing with the anti cheat
        private static void GorillaGameManager_ProjectileHit_ReversePatch()
        {
            if (Main.Instance.RoomModded)
            {
                IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
                {
                    var List = new List<CodeInstruction>();
                    instructions.First(x => x.opcode == OpCodes.Call);
                    return List;
                }

                _ = Transpiler(null);
            }
        }

        /* Dont disable in mountains */

        [HarmonyPatch(typeof(GorillaNetworkJoinTrigger), "OnBoxTriggered"), HarmonyPostfix]
        private static void GeoOnTriggered(GorillaNetworkJoinTrigger __instance)
        {
            Main.Instance.InValidMap = !__instance.gameModeName.Contains("mountain");
        }
    }
}
