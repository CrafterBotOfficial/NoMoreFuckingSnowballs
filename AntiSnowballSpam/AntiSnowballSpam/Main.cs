using BepInEx;
using HarmonyLib;

namespace AntiSnowballSpam
{
    [BepInPlugin(GUID, NAME, VERSION)]
    internal class Main : BaseUnityPlugin
    {
        internal const string
            GUID = "crafterbot.antisnowballspam",
            NAME = "Anti-Snowball",
            VERSION = "1.0.0";
        internal static Main Instance;

        internal bool RoomModded => GorillaNetworking.GorillaComputer.instance.currentGameMode.Contains("modded".ToUpper());
        internal bool InForest;

        internal Main()
        {
            Instance = this;
            new Harmony(GUID).PatchAll();
        }
    }
}
