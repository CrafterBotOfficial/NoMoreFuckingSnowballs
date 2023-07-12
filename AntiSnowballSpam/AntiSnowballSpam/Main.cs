using BepInEx;
using HarmonyLib;
using Utilla;

namespace AntiSnowballSpam
{
    [BepInPlugin("crafterbot.antisnowballspam", "Anti-Snowball", "1.0.2"), BepInDependency("org.legoandmars.gorillatag.utilla")]
    [System.ComponentModel.Description("HauntedModMenu"), ModdedGamemode]
    internal class Main : BaseUnityPlugin
    {
        internal static Main Instance;

        internal bool RoomModded;
        internal bool InValidMap;

        private Harmony _harmony;

        internal Main()
        {
            Instance = this;
            _harmony = new Harmony(Info.Metadata.GUID);
        }


        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.F4))
            {
                RoomModded = true;
                GorillaGameManager.instance.SpawnSlingshotPlayerImpactEffect(new UnityEngine.Vector3(), 0, 0, 0, 0, 1, new Photon.Pun.PhotonMessageInfo());
            }
        }

        #region Callbacksa and Events
        private void OnEnable()
        {
            _harmony.PatchAll(typeof(Patches));
        }

        private void OnDisable()
        {
            _harmony.UnpatchSelf();
        }

        [ModdedGamemodeJoin]
        private void OnGamemodeJoin()
        {
            RoomModded = true;
        }

        [ModdedGamemodeLeave]
        private void OnGamemodeLeave()
        {
            RoomModded = false;
        }
        #endregion
    }
}
