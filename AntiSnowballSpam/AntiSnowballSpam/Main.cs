using BepInEx;
using HarmonyLib;
using Utilla;

namespace AntiSnowballSpam
{
    [BepInPlugin(Id, Name, Version), BepInDependency("org.legoandmars.gorillatag.utilla")]
    [System.ComponentModel.Description("HauntedModMenu"), ModdedGamemode]
    internal class Main : BaseUnityPlugin
    {
        internal const string
            Id = "crafterbot.antisnowballspam",
            Name = "Anti-Snowball",
            Version = "1.0.1";
        internal static Main Instance;

        internal bool RoomModded;
        internal bool InValidMap;

        private Harmony harmony;

        internal Main()
        {
            Instance = this;
            harmony = new Harmony(Id);
        }

        private void OnEnable()
        {
            harmony.PatchAll();
        }

        private void OnDisable()
        {
            harmony.UnpatchSelf();
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
    }
}
