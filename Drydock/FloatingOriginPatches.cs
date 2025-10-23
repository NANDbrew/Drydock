using HarmonyLib;

namespace Drydock
{
    [HarmonyPatch(typeof(FloatingOriginManager))]
    //This is the actual patch that runs after FloatingOriginManager runs on startup
    public static class FloatingOriginManagerPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch("Start")]
        public static void StartPatch()
        {
            MatLib.RegisterMaterials();
            AssetTools.LoadBoats();

        }

    }

}
